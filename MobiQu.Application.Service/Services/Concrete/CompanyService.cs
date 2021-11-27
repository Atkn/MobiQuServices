using MobiQu.Services.Application.Application.Password;
using MobiQu.Services.Application.Common.Dto.Company;
using MobiQu.Services.Application.Common.Models.Responses;
using MobiQu.Services.Application.Common.Utilities;
using MobiQu.Services.Application.Services.Abstraction;
using MobiQu.Services.Core.Domain.Entitites;
using MobiQu.Services.Core.Persistence.EntityFramework.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MobiQu.Services.Application.Services.Concrete
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepository<Company> _companyRepository;
        private readonly IPasswordCryptology _passwordCryptology;
        public CompanyService(IRepository<Company> companyRepository, IPasswordCryptology passwordCryptology)
        {
            _companyRepository = companyRepository;
            _passwordCryptology = passwordCryptology;
        }

        public async Task<ResponseModel<CompanyDto>> GetCompanyInformationAsync(Guid companyId)
        {
            Expression<Func<Company, bool>> predicate = x => x.Id.Equals(companyId);
            var company = await _companyRepository.FindAsync(predicate);
            if (company != null)
            {
                return new ResponseModel<CompanyDto>
                {
                    IsSuccessFull = true,
                    ResponseDateTime = DateTime.Now,
                    ResponseMessage = $"{company.Title} Başarıyla bulundu",
                    ResponseValue = new
                    CompanyDto
                    {
                        Id = company.Id,
                        Title = company.Title,
                        Email = company.Email,
                        Api_Key = company.API_KEY,
                        CreatedAtString = EntityUtilities<Company>.DateTimeFormater(company.CreatedAt.Value),
                        ModifiedAtString = EntityUtilities<Company>.DateTimeFormater(company.ModifiedAt.Value),

                    }
                };
            }
            return null;
        }

        public async Task<ResponseModel<CompanyDto>> GetCompanyInfomartionByApiKeyAsync(string apiKey)
        {
            Expression<Func<Company, bool>> expression = x => x.API_KEY.Equals(apiKey);
            var company = await _companyRepository.FindAsync(expression);
            if (company != null)
            {
                return new ResponseModel<CompanyDto>
                {
                    IsSuccessFull = true,
                    ResponseDateTime = DateTime.Now,
                    ResponseMessage = $"{company.Title} Başarıyla Bulundu",
                    ResponseValue = new CompanyDto
                    {
                        Id = company.Id,
                        Title = company.Title,
                        CreatedAtString = EntityUtilities<Company>.DateTimeFormater(company.CreatedAt.Value),
                        ModifiedAtString = EntityUtilities<Company>.DateTimeFormater(company.ModifiedAt.Value),
                    }
                };
            }
            return null;
        }


        public async Task<ResponseModel<RefreshPasswordDto>> UpdatePasswordAsync(string apiKey, string oldPassword, string newPassword, string confirmPassword)
        {
            
            bool isEqualsPassword = newPassword.Equals(confirmPassword);
            if (isEqualsPassword)
            {
                var company = await _companyRepository.FindAsync(x => x.API_KEY.Equals(apiKey));
                if (company != null)
                {
                    string hashPass = company.Password;
                    bool verifyPass = _passwordCryptology.VerifiedPassword(oldPassword, hashPass);
                    if (verifyPass)
                    {
                        newPassword = _passwordCryptology.HashPassword(newPassword);
                        company.Password = newPassword;
                        company.ModifiedAt = DateTime.Now;
                        var result = await _companyRepository.UpdateEntity(company);
                        if (result)
                        {
                            return new ResponseModel<RefreshPasswordDto>
                            {
                                IsSuccessFull = true,
                                ResponseDateTime = DateTime.Now,
                                ResponseValue = new RefreshPasswordDto
                                {
                                    IsSuccessFull = false,
                                    Message = $"Şifreniz Başarıyla Güncellendi!"
                                }
                            };
                        }
                    }
                    return new ResponseModel<RefreshPasswordDto>
                    {
                        IsSuccessFull = false,
                        ResponseDateTime = DateTime.Now,
                        ResponseValue = new RefreshPasswordDto
                        {
                            IsSuccessFull = false,
                            Message = $"Eski Şifreniz Doğrulanamadı!"
                        }
                    };

                }
                return new ResponseModel<RefreshPasswordDto>
                {
                    IsSuccessFull = false,
                    ResponseDateTime = DateTime.Now,
                    ResponseValue = new RefreshPasswordDto
                    {
                        IsSuccessFull = false,
                        Message = $"{apiKey} - Kullanıcı Bulunamadı!"
                    }
                };
            }
            return new ResponseModel<RefreshPasswordDto>
            {
                IsSuccessFull = false,
                ResponseDateTime = DateTime.Now,
                ResponseValue = new RefreshPasswordDto
                {
                    IsSuccessFull = false,
                    Message = $"Şifreler Eşleşmediği İçin İşleme Devam Edilmedi!"
                }
            };
        }

        public async Task<ResponseModel<LoginResponseDto>> CompanyDetectInformationAsync(string email, string password)
        {
            Expression<Func<Company, bool>> expression = x => x.Email.Equals(email);
            var companyResponse = await _companyRepository.FindAsync(expression);
            if (companyResponse != null)
            {
                string passwordHash = companyResponse.Password;
                bool verifyPassword = _passwordCryptology.VerifiedPassword(password, passwordHash);
                if (verifyPassword)
                {
                    return new ResponseModel<LoginResponseDto>
                    {
                        IsSuccessFull = true,
                        ResponseDateTime = DateTime.Now,
                        ResponseMessage = $"{companyResponse.Title} Başarıyla giriş sağlayabilir.",
                        ResponseValue = new LoginResponseDto
                        {
                            Id = companyResponse.Id,
                            CanLogin = true,
                            Title = companyResponse.Title,
                            API_KEY = companyResponse.API_KEY,
                            Email = companyResponse.Email,
                        }
                    };
                }
                return new ResponseModel<LoginResponseDto>
                {
                    IsSuccessFull = false,
                    ResponseDateTime = DateTime.Now,
                    ResponseMessage = $"{email} giriş sağlayamaz.",
                    ResponseValue = new LoginResponseDto
                    {
                        CanLogin = false,
                    }
                };

            }

            return new ResponseModel<LoginResponseDto>
            {
                IsSuccessFull = false,
                ResponseDateTime = DateTime.Now,
                ResponseMessage = $"{email} giriş sağlayamaz.",
                ResponseValue = new LoginResponseDto
                {
                    CanLogin = false,
                }
            };
        }
    }
}
