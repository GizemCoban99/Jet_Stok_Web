using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using EntityLayer.DTOs.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.Pages.Shared;

namespace WebApp.Pages.Account
{
    public class LoginModel : _AdminBasePageModel
    {
        public LoginModel(UserService userService, PermissionService permissionService, RoleService roleService, IMapper mapper, GenericTypeService genericTypeService) : base(userService, permissionService, roleService, mapper, genericTypeService)
        {

        }

        public void OnGet()
        {
        }

        public void OnGetLogout()
        {
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync().GetAwaiter().GetResult();
            Thread.Sleep(1000);

            Response.Redirect("/PomeloAdmin/login");
        }
        public JsonResult OnPostLogin(LoginDTO data)
        {
            var result = new FormPostResultDTO<LoginDTO>();

            var validate = new LoginDTOValidator().Validate(data);

            if (!validate.IsValid)
            {
                result = new FormPostResultDTO<LoginDTO>(validate);
            }
            else
            {
                result.data = new LoginDTO();

                var userLogin = _userService.Login(data);
                if (userLogin.IsSuccess)
                {
                    var userData = userLogin.Data;


                    var userClaims = new List<Claim>()
                        {
                        new Claim("Id", userData.id.ToString()),
                        new Claim("Email", userData.email),
                        new Claim("Name", userData.name),
                        new Claim("Surname", userData.surname),
                        new Claim("RoleId", userData.role_id.ToString())
                    };

                    var grandmaIdentity = new ClaimsIdentity(userClaims, "User Identity");

                    var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
                    HttpContext.SignInAsync(userPrincipal);

                    result.redirectUrl = "/PomeloAdmin/dashboard";

                }
                else
                {
                    result = new FormPostResultDTO<LoginDTO>(errorMessage: "Email veya parola hatalý. Lütfen tekrar deneyin.");
                }


            }

            return new JsonResult(result);
        }

    }
}
