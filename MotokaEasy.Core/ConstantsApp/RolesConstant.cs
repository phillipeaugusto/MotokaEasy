using System.Diagnostics.CodeAnalysis;

namespace MotokaEasy.Core.ConstantsApp;

[ExcludeFromCodeCoverage]
public static class RolesConstant
{
    public const string RoleAdministrator = "adm";
    public const string RoleUser = "user";
    public const string RoleAdmUser = RoleAdministrator + "," + RoleUser;
}