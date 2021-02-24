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
    public class SecurityApplicationService : ISecurityApplicationService
    {
        UserManager<User> _userManager;
        SignInManager<User> _signInManager;

        public SecurityApplicationService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }



        public Task<User> FindUserByEmail(string userEmail)
        {
            return _userManager.FindByEmailAsync(userEmail);
        }

        public Task<User> FindUserByUsername(string username)
        {
            return _userManager.FindByNameAsync(username);
        }

        public Task<User> GetCurrentUser(ClaimsPrincipal principal)
        {
            return _userManager.GetUserAsync(principal);
        }

        public Task<IList<string>> GetUserRoles(User user)
        {
            return _userManager.GetRolesAsync(user);
        }

        public async Task<bool> Login(LoginViewModel vm)
        {
            var result = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, vm.RememberMe, true);
            return result.Succeeded;
        }

        public Task Logout()
        {
            return _signInManager.SignOutAsync();
        }

        public async Task<string[]> Register(RegisterViewModel vm, Roles role)
        {
            User user = new User()
            {
                UserName = vm.Username,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Email = vm.Email,
            };

            string[] errors = null;
            var result = await _userManager.CreateAsync(user, vm.Password);
            if (result.Succeeded)
            {
                var resultRole = await _userManager.AddToRoleAsync(user, role.ToString());

                if (!resultRole.Succeeded)
                {
                    for (int i = 0; i < result.Errors.Count(); i++)
                        result.Errors.Append(result.Errors.ElementAt(i));
                }
            }

            if (result.Errors != null && result.Errors.Count() > 0)
            {
                errors = new string[result.Errors.Count()];
                for (int i = 0; i < result.Errors.Count(); i++)
                {
                    errors[i] = result.Errors.ElementAt(i).Description;
                }
            }

            return errors;
        }

        public AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string returnUrl)
        {
            return _signInManager.ConfigureExternalAuthenticationProperties(provider, returnUrl);
        }

        public Task<ExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            return _signInManager.GetExternalLoginInfoAsync();
        }

        public Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent, bool bypassTwoFactor)
        {
            return _signInManager.ExternalLoginSignInAsync(loginProvider, providerKey, isPersistent, bypassTwoFactor);
        }

        public async Task<IdentityResult> RegisterExternal(User user, Roles role)
        {
            var result = await _userManager.CreateAsync(user);
            var result2 = await _userManager.AddToRoleAsync(user, role.ToString());
            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(User user, ExternalLoginInfo info)
        {
            var result = await _userManager.AddLoginAsync(user, info);
            return result;
        }

        public async Task SignInAsync(User user, bool isPersistent)
        {
            await _signInManager.SignInAsync(user, isPersistent);
        }
    }
}
