using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MotokaEasy.Core.Infrastructure.MessageBroker.Contracts;
using MotokaEasy.Core.Infrastructure.MessageBroker.Dto;
using MotokaEasy.Core.Resilience.Contracts;
using MotokaEasy.Core.Shared;
using MotokaEasy.Domain.Events;
using MotokaEasy.Domain.Repositories;
using MotokaEasy.Domain.Shared;

namespace MotokaEasy.Consumers.Consumers;
[ExcludeFromCodeCoverage]
public class AtualizarNumeroPlacaVeiculoConsumer: BackgroundService
    {
        private readonly ILogger<AtualizarNumeroPlacaVeiculoConsumer> _logger;
        private readonly IMessageBroker _messageBroker;
        private readonly IResilience _resilience;
        private readonly IServiceProvider _serviceProvider;

        public AtualizarNumeroPlacaVeiculoConsumer(ILogger<AtualizarNumeroPlacaVeiculoConsumer> logger, IMessageBroker messageBroker, IResilience resilience, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _messageBroker = messageBroker;
            _resilience = resilience;
            _serviceProvider = serviceProvider;
        }

        private async void Listen(MessageBrokerDto messageBrokerDto)
        {
            try
            {
                _logger.LogInformation("Initializing Procedure 'AtualizarPlacaVeiculoConsumer'");
                var objEvent = ObjectByJson.ReturnObject<AtualizarNumeroPlacaVeiculoEvent>(messageBrokerDto.Message);
                
                using var scope = _serviceProvider.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IVeiculoRepository>();

                await repository.AtualizarPlacaVeiculoAsync(objEvent.VeiculoId, objEvent.Placa, new CancellationToken());
                _resilience.Execute(() => _messageBroker.ConfirmReading(messageBrokerDto));
                
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                _resilience.Execute(() => _messageBroker.RepublishQueue(QueueConstants.QueueAtualizarPlacaVeiculo, QueueConstants.QueueAtualizarPlacaVeiculoErro, ObjectByJson.ReturnObject<AtualizarNumeroPlacaVeiculoEvent>(messageBrokerDto.Message), messageBrokerDto, e.Message));
            }
            finally
            {
                _logger.LogInformation("Finished Procedure 'AtualizarPlacaVeiculoConsumer'");
            }
        }
      
        private void ConfigureListen()
        {
            _logger.LogInformation("ConfigureListen 'AtualizarPlacaVeiculoConsumer'");
            _messageBroker.ListenerQueue += Listen;
            _messageBroker.ConsumeMessageQueue(QueueConstants.QueueAtualizarPlacaVeiculo, 1, false);
        }
        
        private void Consumers()
        {
            ConfigureListen();
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(Consumers, stoppingToken);
        }
    }