using System;
using System.Collections.Generic;
using MotokaEasy.Domain.Dto.InputDto;
using MotokaEasy.Domain.Dto.OutPutDto;

namespace MotokaEasy.Tests.UnitaryTests.Repositories;

public class PlanoRepositoryTests:  RepositoryBaseTests<PlanoInputDto, PlanoOutPutDto>
{
    private static readonly Guid GuidFilter = Guid.NewGuid();
    private static readonly PlanoInputDto DatabaseInput = new() {Descricao = "Plano a", Valor = 10.00, QuantidadeDias = 2};
    private static readonly PlanoInputDto DatabaseInputSecundario = new() { Descricao = "Plano b", Valor = 20.00, QuantidadeDias = 1};
    private static readonly PlanoOutPutDto DatabaseOutPut = new() {Id = GuidFilter, Descricao = "Plano a", Valor = 20.00, QuantidadeDias = 1};
    private static readonly List<PlanoInputDto> ListInput = [DatabaseInput, DatabaseInputSecundario];
    private static readonly List<PlanoOutPutDto> ListOutPut = [DatabaseOutPut];
    
    public PlanoRepositoryTests() : base(DatabaseInput, DatabaseOutPut, ListInput, GuidFilter, ListOutPut)
    {
    }
}