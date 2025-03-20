using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebApp.Pages.Shared;

namespace WebApp.Pages.Settings.Role
{
    public class RolesModel : _AdminBasePageModel
    {
        public List<RoleDTO> data = new List<RoleDTO>();
        public RolesModel(UserService userService, PermissionService permissionService, RoleService roleService, IMapper mapper, GenericTypeService genericTypeService) : base(userService, permissionService, roleService, mapper, genericTypeService)
        {
        }

        public void OnGet()
        {
            pageTitle = "Roller";
            data = _roleService.GetList().Data;
        }

        public JsonResult OnPost(RoleDTO data)
        {
            var result = _roleService.Delete(data);
            return new JsonResult(result);
        }
    }
}
