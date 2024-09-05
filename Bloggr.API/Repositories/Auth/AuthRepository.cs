using Bloggr.API.Models.API;
using Bloggr.API.Models.Domain;
using Bloggr.API.Models.DTO.Auth;
using Microsoft.AspNetCore.Identity;

namespace Bloggr.API.Repositories.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> userManager;
        private readonly ITokenRepository tokenRepository;
        private readonly IEmailSender<User> emailSender;

        public AuthRepository(UserManager<User> userManager, ITokenRepository tokenRepository, IEmailSender<User> emailSender)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            this.emailSender = emailSender;
        }
        public async Task<APIResponse> SignIn(SignInRequestDTO signInRequest)
        {
            var result = new APIResponse();
            var user = await userManager.FindByEmailAsync(signInRequest.Email);

            if (user == null)
            {
                result.Error = "User not found";
                return result;
            }

            var passwordIsValid = await userManager.CheckPasswordAsync(user, signInRequest.Password);
            if (!passwordIsValid)
            {
                result.Error = "Invalid password";
                return result;
            }

            var roles = await userManager.GetRolesAsync(user);
            if (roles == null || !roles.Any())
            {
                result.Error = "User has no roles assigned";
                return result;
            }

            var jwt = tokenRepository.CreateJWTToken(user, roles.ToList());

            result.Data = new
            {
                UserId = Guid.Parse(user.Id),
                user.Email,
                user.UserName,
                JWTToken = jwt
            };

            return result;
        }

        public async Task<APIResponse> SignUp(SignUpRequestDTO signUpRequest)
        {
            var result = new APIResponse();
            var user = new User
            {
                PhoneNumber = signUpRequest.PhoneNumber,
                FirstName = signUpRequest.FirstName,
                LastName = signUpRequest.LastName,
                UserName = signUpRequest.UserName,
                Email = signUpRequest.Email,
            };

            var res = await userManager.CreateAsync(user, signUpRequest.Password);

            if (res.Succeeded)
            {
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                await emailSender.SendConfirmationLinkAsync(user, signUpRequest.Email, token);
                await userManager.AddToRolesAsync(user, signUpRequest.Roles);
            }
            result.Data = new
            {
                UserId = Guid.Parse(user.Id),
                user.Email,
                user.UserName,
                user.PasswordHash
            };

            return result;
        }
    }
}