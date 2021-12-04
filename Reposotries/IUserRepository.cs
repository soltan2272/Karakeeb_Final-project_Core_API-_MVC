using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Reposotries
{
    public interface IUserRepository
    {
        Task<AuthModel> SignUp(SignUpModel signUpModel);

        Task<AuthModel> Login(LoginModel loginModel);

        Task<string> AddRole(AddRoleModel Model);
    }
}
