using System;
using System.Collections.Generic;
using MotokaEasy.Domain.Entities;

namespace MotokaEasy.Tests.UnitaryTests.Repositories;

public class ConfiguracaoRepositoryTests: RepositoryBaseTests<Configuracao, Configuracao>
{
    private static readonly Guid GuidFilter = Guid.NewGuid();
    private static readonly Configuracao DatabaseInput = new() {Descricao = "param a", Valor = "1"};
    private static readonly Configuracao DatabaseOutPut = new() {Descricao = "param a", Valor = "1"};
    private static readonly Configuracao DatabaseInputSecundario = new() {Descricao = "param b", Valor = "2"};
    private static readonly List<Configuracao> ListInput = new() { DatabaseInput, DatabaseInputSecundario };
    
    public ConfiguracaoRepositoryTests() : base(DatabaseInput, DatabaseOutPut, ListInput, GuidFilter, ListInput)
    {
    }
}