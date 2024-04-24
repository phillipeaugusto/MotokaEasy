using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using MotokaEasy.Domain.Dto.OutPutDto;
using MotokaEasy.Domain.Entities;
using MotokaEasy.Domain.Repositories;
using Xunit;
using static System.Threading.Tasks.Task;

namespace MotokaEasy.Tests.UnitaryTests.Repositories;

public class EntregadorRepositoryTests: RepositoryBaseTests<Entregador, EntregadorOutPutDto>
{
    private readonly Mock<IEntregadorRepository> _mockRepository = new();
    private static readonly Guid GuidFilter = Guid.NewGuid();
    private static readonly Entregador DatabaseInput = new() {Id = GuidFilter, CnpjCpf = "00567845372", NumeroCnh = "1223454576", TipoCnh = 1, Nome = "entregador x"};
    private static readonly EntregadorOutPutDto DatabaseOutPut = new() {Id = GuidFilter, CnpjCpf = "00567845372", NumeroCnh = "1223454576", TipoCnh = 1, Nome = "entregador x"};
    private static readonly EntregadorOutPutDto DatabaseOutPutSecundario = new() {Id = new Guid(), CnpjCpf = "00567845373", NumeroCnh = "1223454596", TipoCnh = 1, Nome = "entregador z"};
    private static readonly Entregador DatabaseInputSecundario = new() {Id = GuidFilter, CnpjCpf = "00567845371", NumeroCnh = "1223454574", TipoCnh = 1, Nome = "entregador y"};
    private static readonly List<Entregador> ListInput = [DatabaseInput, DatabaseInputSecundario];
    private static readonly List<EntregadorOutPutDto> ListOutPut = [DatabaseOutPut, DatabaseOutPutSecundario];
    private static readonly List<EntregadorOutPutDto> ListValidarSeCnhEstaVinculadaAlgumEntregadorOk = [DatabaseOutPut, DatabaseOutPutSecundario];

   
    public EntregadorRepositoryTests() : base(DatabaseInput, DatabaseOutPut, ListInput, GuidFilter, ListOutPut)
    {
        
    }
    
    [Fact]
    public async Task teste_validar_se_cnh_esta_vinculada_algum_entregador_Ok()
    {
        _mockRepository.Setup(x => x.ValidarSeCnhEstaVinculadaAlgumEntregadorAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(FromResult(true));
        var obj = await _mockRepository.Object.ValidarSeCnhEstaVinculadaAlgumEntregadorAsync("00000002", new CancellationToken());
        Assert.True(obj);
    }
    
    [Fact]
    public async Task teste_validar_se_cnh_esta_vinculada_algum_entregador_NoOk()
    {
        _mockRepository.Setup(x => x.ValidarSeCnhEstaVinculadaAlgumEntregadorAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(FromResult(false));
        var obj = await _mockRepository.Object.ValidarSeCnhEstaVinculadaAlgumEntregadorAsync("00000001", new CancellationToken());
        Assert.False(obj);
    }
    
    [Fact]
    public async Task teste_validar_se_cnpj_cpf_esta_vinculado_algum_entregador_Ok()
    {
        _mockRepository.Setup(x => x.ValidarSeCnpjCpfEstaVinculadoAlgumEntregadorAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(FromResult(false));
        var obj = await _mockRepository.Object.ValidarSeCnpjCpfEstaVinculadoAlgumEntregadorAsync("00000001", new CancellationToken());
        Assert.False(obj);
    }
    
    [Fact]
    public async Task teste_validar_se_cnpj_cpf_esta_vinculado_algum_entregador_NoOk()
    {
        _mockRepository.Setup(x => x.ValidarSeCnpjCpfEstaVinculadoAlgumEntregadorAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(FromResult(true));
        var obj = await _mockRepository.Object.ValidarSeCnpjCpfEstaVinculadoAlgumEntregadorAsync("00000000", new CancellationToken());
        Assert.True(obj);
    }
}