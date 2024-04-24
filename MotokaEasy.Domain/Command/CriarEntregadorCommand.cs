using System;
using Flunt.Notifications;
using Flunt.Validations;
using MotokaEasy.Core.Domain.Contracts;
using MotokaEasy.Domain.Dto.InputDto;

namespace MotokaEasy.Domain.Command;

public class CriarEntregadorCommand: Notifiable, IValidator
{
    public CriarEntregadorCommand(EntregadorInputDto entregadorInputDto)
    {
        EntregadorInputDto = entregadorInputDto;
    }

    public EntregadorInputDto EntregadorInputDto { get; set; }
    
    public void Validate()
    {
        AddNotifications(
            new Contract()
                .Requires()
                .IsFalse(EntregadorInputDto.Nome.Length is < 10 or > 150   , "Nome", "O nome deve ter no minimo 10 caracteres e no máximo 150, verifique!.")
                .IsFalse(EntregadorInputDto.CnpjCpf.Length is < 11 or > 14 , "CnpjCpf", "CPF deve conter 11 digitos, cnpj deve ter 14 digitos, verifique!.")
                .IsFalse(EntregadorInputDto.DataNascimento.Year > DateTime.Now.Year, "DataNascimento", "A Data De Nascimento não deve ser Maior ou igual a data atual, verifique!.")
                .IsFalse((DateTime.Now.Year - EntregadorInputDto.DataNascimento.Year < 18), "DataNascimento", "O Entregador deve ter 18 anos ou mais, verifique!.")
                .IsFalse(EntregadorInputDto.NumeroCnh.Length is < 11 or > 11, "NumeroCnh", "A CNH deve conter 11 caracteres, verifique!")
                .IsFalse(EntregadorInputDto.TipoCnh == 0, "TipoCnh", "Tipo De CNH inválido, verifique!")
        );
    }
}
