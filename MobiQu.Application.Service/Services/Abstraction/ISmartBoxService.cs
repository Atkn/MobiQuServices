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
    }
}
