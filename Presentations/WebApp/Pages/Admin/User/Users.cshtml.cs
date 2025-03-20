using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebApp.Pages.Shared;

namespace WebApp.Pages.Settings.User
{
    public class UsersModel : _AdminBasePageModel
    {
        public List<UserDTO> data = new List<UserDTO>();
        public UsersModel(UserService userService, PermissionService permissionService, RoleService roleService, IMapper mapper, GenericTypeService genericTypeService) : base(userService, permissionService, roleService, mapper, genericTypeService)
        {
        }

        public void OnGet()
        {

            pageTitle = "Kullanýcýlar";
            data = _userService.GetList().Data.Where(q => q.role_id > 1).ToList();
        }

        public JsonResult OnPost(UserDTO data)
        {
            var result = _userService.Delete(data);
            return new JsonResult(result);
        }
    }
}
