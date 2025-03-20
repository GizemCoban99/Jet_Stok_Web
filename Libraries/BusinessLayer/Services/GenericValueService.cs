using AutoMapper;
using DataAccessLayer.Repos;
using EntityLayer.DTOs;
using EntityLayer.Models;

namespace BusinessLayer.Services
{
    public class GenericValueService
    {
        private readonly GenericValueRepo _repo = new GenericValueRepo();

        private readonly IMapper _mapper;
        public GenericValueService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ServiceResultDTO<List<GenericValueDTO>> GetList(int start, int end, string search, int type_id)
        {
            try
            {
                int count = 0;
                var data = _repo.GetList(start, end, search, type_id, out count);
                return new ServiceResultDTO<List<GenericValueDTO>>(data: _mapper.Map<List<GenericValueDTO>>(data), _total_rows: count);
            }
            catch (Exception ex)
            {
                return new ServiceResultDTO<List<GenericValueDTO>>(exception: ex);
            }
        }

        public ServiceResultDTO<List<GenericValueDTO>> GetListParent(int start, int end, string search, int parent_id)
        {
            try
            {
                int count = 0;
                var data = _repo.GetListParent(start, end, search, parent_id, out count);
                return new ServiceResultDTO<List<GenericValueDTO>>(data: _mapper.Map<List<GenericValueDTO>>(data), _total_rows: count);
            }
            catch (Exception ex)
            {
                return new ServiceResultDTO<List<GenericValueDTO>>(exception: ex);
            }
        }

        public ServiceResultDTO<List<GenericValueDTO>> GetAll(int start, int end, string search)
        {
            try
            {
                int count = 0;
                var data = _repo.GetAll(start, end, search, out count);
                return new ServiceResultDTO<List<GenericValueDTO>>(data: _mapper.Map<List<GenericValueDTO>>(data), _total_rows: count);
            }
            catch (Exception ex)
            {
                return new ServiceResultDTO<List<GenericValueDTO>>(exception: ex);
            }
        }
        public ServiceResultDTO<List<GenericValueDTO>> GetHome(List<int> types, List<int> ids)
        {
            try
            {
                int count = 0;
                var data = _repo.GetHome(types,ids);
                return new ServiceResultDTO<List<GenericValueDTO>>(data: _mapper.Map<List<GenericValueDTO>>(data), _total_rows: count);
            }
            catch (Exception ex)
            {
                return new ServiceResultDTO<List<GenericValueDTO>>(exception: ex);
            }
        }


        public ServiceResultDTO<GenericValueDTO> Get(int id)
        {
            try
            {
                var data = _repo.Get(id);
                return new ServiceResultDTO<GenericValueDTO>(data: _mapper.Map<GenericValueDTO>(data));
            }
            catch (Exception ex)
            {
                return new ServiceResultDTO<GenericValueDTO>(exception: ex);
            }
        }
        public ServiceResultDTO<GenericValueDTO> Get(string url)
        {
            try
            {
                var data = _repo.Get(url);
                return new ServiceResultDTO<GenericValueDTO>(data: _mapper.Map<GenericValueDTO>(data));
            }
            catch (Exception ex)
            {
                return new ServiceResultDTO<GenericValueDTO>(exception: ex);
            }
        }

        public ServiceResultDTO<GenericValueDTO> Delete(GenericValueDTO data)
        {
            try
            {
                var res = _repo.Delete(data);
                return new ServiceResultDTO<GenericValueDTO>(res);
            }
            catch (Exception ex)
            {
                return new ServiceResultDTO<GenericValueDTO>(exception: ex);
            }
        }
        public FormPostResultDTO<GenericValueDTO> AddOrUpdate(GenericValueDTO genericType)
        {
            try
            {
                var data = _mapper.Map<GenericValue>(genericType);

                if (genericType.id == 0)
                {
                    var insertResult = _repo.Add(data);
                }
                else
                {
                    var _data = _repo.Get(data.id);
                    if (_data == null || _data.id == 0)
                        return new FormPostResultDTO<GenericValueDTO>(false, errors: new List<string> { "The generic value was not found." });
                    else
                    {
                        var updateResult = _repo.Update(data);
                    }

                }

                return new FormPostResultDTO<GenericValueDTO>(data: genericType, title: "Successful", redirectUrl: "/PomeloAdmin/generic-values/" + genericType.type_id);
            }
            catch (Exception ex)
            {
                return new FormPostResultDTO<GenericValueDTO>(ex);
            }
        }

        public ServiceResultDTO<List<GenericValueDTO>> GetBlogCategoryList()
        {
            try
            {
                var data = _repo.GetBlogCategoryList();
                return new ServiceResultDTO<List<GenericValueDTO>>(data: _mapper.Map<List<GenericValueDTO>>(data));
            }
            catch (Exception ex)
            {
                return new ServiceResultDTO<List<GenericValueDTO>>(exception: ex);
            }
        }


        public ServiceResultDTO<GenericValueDTO> GetPackagesByJetstokId(int id)
        {
            try
            {
                var data = _repo.GetPackagesByJetstokId(id);
                return new ServiceResultDTO<GenericValueDTO>(data: _mapper.Map<GenericValueDTO>(data));
            }
            catch (Exception ex)
            {
                return new ServiceResultDTO<GenericValueDTO>(exception: ex);
            }
        }
    }
}
