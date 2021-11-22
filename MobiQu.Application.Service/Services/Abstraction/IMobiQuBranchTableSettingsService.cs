using MobiQu.Services.Application.Common.Dto.MobiQuBranchTableSettings;
using MobiQu.Services.Application.Common.Models.BodyModels.Settings;
using MobiQu.Services.Application.Common.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobiQu.Services.Application.Services.Abstraction
{
    public interface IMobiQuBranchTableSettingsService
    {
        Task<ResponseModel<TableSettingsDto>> GetTableSettingsAsync(string API_KEY);
        Task<bool> UpdateTableSettingsAsync(UpdateTableSettingsModel tableSettings, string API_KEY);
    }
}
