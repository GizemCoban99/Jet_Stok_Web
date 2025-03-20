using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebApp.Pages.Shared;

namespace WebApp.Pages.Admin.GenericValue
{
    public class GenericValueAddOrUpdateModel : _AdminBasePageModel
    {
        public List<GenericValueDTO> parents = new List<GenericValueDTO>();
        public GenericTypeDTO type;
        public GenericValueDTO formModel = new GenericValueDTO();
        private readonly GenericValueService _genericValueService;
        public GenericValueAddOrUpdateModel(UserService userService, PermissionService permissionService, RoleService roleService, IMapper mapper, GenericTypeService genericTypeService, GenericValueService genericValueService) : base(userService, permissionService, roleService, mapper, genericTypeService)
        {
            _genericValueService = genericValueService;
        }

        public void OnGet(int typeId, int id = 0)
        {
            if (typeId == 0)
                Response.Redirect("/PomeloAdmin/dashboard");

            type = _genericTypeService.Get(typeId).Data;

            formModel.type_id = typeId;

            if (id > 0)
            {
                formModel = _genericValueService.Get(id).Data;
            }
            if (type.is_parent_1 && type.parent_1_id > 0)
            {
                parents = _genericValueService.GetList(0, 10000, "", type.parent_1_id).Data;
            }
        }
        public JsonResult OnPost(GenericValueDTO formModel)
        {

            if (formModel.image_file != null)
            {
                var extent = Path.GetExtension(formModel.image_file.FileName);
                var randomName = ($"{Guid.NewGuid()}{extent}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\upload", randomName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    formModel.image_file.CopyTo(stream);
                }
                formModel.image = randomName;
            }
            else if (formModel.id > 0)
            {
                var image = _genericValueService.Get(formModel.id).Data.image;
                formModel.image = image;
            }

            if (formModel.image_description_file != null)
            {
                var extent = Path.GetExtension(formModel.image_description_file.FileName);
                var randomName = ($"{Guid.NewGuid()}{extent}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\upload", randomName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    formModel.image_description_file.CopyTo(stream);
                }
                formModel.image_description = randomName;
            }
            else if (formModel.id > 0)
            {

                var image_desc = _genericValueService.Get(formModel.id).Data.image_description;
                formModel.image_description = image_desc;
            }

            if (formModel.image_banner_file != null)
            {
                var extent = Path.GetExtension(formModel.image_banner_file.FileName);
                var randomName = ($"{Guid.NewGuid()}{extent}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\upload", randomName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    formModel.image_banner_file.CopyTo(stream);
                }
                formModel.image_banner = randomName;
            }
            else if (formModel.id > 0)
            {
                var image = _genericValueService.Get(formModel.id).Data.image_banner;
                formModel.image_banner = image;
            }


            var result = _genericValueService.AddOrUpdate(formModel);

            return new JsonResult(result);
        }
    }
}
