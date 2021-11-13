using MobiQu.Services.Application.Common.Dto.MobiQuBranchTableSettings;
using MobiQu.Services.Application.Common.Models.Responses;
using MobiQu.Services.Application.Common.Utilities;
using MobiQu.Services.Application.Services.Abstraction;
using MobiQu.Services.Core.Domain.Entitites.Projects;
using MobiQu.Services.Core.Persistence.EntityFramework.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MobiQu.Services.Application.Services.Concrete
{
    public class MobiQuBranchTableSettingsService : IMobiQuBranchTableSettingsService 
    {
        private readonly IRepository<MobiQuBranchTableSettings> _tableSettings;
        public MobiQuBranchTableSettingsService(IRepository<MobiQuBranchTableSettings> tableSettings)
        {
            _tableSettings = tableSettings;
        }

        public async Task<ResponseModel<TableSettingsDto>> GetTableSettingsAsync(string API_KEY)
        {
            var companyId = Guid.Parse("8BB239F1-530D-4E8A-943E-7D1248385F05");
            Expression<Func<MobiQuBranchTableSettings, bool>> expression = x => x.CompanyId.Equals(companyId);
            var tableSettings = await _tableSettings.FindAsync(expression);
            if(tableSettings != null)
            {
                return new ResponseModel<TableSettingsDto>
                {
                    IsSuccessFull = true,
                    ResponseDateTime = DateTime.Now,
                    ResponseMessage ="Veriler Başarıyla Çekildi",
                    ResponseValue = new TableSettingsDto
                    {
                        
                        Id = tableSettings.Id,
                        Title = tableSettings.Title,
                        CreatedAtString = EntityUtilities<MobiQuBranchTableSettings>.DateTimeFormater(tableSettings.CreatedAt.Value),
                        ModifiedAtString = EntityUtilities<MobiQuBranchTableSettings>.DateTimeFormater(tableSettings.ModifiedAt.Value),
                    }
                };
            }
            return null;




        }
    }
}
