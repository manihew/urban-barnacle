using Company1.Department1.Project1.AuthenticationService.Models;
using System;
using System.Threading.Tasks;

namespace Company1.Department1.Project1.AuthenticationService.Interfaces
{
    public interface ITokenRepository
    {
        Task SaveTokenAsync(User user);

        Task<Token> GetTokenAsync(String id);

        Task DeleteTokenAsync(String id);
    }
}