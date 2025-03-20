using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebApp.Pages.Shared;

namespace WebApp.Pages.Settings.User
{
    public class UserAddOrUpdateModel : _AdminBasePageModel
    {
        public UserDTO formModel = new UserDTO();
        public List<RoleDTO> roles = new List<RoleDTO>();
        public UserAddOrUpdateModel(UserService userService, PermissionService permissionService, RoleService roleService, IMapper mapper, GenericTypeService genericTypeService) : base(userService, permissionService, roleService, mapper, genericTypeService)
        {
        }

        public void OnGet(int id = 0)
        {
            roles = _roleService.GetList().Data;
            if (id > 0 && id > 1)
            {
                formModel = _userService.Get(id).Data;

                pageTitle = "Kullanýcý Güncelle";
            }
            else
            {
                pageTitle = "Kullanýcý Ekle";
            }
        }

        public JsonResult OnPost(UserDTO formModel)
        {

            var result = _userService.AddOrUpdate(formModel);

            return new JsonResult(result);
        }
    }
}
