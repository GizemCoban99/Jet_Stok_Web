using AutoMapper; 
using DataAccessLayer.Repos;
using EntityLayer.DTOs; 
using EntityLayer.Models; 

namespace BusinessLayer.Services
{
    public class RoleService 
    {

        private readonly IMapper _mapper;
        private readonly RoleRepo _repo = new RoleRepo();
        public RoleService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ServiceResultDTO<RoleDTO> Get(int id)
        {
            try
            {
                var data = _repo.GetByIdRole(id);
                return new ServiceResultDTO<RoleDTO>(data: _mapper.Map<RoleDTO>(data));
            }
            catch (Exception ex)
            {
                return new ServiceResultDTO<RoleDTO>(exception: ex);
            }
        }

        public ServiceResultDTO<List<RoleDTO>> GetList()
        {
            try
            {
                var data = _repo.GetRoles().Where(q=>q.id>1).ToList();
                return new ServiceResultDTO<List<RoleDTO>>(data: _mapper.Map<List<RoleDTO>>(data));
            }
            catch (Exception ex)
            {
                return new ServiceResultDTO<List<RoleDTO>>(exception: ex);
            }
        }

        public FormPostResultDTO<RoleDTO> AddOrUpdate(RoleDTO role)
        {
            try
            {
                var validate = new RoleDTOValidator().Validate(role);

                if (!validate.IsValid)
                    return new FormPostResultDTO<RoleDTO>(validate);

                var roleData = _mapper.Map<Role>(role);
                var _data = _repo.GetRoleCheck(roleData);
                if (_data != null && _data.id != 0)
                    return new FormPostResultDTO<RoleDTO>(false, errors: new List<string> { "This role is already registered." });



                if (role.id == 0)
                {
                    var insertResult = _repo.Add(roleData);
                }
                else
                {
                    var updateResult = _repo.Update(roleData);
                }

                return new FormPostResultDTO<RoleDTO>(data: role, title: "Successful", redirectUrl: "/PomeloAdmin/roles");
            }
            catch (Exception ex)
            {
                return new FormPostResultDTO<RoleDTO>(ex);
            }




        }

        public FormPostResultDTO<RoleDTO> Delete(RoleDTO role)
        {
            try
            {
                if (role.id == 0)
                    return new FormPostResultDTO<RoleDTO>(false, errors: new List<string> { "The role can't be empty" });

                var roleData = _mapper.Map<Role>(role);
                var _data = _repo.Delete(role.id);
                if (!_data)
                    return new FormPostResultDTO<RoleDTO>(false, errors: new List<string> { "The role could not be deleted." });
                 
                return new FormPostResultDTO<RoleDTO>(data: role, title: "Successful", redirectUrl: "/PomeloAdmin/roles");
            }
            catch (Exception ex)
            {
                return new FormPostResultDTO<RoleDTO>(ex);
            }




        }
    }
}
