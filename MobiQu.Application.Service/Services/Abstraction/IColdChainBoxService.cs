using MobiQu.Services.Application.Common.Dto.ColdChainBox;
using MobiQu.Services.Application.Common.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobiQu.Services.Application.Services.Abstraction
{
    public interface IColdChainBoxService
    {
        Task<ResponseModel<ColdChainBoxDto>> GetColdChainBoxByNumber(string boxNumber);

        Task<ResponseModel<ColdChainBoxDto>> GetColdChainBoxById(Guid boxId);

        Task<ResponseModel<List<ColdChainBoxDto>>> GetColdChainBoxesByCompanyId(Guid companyId, int skip, int take);
    }
}
