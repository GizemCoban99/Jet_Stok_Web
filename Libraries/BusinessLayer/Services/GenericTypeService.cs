using AutoMapper; 
using DataAccessLayer.Repos;
using EntityLayer.DTOs;
using EntityLayer.Models; 

namespace BusinessLayer.Services
{
    public class GenericTypeService
    {
        private readonly GenericTypeRepo _repo = new GenericTypeRepo(); 

        private readonly IMapper _mapper;
        public GenericTypeService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ServiceResultDTO<List<GenericTypeDTO>> GetList()
        {
            try
            {
                var data = _repo.GetList();
                return new ServiceResultDTO<List<GenericTypeDTO>>(data: _mapper.Map<List<GenericTypeDTO>>(data));
            }
            catch (Exception ex)
            {
                return new ServiceResultDTO<List<GenericTypeDTO>>(exception: ex);
            }
        }

 
        public ServiceResultDTO<GenericTypeDTO> Get(int id)
        {
            try
            {
                var data = _repo.Get(id);
                return new ServiceResultDTO<GenericTypeDTO>(data: _mapper.Map<GenericTypeDTO>(data));
            }
            catch (Exception ex)
            {
                return new ServiceResultDTO<GenericTypeDTO>(exception: ex);
            }
        } 

        public FormPostResultDTO<GenericTypeDTO> AddOrUpdate(GenericTypeDTO genericType)
        {
            try
            { 
                var data = _mapper.Map<GenericType>(genericType);
                 
                if (genericType.id == 0)
                { 
                    var insertResult = _repo.Add(data);
                }
                else
                { 
                    var _data = _repo.Get(data.id);
                    if (_data == null || _data.id == 0)
                        return new FormPostResultDTO<GenericTypeDTO>(false, errors: new List<string> { "The generic type was not found." });
                    else
                    { 
                        var updateResult = _repo.Update(data);
                    }

                }

                return new FormPostResultDTO<GenericTypeDTO>(data: genericType, title: "Successful", redirectUrl: "/PomeloAdmin/generic-types");
            }
            catch (Exception ex)
            {
                return new FormPostResultDTO<GenericTypeDTO>(ex);
            }
        }
    }
}
