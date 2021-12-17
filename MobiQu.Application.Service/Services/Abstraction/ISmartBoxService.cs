using MobiQu.Services.Application.Common.Dto.Device;
using MobiQu.Services.Application.Common.Models.Responses;
using MobiQu.Services.Application.Dto;
using MobiQu.Services.Core.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobiQu.Application.Service.Abstraction
{
    public interface ISmartBoxService
    {
        Task<ResponseModel<List<SmartBoxDto>>> GetSmartBoxesAsync(int skip, int take, Guid companyId);

        Task<ResponseModel<SmartBoxDto>> GetSmartBoxByNumberAsync(string boxNumber);

        Task<ResponseModel<SmartBoxDto>> GetSmartBoxByIdAsync(Guid boxId);
        Task<ResponseModel<SmartBoxDto>> GetSmartBoxdByDeviceIdAsync(Guid deviceId);

        Task<ResponseModel<DeviceDto>> GetDeviceInfoByDeviceNumberAsync(string deviceNumber);

        Task<ResponseModel<DeviceDto>> GetDeviceInfoByDeviceIdAsync(Guid deviceId);

        Task<ResponseModel<SmartBoxDto>> GetSmartBoxInformationByDeviceId(Guid deviceId);

        Task<ResponseModel<SmartBoxDto>> GetSmartBoxCountCompanyAsync(string API_KEY);

    }
}
