using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using EntityLayer.DTOs.Component.Datatable;
using Microsoft.AspNetCore.Mvc;
using WebApp.Pages.Shared;

namespace WebApp.Pages.Admin.GenericValue
{
    public class GenericValuesModel : _AdminBasePageModel
    {
        public GenericTypeDTO type;
        private readonly GenericValueService _genericValueService;
        public GenericValuesModel(UserService userService, PermissionService permissionService, RoleService roleService, IMapper mapper, GenericTypeService genericTypeService, GenericValueService genericValueService) : base(userService, permissionService, roleService, mapper, genericTypeService)
        {
            _genericValueService = genericValueService;
        }

        public void OnGet(int id)
        {
            type = _genericTypeService.Get(id).Data;
            pageTitle = type.type_name;
        }

        public JsonResult OnPostData(DataTableRequest param, int id)
        {
            var data = _genericValueService.GetList(param.start, param.length, param.search.value, id);
            return new JsonResult(new DataTableResponse<List<GenericValueDTO>>
            {
                draw = param.draw,
                recordsTotal = data.Data.Count,
                recordsFiltered = data.total_rows,
                data = data.Data
            });
        }


        public JsonResult OnPost(GenericValueDTO data)
        {
            var result = _genericValueService.Delete(data);
            return new JsonResult(result);
        }
    }
}
