using AutoMapper; 
using CoreLayer;
using DataAccessLayer.Repos;
using EntityLayer.DTOs;
using EntityLayer.DTOs.Request;
using EntityLayer.Models;
using static EntityLayer.DTOs.UserDTO;

namespace BusinessLayer.Services
{
    public class UserService 
    {
        private readonly UserRepo _repo = new UserRepo(); 

        private readonly IMapper _mapper;
        public UserService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ServiceResultDTO<List<UserDTO>> GetList()
        {
            try
            {
                var data = _repo.GetAllUsers();
                return new ServiceResultDTO<List<UserDTO>>(data: _mapper.Map<List<UserDTO>>(data));
            }
            catch (Exception ex)
            {
                return new ServiceResultDTO<List<UserDTO>>(exception: ex);
            }
        }


        public ServiceResultDTO<UserDTO> Update(User model)
        {
            try
            {
                var data = _repo.Update(model);
                return new ServiceResultDTO<UserDTO>(data: _mapper.Map<UserDTO>(data));
            }
            catch (Exception ex)
            {
                return new ServiceResultDTO<UserDTO>(exception: ex);
            }
        }

        public FormPostResultDTO<UserDTO> Delete(UserDTO model)
        {
            try
            {
                var data = _repo.Delete(_mapper.Map<User>(model));
                return new FormPostResultDTO<UserDTO>(isSuccess: data, redirectUrl: "/PomeloAdmin/users");
            }
            catch (Exception ex)
            {
                return new FormPostResultDTO<UserDTO>(ex);
            }
        }

        public ServiceResultDTO<UserDTO> Get(int id)
        {
            try
            {
                var data = _repo.GetUserById(id);
                return new ServiceResultDTO<UserDTO>(data: _mapper.Map<UserDTO>(data));
            }
            catch (Exception ex)
            {
                return new ServiceResultDTO<UserDTO>(exception: ex);
            }
        }

        public ServiceResultDTO<UserDTO> Login(LoginDTO user)
        {
            try
            {
                var userData = new User()
                {
                    email = user.Email,
                    password = Globals.DataEncription(user.Password, user.Email)
                };
                var data = _repo.Login(userData);
                if (data != null)
                {   
                    return new ServiceResultDTO<UserDTO>(data: _mapper.Map<UserDTO>(data));
                }

                return new ServiceResultDTO<UserDTO>(false, message: "The email address or password is incorrect. Please try again.");
            }
            catch (Exception ex)
            {
                return new ServiceResultDTO<UserDTO>(exception: ex);
            }
        }


        public FormPostResultDTO<UserDTO> AddOrUpdate(UserDTO user)
        {
            try
            {
                if (user.id == 0 && user.new_password == null)
                {
                    //şifre boşsa uyarı versin
                    user.new_password = "";
                }
                else
                {
                    user.new_password_check = user.new_password;
                }
                user.phone = string.IsNullOrEmpty(user.phone) ? user.phone : user.phone.Replace("(", "").Replace(")", "").Replace(" ", "");

                var validate = new UserDTOValidator().Validate(user);

                if (!validate.IsValid)
                    return new FormPostResultDTO<UserDTO>(validate);

                var userData = _mapper.Map<User>(user);


                if (user.id == 0)
                {
                    //Yeni kayıtta email kontrolü
                    var _data = _repo.HasUserByEmail(userData.email);
                    if (_data != null && _data.id != 0)
                        return new FormPostResultDTO<UserDTO>(false, errors: new List<string> { "This user is already registered." });
                }


                //eğer parola boş değilse yeni gelen parolayı şifrele
                if (!String.IsNullOrEmpty(user.new_password))
                    userData.password = Globals.DataEncription(user.new_password, user.email);


                if (user.id == 0)
                {

                    var insertResult = _repo.Add(userData);
                }
                else
                {
                    //kullanıcı gerçekten kayıtlı mı kontrolü
                    var _data = _repo.GetUserById(userData.id);
                    if (_data == null || _data.id == 0)
                        return new FormPostResultDTO<UserDTO>(false, errors: new List<string> { "The user was not found." });
                    else
                    {
                        _data.name = userData.name;
                        _data.surname = userData.surname;
                        _data.phone = userData.phone;
                        _data.tc_number = userData.tc_number;
                        _data.role_id = userData.role_id;
                        if (!string.IsNullOrEmpty(userData.password))
                        {
                            _data.password = userData.password;
                        }



                        var updateResult = _repo.Update(_data);
                    }

                }

                return new FormPostResultDTO<UserDTO>(data: user, title: "Successful", redirectUrl: "/PomeloAdmin/users");
            }
            catch (Exception ex)
            {
                return new FormPostResultDTO<UserDTO>(ex);
            }
        }
    }
}
