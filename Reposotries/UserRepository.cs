using AutoMapper;
using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.Models;
using Models.Models.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Userr;

namespace Reposotries
{
    public class UserRepository : IUserRepository
    {
       UserManager<User> UserManager;
       RoleManager<Role> roleManager;
        Project_Context Context;
        private readonly IMapper _mapper;
        public IConfiguration Configuration { get; }
        public UserRepository(UserManager<User> userManager,
            IConfiguration configuration, RoleManager<Role> _roleManager,
            Project_Context context, IMapper mapper)
        {
            UserManager = userManager;
            Configuration = configuration;
            roleManager = _roleManager;
            Context=context;
            _mapper = mapper;
        }


        public async Task<AuthModel> SignUp(SignUpModel signUpModel)
        {

            if (await UserManager.FindByEmailAsync(signUpModel.Email) is not null)
                return new AuthModel { Message = "Email is already registered" };


            if (await UserManager.FindByNameAsync(signUpModel.Full_Name) is not null)
                return new AuthModel { Message = "UserName is already registered Please Select another one" };

            User Temp = signUpModel.ToModel();
            var result = await UserManager.CreateAsync(Temp, signUpModel.Password);
            if (!result.Succeeded)
            {
                string errors = string.Empty;
                foreach (var err in result.Errors)
                {
                    errors += $"{err.Description} , ";
                }
                return new AuthModel
                {
                    Message = errors
                };
            }

            await UserManager.AddToRoleAsync(Temp, "User");


            var userClaims = await UserManager.GetClaimsAsync(Temp);
            var roles = await UserManager.GetRolesAsync(Temp);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var SigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]));

            var Claims = new List<Claim>()
            {

                new Claim(ClaimTypes.Name,signUpModel.Full_Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email,signUpModel.Email),
                new Claim("uid",$"{Temp.Id}")
            }
            .Union(userClaims)
            .Union(roleClaims);



            var Token = new JwtSecurityToken
                (
                    issuer: Configuration["JWT:ValidIssuer"],
                    audience: Configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(14),
                    signingCredentials: new SigningCredentials(SigninKey, SecurityAlgorithms.HmacSha256Signature),
                    claims: Claims
                );


            return new AuthModel
            {
                Email = Temp.Email,
                ExpiresOn = Token.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(Token),
                Username = Temp.UserName,
                User_ID = Temp.Id
                
            };
        }
     public async Task<AuthModel> Login(LoginModel model)
        {
            var authModel = new AuthModel();

            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user is null || !(await UserManager.CheckPasswordAsync(user, model.Password)))
            {
                authModel.Message = "Email or password is inncorrect";
                authModel.IsAuthenticated =false;
                return authModel;
            }

            var userClaims = await UserManager.GetClaimsAsync(user);
            var roles = await UserManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var SigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]));

            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("uid",$"{user.Id}")
            }
            .Union(userClaims)
            .Union(roleClaims);



            var Token = new JwtSecurityToken
                (
                    issuer: Configuration["JWT:ValidIssuer"],
                    audience: Configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(14),
                    signingCredentials: new SigningCredentials(SigninKey, SecurityAlgorithms.HmacSha256Signature),
                    claims: Claims
                );
            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(Token);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.ExpiresOn = Token.ValidTo;
            authModel.Roles = roles.ToList();
            authModel.User_ID = user.Id;
            return authModel;

        }

        public async Task<string> AddRole(AddRoleModel Model)
        {
            var user = await UserManager.FindByIdAsync(Model.UserID.ToString());

            if (user is null || !await roleManager.RoleExistsAsync(Model.Role))
                return "Invalid user ID or Role";

            if (await UserManager.IsInRoleAsync(user,Model.Role))
                return "User already assigned to this role";

            var result = await UserManager.AddToRoleAsync(user,Model.Role);

            return result.Succeeded ? string.Empty : "Sonething went wrong";
        }
        public async Task<List<ViewUser>> GetUsersAsync()
        {


            var alluser = UserManager.GetUsersInRoleAsync("User").Result;
           
            List<ViewUser> users = _mapper.Map<List<ViewUser>>(alluser);
          
            return users;
        }

        public async Task<List<ViewUser>> GetSellersAsync()
        {


            var allsellers = UserManager.GetUsersInRoleAsync("Seller").Result;

            List<ViewUser> users = _mapper.Map<List<ViewUser>>(allsellers);

            return users;
        }
        public async Task<List<ViewUser>> GetAdminsAsync()
        {


            var alladmins = UserManager.GetUsersInRoleAsync("Admin").Result;

            List<ViewUser> users = _mapper.Map<List<ViewUser>>(alladmins);

            return users;
        }

        public async Task<ViewUser> GetUserBYIDAsync(int id)
        {
            var res= await UserManager.FindByIdAsync(id.ToString());
            ViewUser usr = _mapper.Map<ViewUser>(res);
            return usr;
            
        }

        public async Task<List<User>> DeleteUser(int id)
        {
            var usr = await UserManager.FindByIdAsync(id.ToString());
            var res = await UserManager.DeleteAsync(usr);
            var users = Context.Users.ToList();
            return users;

        }
     
    }

   
}
