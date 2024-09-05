using Bloggr.API.Models.API;
using Bloggr.API.Models.DTO.Auth;

namespace Bloggr.API.Repositories.Auth
{
    public interface IAuthRepository
    {
        Task<APIResponse> SignUp(SignUpRequestDTO signUpRequest);
        Task<APIResponse> SignIn(SignInRequestDTO signInRequest);
    }
}