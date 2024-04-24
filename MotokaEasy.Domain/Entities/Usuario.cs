using MotokaEasy.Core.ConstantsApp;
using MotokaEasy.Core.Domain.Entities;
using MotokaEasy.Core.Shared;
using MotokaEasy.Domain.Dto.OutPutDto;

namespace MotokaEasy.Domain.Entities;

public class Usuario: Entity<Usuario, Usuario, UsuarioOutPutDto>
{
    public Usuario() { }

    

    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Numero { get; set; }
    public string Role { get; set; }
    
    public void SetRoleUser()
    {
        Role = RolesConstant.RoleUser;
    }
    public void CriptografarPassWord()
    {
        Senha = Function.Generate256(Senha);
    }
}