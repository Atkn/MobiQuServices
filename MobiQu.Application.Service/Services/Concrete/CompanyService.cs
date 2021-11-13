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
        public CompanyService(IRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;
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
    }
}
