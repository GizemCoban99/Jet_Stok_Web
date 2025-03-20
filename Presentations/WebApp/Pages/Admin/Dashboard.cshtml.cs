using AutoMapper;
using BusinessLayer.Services;
using WebApp.Pages.Shared;

namespace WebApp.Pages.App
{
    public class DashboardModel : _AdminBasePageModel
    {
        public DashboardModel(UserService userService, PermissionService permissionService, RoleService roleService, IMapper mapper, GenericTypeService genericTypeService) : base(userService, permissionService, roleService, mapper, genericTypeService)
        {
        }

        public void OnGet()
        {

            pageTitle = "Dashboard";

        }
    }
}
