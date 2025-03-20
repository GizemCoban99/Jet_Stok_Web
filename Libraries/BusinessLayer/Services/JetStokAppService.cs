using AutoMapper;
using DataAccessLayer.Repos.Marketplaces;
using EntityLayer.DTOs.JetStokApp;
using EntityLayer.Models.JetStokApp;
using CoreLayer;
using EntityLayer.Models;
using EntityLayer.DTOs;

namespace BusinessLayer.Services
{
    public class JetStokAppService
    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly JetStokAppRepo _repo = new JetStokAppRepo();

        #endregion

        #region Ctor
        public JetStokAppService(IMapper mapper)
        {
            _mapper = mapper;
        }

        #endregion
        
        #region Methods
        public ServiceResultDTO<CompanyPackageDTO> AddJetstokPackage(long package_id, long company_id, string price)
        {
            try
            {
                var package = _repo.GetJetstokPackage(package_id);
                if (package != null)
                {
                    var data = _mapper.Map<CompanyPackage>(package);
                    data.company_id = company_id;
                    data.package_id = package.id;
                    data.end_date = DateTime.Now.AddYears(1);
                    data.is_sale = true;
                    data.sale_price = price.ConvertToDecimal();
                    _repo.AddJetstokPackage(data);
                }


                return new ServiceResultDTO<CompanyPackageDTO>();
            }
            catch (Exception ex)
            {
                return new ServiceResultDTO<CompanyPackageDTO>(exception: ex);
            }
        }

        public FormPostResultDTO<JetStokUserDTO> JetstokUserAdd(JetStokUserDTO user)
        {
            try
            {
                if (!string.IsNullOrEmpty(user.phone))
                    user.phone = user.phone.Trim().Replace("(", "").Replace(")", "").Replace(" ", "");

                var userData = _mapper.Map<JetStokUser>(user);

                if (!string.IsNullOrEmpty(user.password))
                    userData.password = Globals.DataEncription(user.password, user.email);

                if (userData.id == 0)
                {
                    userData.create_date = DateTime.Now;
                    var insertResult = _repo.AddJetstokUser(userData);
                    if (insertResult != null)
                        user.id = insertResult.id;
                }

                return new FormPostResultDTO<JetStokUserDTO>(data: user, title: "Başarılı", redirectUrl: "/users");
            }
            catch (Exception ex)
            {
                return new FormPostResultDTO<JetStokUserDTO>(ex);
            }
        }

        public FormPostResultDTO<CompanyDTO> AddJetstokCompany(CompanyDTO company)
        {
            try
            {
                if (!string.IsNullOrEmpty(company.phone))
                    company.phone = company.phone.Trim().Replace("(", "").Replace(")", "").Replace(" ", "");

                var companyData = _mapper.Map<JetStokCompany>(company);

                if (company.id == 0)
                {
                    companyData.approved = 0;
                    companyData.is_active = false;
                    var insertResult = _repo.AddJetstokCompany(companyData);
                    if (insertResult != null)
                        company.id = insertResult.id;
                    else
                        return new FormPostResultDTO<CompanyDTO>("Hata oluştu");
                }

                return new FormPostResultDTO<CompanyDTO>(data: company, title: "Başarılı", redirectUrl: "/company");
            }
            catch (Exception ex)
            {
                return new FormPostResultDTO<CompanyDTO>(ex);
            }
        }

        public ServiceResultDTO<JetStokUserDTO> GetUser(long id, long company_id)
        {
            try
            {
                var data = _repo.GetJetstokUserById(id, company_id);
                return new ServiceResultDTO<JetStokUserDTO>(data: _mapper.Map<JetStokUserDTO>(data));
            }
            catch (Exception ex)
            {
                return new ServiceResultDTO<JetStokUserDTO>(exception: ex);
            }
        }

        public ServiceResultDTO<JetStokUserDTO> GetUserByEmail(string email)
        {
            try
            {
                var data = _repo.GetJetstokUserByEmail(email);
                return new ServiceResultDTO<JetStokUserDTO>(data: _mapper.Map<JetStokUserDTO>(data));
            }
            catch (Exception ex)
            {
                return new ServiceResultDTO<JetStokUserDTO>(exception: ex);
            }
        }


        public ServiceResultDTO<JetStokUserDTO> GetUserByGuid(string guid)
        {
            try
            {
                var data = _repo.GetJetstokUserByGuid(guid);
                return new ServiceResultDTO<JetStokUserDTO>(data: _mapper.Map<JetStokUserDTO>(data));
            }
            catch (Exception ex)
            {
                return new ServiceResultDTO<JetStokUserDTO>(exception: ex);
            }
        }

        public ServiceResultDTO<CompanyPackageDTO> GetActivePackage(long company_id)
        {
            try
            {
                var data = _mapper.Map<CompanyPackageDTO>(_repo.GetActivePackage(company_id));

                return new ServiceResultDTO<CompanyPackageDTO>(data: data);
            }
            catch (Exception ex)
            {
                return new ServiceResultDTO<CompanyPackageDTO>(exception: ex);
            }
        }

        public ServiceResultDTO<PaymentLog> InsertPaymentRecord(PaymentLog model)
        {
            try
            {
                return new ServiceResultDTO<PaymentLog>(_repo.InsertPaymentRecord(model));
            }
            catch (Exception ex)
            {
                return new ServiceResultDTO<PaymentLog>(exception: ex);
            }
        }

        public bool UpdateCompanyApprovedActive(long id)
        {
            try
            {
                var data = _repo.UpdateCompanyApprovedActive(id);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        } 
        #endregion
    }
}
