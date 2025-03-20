using AutoMapper; 
using DataAccessLayer.Repos;
using EntityLayer.DTOs; 

namespace BusinessLayer.Services
{
    public class PermissionService
    {
        private readonly PermissionRepo _repo = new PermissionRepo();
        private readonly IMapper _mapper;
        public PermissionService(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Permission id ye göre tüm yetkileri verir
        /// </summary>
        /// <param name="permissions"></param>
        /// <returns></returns>
        public ServiceResultDTO<List<PermissionDTO>> GetList(string permissions)
        {
            try
            {
                var data = _repo.GetPermissions(permissions);
                return new ServiceResultDTO<List<PermissionDTO>>(data:_mapper.Map<List<PermissionDTO>>(data));
            }
            catch (Exception ex)
            {
                return new ServiceResultDTO<List<PermissionDTO>>(exception: ex);
            }
        }



        /// <summary>
        ///  tüm yetkileri verir
        /// </summary>
        /// <returns></returns>
        public ServiceResultDTO<List<PermissionGroupDTO>> GetAllList()
        {
            try
            {
                var dataGroup = _mapper.Map<List<PermissionGroupDTO>>(_repo.GetPermissionGroup());
                var data = _mapper.Map<List<PermissionDTO>>(_repo.GetPermissions());
                foreach (var item in dataGroup)
                {
                    var permissions = data.Where(q => q.group_id == item.group_id).ToList();
                    if (permissions.Count>0)
                    {
                        item.permissions = permissions;
                    }
                }
                return new ServiceResultDTO<List<PermissionGroupDTO>>(data: dataGroup);
            }
            catch (Exception ex)
            {
                return new ServiceResultDTO<List<PermissionGroupDTO>>(exception: ex);
            }
        }
    }
}
