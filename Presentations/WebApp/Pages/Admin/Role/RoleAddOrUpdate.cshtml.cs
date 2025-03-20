using AutoMapper;
using BusinessLayer.Services;
using CoreLayer;
using EntityLayer.DTOs;
using EntityLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApp.Helper;
using WebApp.Pages.Shared;

namespace WebApp.Pages.Settings.Role
{
    public class RoleAddOrUpdateModel : _AdminBasePageModel
    {
        public RoleDTO formModel = new RoleDTO();
        public List<PermissionGroupDTO> permissions = new List<PermissionGroupDTO>();
        public List<long> activePermissions = new List<long>();
        public RoleAddOrUpdateModel(UserService userService, PermissionService permissionService, RoleService roleService, IMapper mapper, GenericTypeService genericTypeService) : base(userService, permissionService, roleService, mapper, genericTypeService)
        {
        }

        public void OnGet(int id = 0)
        {
            permissions = _permissionService.GetAllList().Data;
            if (id > 0)
            {
                formModel = _roleService.Get(id).Data;
                activePermissions = formModel.permissions != null ? formModel.permissions.Split(',').Select(s => s.ToLong()).ToList() : new List<long>();

                pageTitle = "Rol Güncelle";
            }
            else
            {
                pageTitle = "Rol Ekle";
            }
        }

        public JsonResult OnPost(RoleDTO formModel)
        {
            var result = _roleService.AddOrUpdate(formModel);

            if (formModel.id != 0)
                SessionHelper.SetObjectAsJson<List<Permission>>(HttpContext.Session, "userPermissions", null);

            return new JsonResult(result);
        }
    }
}
