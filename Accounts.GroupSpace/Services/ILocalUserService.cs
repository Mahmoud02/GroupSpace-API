using Accounts.GroupSpace.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.GroupSpace.Services
{
    public interface ILocalUserService
    {
        
        Task<bool> ValidateCredentialsAsync(string userName, string password);
        Task<IEnumerable<UserClaim>> GetUserClaimsBySubjectAsync(string subject);
        Task<User> GetUserByUserNameAsync(string userName);
        Task<User> GetUserBySubjectAsync(string subject);
       
        void AddUser(User userToAdd,string password);
        Task<bool> IsUserActive(string subject);
        Task<bool> ActivateUser(string securityCode);
        Task<bool> SaveChangesAsync();
        Task<string> InitiatePasswordResetRequest(string email);
        Task<bool> SetPassword(string securityCode,string password);     
       
    }
}

