using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using WebApp.Pages.Shared;

namespace WebApp.Pages.Admin.GenericType
{
    public class GenericTypesModel : _AdminBasePageModel
    {
        public List<GenericTypeDTO> list = new List<GenericTypeDTO>();
        public GenericTypesModel(UserService userService, PermissionService permissionService, RoleService roleService, IMapper mapper, GenericTypeService genericTypeService) : base(userService, permissionService, roleService, mapper, genericTypeService)
        {
        }

        public void OnGet()
        {
            pageTitle = "Data Tipleri";
            list = _genericTypeService.GetList().Data;
        }
    }
}
