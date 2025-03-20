using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using EntityLayer.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Security.Claims;
using WebApp.Helper;

namespace WebApp.Pages.Shared
{
    public abstract class _AdminBasePageModel : PageModel
    {
        protected readonly UserService _userService;
        protected readonly PermissionService _permissionService;
        protected readonly RoleService _roleService;

        protected readonly GenericTypeService _genericTypeService;

        protected readonly IMapper _mapper;

        public AccountInfoDTO accountInfo;

        public string pageTitle { get; set; }

        protected _AdminBasePageModel(UserService userService, PermissionService permissionService, RoleService roleService, IMapper mapper, GenericTypeService genericTypeService)
        {
            _userService = userService;
            _permissionService = permissionService;
            _roleService = roleService;
            _mapper = mapper;
            _genericTypeService = genericTypeService;
        }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            List<string> notAccessPages = new List<string>() { "" };
            accountInfo = UserInfo(HttpContext);
            ViewData["Permissions"] = $"{JsonConvert.SerializeObject(new List<PermissionDTO>())}";
            ViewData["AccountInfo"] = $"{JsonConvert.SerializeObject(accountInfo)}";
            ViewData["GenericTypes"] = $"{JsonConvert.SerializeObject(new List<GenericTypeDTO>())}";

            try
            {
                if (accountInfo.id > 0)
                {
                    ViewData["NameSurname"] = $"{accountInfo.name} {accountInfo.surname}";
                    ViewData["NameSurnameFirstChracter"] = $"{accountInfo.name.Substring(0, 1)}{accountInfo.surname.Substring(0, 1)}";
                    ViewData["Email"] = $"{accountInfo.email}";
                }
                else
                {
                    ViewData["NameSurname"] = $"";
                    ViewData["NameSurnameFirstChracter"] = $"";
                    ViewData["Email"] = $"";
                }

                ViewData["RefererUrl"] = Request.Headers["Referer"].ToString();

            }
            catch (Exception ex)
            {
                ViewData["NameSurname"] = $"";
                ViewData["NameSurnameFirstChracter"] = $"";
                ViewData["Email"] = $"";
            }

            ViewData["GenericTypes"] = $"{JsonConvert.SerializeObject(_genericTypeService.GetList().Data)}";


            var handlerName = context.HandlerMethod.Name;
            var page = context.RouteData.Values.First(f => f.Key == "page").Value == null ? "" : context.RouteData.Values.First(f => f.Key == "page").Value.ToString();
            if (!permissionPages.Any(q => q == page))
            {
                if (accountInfo.id > 0)
                {
                    var userData = _userService.Get(accountInfo.id);
                    if (userData.IsSuccess && userData.Data != null)
                    {
                        var user = userData.Data;

                        var userPermissions = SessionHelper.GetObjectFromJson<List<PermissionDTO>>(HttpContext.Session, "userPermissions");
                        if (userPermissions == null || userPermissions.Count == 0)
                        {
                            userData.Data.last_login_date = DateTime.Now;
                            var updateLastLogin = _userService.Update(_mapper.Map<User>(userData.Data));

                            var permissionData = _roleService.Get(userData.Data.role_id).Data;

                            if (permissionData != null && permissionData.id != 0)
                            {
                                userPermissions = _permissionService.GetList(permissionData.permissions).Data;

                                if (userPermissions == null)
                                    userPermissions = new List<PermissionDTO>();

                                SessionHelper.SetObjectAsJson(HttpContext.Session, "userPermissions", userPermissions);


                            }
                        }

                        ViewData["Permissions"] = $"{JsonConvert.SerializeObject(userPermissions)}";

                        if (user != null && user.id != 0)
                        {
                            SessionHelper.SetObjectAsJson(HttpContext.Session, "userRole", user.role_id);

                            if (user.role_id != 1)
                            {
                                if (!permissionPages.Contains(page))
                                {

                                    var perResult = userPermissions.FirstOrDefault(s => s.page == page);

                                    if (perResult == null ||
                                        (!string.IsNullOrEmpty(perResult.handler_method_name)
                                        && perResult.handler_method_name != handlerName)
                                        )
                                    {
                                        Response.Redirect("AccessDenied");
                                    }

                                }

                            }

                        }
                    }
                    else
                    {
                        Response.Redirect("login");
                    }
                }
                else
                {
                    Response.Redirect("/PomeloAdmin/login");
                }
            }
        }


        public bool LoginControl(HttpContext context)
        {
            var userInfo = UserInfo(context);
            if (userInfo.id > 0)
                return true;
            else
                return false;
        }

        public AccountInfoDTO UserInfo(HttpContext context)
        {
            var result = new AccountInfoDTO();
            var identity = (ClaimsIdentity)(context.User.Identity == null ? new ClaimsIdentity() : context.User.Identity);
            if (identity.IsAuthenticated)
            {
                result.id = Convert.ToInt32(identity.Claims.Where(q => q.Type == "Id").Select(q => q.Value).FirstOrDefault());
                result.email = identity?.Claims.Where(q => q.Type == "Email").Select(q => q.Value).FirstOrDefault();
                result.name = identity.Claims.Where(q => q.Type == "Name").Select(q => q.Value).FirstOrDefault();
                result.surname = identity.Claims.Where(q => q.Type == "Surname").Select(q => q.Value).FirstOrDefault();
                result.surname = identity.Claims.Where(q => q.Type == "Surname").Select(q => q.Value).FirstOrDefault();
                result.role_id = Convert.ToInt32(identity?.Claims.Where(q => q.Type == "RoleId").Select(q => q.Value).FirstOrDefault());

                if (identity.Claims.Where(q => q.Type == "IsAdminLogin").Select(q => q.Value).FirstOrDefault() != null)
                {
                    try
                    {
                        result.is_admin_login = Convert.ToBoolean(identity.Claims.Where(q => q.Type == "IsAdminLogin").Select(q => q.Value).FirstOrDefault());
                    }
                    catch (Exception)
                    {
                        result.is_admin_login = false;
                    }
                }

            }

            return result;
        }



        private static List<string> permissionPages = new List<string>
        {
            "/Admin/Login"
        };


    }
}
