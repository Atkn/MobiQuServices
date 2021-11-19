using MobiQu.Services.Application.Common.Dto.MobiQuBranchTableSettings;
using MobiQu.Services.Application.Common.Models.Responses;
using MobiQu.Services.Application.Common.Utilities;
using MobiQu.Services.Application.Services.Abstraction;
using MobiQu.Services.Core.Domain.Entitites;
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
        private readonly IRepository<Company> _companyRepository;
        public MobiQuBranchTableSettingsService(IRepository<MobiQuBranchTableSettings> tableSettings, IRepository<Company> companyRepository)
        {
            _tableSettings = tableSettings;
            _companyRepository = companyRepository;
        }

        public async Task<ResponseModel<TableSettingsDto>> GetTableSettingsAsync(string API_KEY)
        {
            var company = await _companyRepository.FindAsync(x => x.API_KEY.Equals(API_KEY));
            if (company != null)
            {
                Expression<Func<MobiQuBranchTableSettings, bool>> expression = x => x.CompanyId.Equals(company.Id);
                var tableSettings = await _tableSettings.FindAsync(expression);
                if (tableSettings != null)
                {
                    return new ResponseModel<TableSettingsDto>
                    {
                        IsSuccessFull = true,
                        ResponseDateTime = DateTime.Now,
                        ResponseMessage = "Veriler Başarıyla Çekildi",
                        ResponseValue = new TableSettingsDto
                        {

                            Id = tableSettings.Id,
                            Title = tableSettings.Title,
                            IsShowBatteryState = tableSettings.IsShowBatteryState,
                            IsShowBoxNumber = tableSettings.IsShowBoxNumber,
                            IsShowDeliveryStatus = tableSettings.IsShowDeliveryStatus,
                            IsShowHumidity = tableSettings.IsShowHumidity,
                            IsShowLocation = tableSettings.IsShowLocation,
                            IsShowLockState = tableSettings.IsShowLockState,
                            IsShowShock = tableSettings.IsShowShock,
                            IsShowTemperature = tableSettings.IsShowTemperature,
                            CreatedAtString = EntityUtilities<MobiQuBranchTableSettings>.DateTimeFormater(tableSettings.CreatedAt),
                            ModifiedAtString = EntityUtilities<MobiQuBranchTableSettings>.DateTimeFormater(tableSettings.ModifiedAt),
                        }
                    };
                }
                return null;
            }
            return new ResponseModel<TableSettingsDto>
            {
                IsSuccessFull = false,
                ResponseDateTime = DateTime.Now,
                ResponseMessage = $"{API_KEY} anahtarınız doğru değildir.",
            };






        }
    }
}
