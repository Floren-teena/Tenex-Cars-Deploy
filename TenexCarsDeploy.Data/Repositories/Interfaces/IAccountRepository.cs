using System.Threading.Tasks;
using TenexCarsDeploy.Data.Models;

namespace TenexCarsDeploy.Data.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<Operator> GetOperatorById(string Id);
        Task<bool> SetInitialPasswordAsync(string userId, string newPassword);


        //Task<string> GeneratePasswordResetTokenAsync(string userId);
    }
}
