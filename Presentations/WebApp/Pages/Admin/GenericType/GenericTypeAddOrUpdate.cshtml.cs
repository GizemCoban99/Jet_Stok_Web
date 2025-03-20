using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebApp.Pages.Shared;

namespace WebApp.Pages.Admin.GenericType
{
    public class GenericTypeAddOrUpdateModel : _AdminBasePageModel
    {
        public GenericTypeDTO formModel = new GenericTypeDTO();
        public List<GenericTypeDTO> list = new List<GenericTypeDTO>();
        public GenericTypeAddOrUpdateModel(UserService userService, PermissionService permissionService, RoleService roleService, IMapper mapper, GenericTypeService genericTypeService) : base(userService, permissionService, roleService, mapper, genericTypeService)
        {
        }

        public void OnGet(int id = 0)
        {
            list = _genericTypeService.GetList().Data;
            if (id > 0)
            {
                formModel = _genericTypeService.Get(id).Data;

                pageTitle = "Data Tipi Güncelle";
            }
            else
            {
                pageTitle = "Data Tipi Ekle";
            }
        }

        public JsonResult OnPost(GenericTypeDTO formModel)
        {
            var result = _genericTypeService.AddOrUpdate(formModel);

            return new JsonResult(result);
        }
    }
}
