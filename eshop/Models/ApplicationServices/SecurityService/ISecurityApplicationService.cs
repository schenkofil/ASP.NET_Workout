using eshop.Areas.Security.ViewModels;
using eshop.Models.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eshop.Models.ApplicationServices
{
    public interface ISecurityApplicationService
    {
        Task<string[]> Register(RegisterViewModel vm, Roles role);
        Task<bool> Login(LoginViewModel vm);
        Task Logout();
        Task<User> FindUserByUsername(string username);
        Task<User> FindUserByEmail(string userEmail);
        Task<IList<string>> GetUserRoles(User user);
        Task<User> GetCurrentUser(ClaimsPrincipal principal);
        AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string returnUrl);
        Task<ExternalLoginInfo> GetExternalLoginInfoAsync();
        Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent, bool bypassTwoFactor);
        Task<IdentityResult> RegisterExternal(User user, Roles role);
        Task<IdentityResult> AddLoginAsync(User user, ExternalLoginInfo info);
        Task SignInAsync(User user, bool isPersistent);
    }
}
