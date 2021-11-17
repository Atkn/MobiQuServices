using MobiQu.Services.Application.Common.Dto.Company;
using MobiQu.Services.Application.Common.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobiQu.Services.Application.Services.Abstraction
{
    public interface ICompanyService
    {
        Task<ResponseModel<CompanyDto>> GetCompanyInformationAsync(Guid companyId);

        Task<ResponseModel<CompanyDto>> GetCompanyInfomartionByApiKeyAsync(string apiKey);

        Task<ResponseModel<LoginResponseDto>> CompanyDetectInformationAsync(string email, string password);
    }
}
