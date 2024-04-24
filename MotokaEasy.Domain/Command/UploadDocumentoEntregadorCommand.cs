using System;
using System.Text.RegularExpressions;
using Flunt.Notifications;
using Flunt.Validations;
using Microsoft.AspNetCore.Http;
using MotokaEasy.Core.Domain.Contracts;
using MotokaEasy.Core.Messages.Shared;

namespace MotokaEasy.Domain.Command;

public class UploadDocumentoEntregadorCommand: Notifiable, IValidator
{
    private readonly Regex _fileRegex = new(@"\.(png|bmp)$", RegexOptions.IgnoreCase);
    public UploadDocumentoEntregadorCommand(Guid entregadorId, IFormFile file)
    {
        EntregadorId = entregadorId;
        File = file;
    }

    public Guid EntregadorId { get; set; }
    public IFormFile File { get; set; }
    
    public void Validate()
    {
        AddNotifications(
            new Contract()
                .Requires()
                .IsFalse(EntregadorId == Guid.Empty, "EntregadorId", GlobalMessageConstants.CampoInvalidoOuInexistente)
                .IsFalse(File == null, "File", "Arquivo não informado, verifique!")
                .IsFalse(File != null && !_fileRegex.IsMatch(File.FileName), "File", "Arquivo de formato inválido, só e permitido .png e .bmp, verifique!")
        );
    }
}