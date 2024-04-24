using System;
using MotokaEasy.Domain.Command;
using MotokaEasy.Domain.Dto.InputDto;
using Xunit;

namespace MotokaEasy.Tests.UnitaryTests.Commands;

public class CriarLocacaoCommandTests
{
    private readonly CriarLocacaoCommand _invalidComand = new(new LocacaoInputDto{DataInicio = DateTime.Now.AddDays(-1), DataTerminio = DateTime.Now.AddDays(-2), DataPrevisaoTerminio = DateTime.Now, EntregadorId = Guid.NewGuid(), PlanoId = Guid.NewGuid(), VeiculoId = Guid.NewGuid()});
    private readonly CriarLocacaoCommand _validComand = new(new LocacaoInputDto { DataInicio = DateTime.Now, DataTerminio = DateTime.Now.AddDays(2), DataPrevisaoTerminio = DateTime.Now.AddDays(2), EntregadorId = Guid.NewGuid(), PlanoId = Guid.NewGuid(), VeiculoId = Guid.NewGuid()});

    [Fact]
    public void teste_criar_locacao_command_NoOk()
    {
        _invalidComand.Validate();
        Assert.True(_invalidComand.Invalid);
    }
        
    [Fact]
    public void teste_criar_locacao_command_Ok()
    {
        _validComand.Validate();
        Assert.True(_validComand.Valid);
    }
}