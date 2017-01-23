using System;

namespace Company1.Department1.Project1.AuthenticationService.Interfaces
{
    public interface ILdapRepository
    {
        Boolean ValidateUser(String username, String password);
    }
}
