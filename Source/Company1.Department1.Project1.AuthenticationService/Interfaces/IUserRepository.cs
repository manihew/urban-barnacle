using Company1.Department1.Project1.AuthenticationService.Models;
using System.Threading.Tasks;

namespace Company1.Department1.Project1.AuthenticationService.Interfaces
{
    public interface IUserRepository
    {
        Task SaveAuthenticationAsync(User user);
    }
}
