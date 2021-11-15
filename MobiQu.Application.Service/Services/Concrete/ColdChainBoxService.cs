using Microsoft.EntityFrameworkCore;
using MobiQu.Services.Application.Common.Dto.ColdChainBox;
using MobiQu.Services.Application.Common.Models.Responses;
using MobiQu.Services.Application.Common.Utilities;
using MobiQu.Services.Application.Services.Abstraction;
using MobiQu.Services.Core.Domain.Entitites.Projects;
using MobiQu.Services.Core.Persistence.EntityFramework.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobiQu.Services.Application.Services.Concrete
{
    public class ColdChainBoxService : IColdChainBoxService
    {
        private readonly IRepository<ColdChainBox> _coldChainBoxRepository;
        public ColdChainBoxService(IRepository<ColdChainBox> coldChainBoxRepository)
        {
            _coldChainBoxRepository = coldChainBoxRepository;
        }

        public async Task<ResponseModel<ColdChainBoxDto>> GetColdChainBoxByNumber(string boxNumber)
        {
            var coldChainBox = await _coldChainBoxRepository.FindAsync(x => x.Title.Equals(boxNumber));
            if (coldChainBox != null)
            {
                var responseModel = new ResponseModel<ColdChainBoxDto>
                {
                    IsSuccessFull = true,
                    ResponseDateTime = DateTime.Now,
                    ResponseMessage = $"{coldChainBox.Title} Başarıyla bulundu",
                    ResponseValue = new ColdChainBoxDto
                    {
                        Id = coldChainBox.Id,
                        Title = coldChainBox.Title,
                        CreatedAtString = EntityUtilities<ColdChainBox>.DateTimeFormater(coldChainBox.CreatedAt),
                        ModifiedAtString = EntityUtilities<ColdChainBox>.DateTimeFormater(coldChainBox.ModifiedAt)
                    },
                    ResponseType = Common.Enums.HttpResponseType.OK,
                    
                };
                return responseModel;
            }
            return null;
        }


        public async Task<ResponseModel<ColdChainBoxDto>> GetColdChainBoxById(Guid boxId)
        {
            var coldChainBox = await _coldChainBoxRepository.FindAsync(x => x.Id.Equals(boxId));
            if (coldChainBox != null)
            {
                var responseModel = new ResponseModel<ColdChainBoxDto>
                {
                    IsSuccessFull = true,
                    ResponseDateTime = DateTime.Now,
                    ResponseMessage = $"{coldChainBox.Title} Başarıyla bulundu",
                    ResponseValue = new ColdChainBoxDto
                    {
                        Id = coldChainBox.Id,
                        Title = coldChainBox.Title,
                        CreatedAtString = EntityUtilities<ColdChainBox>.DateTimeFormater(coldChainBox.CreatedAt),
                        ModifiedAtString = EntityUtilities<ColdChainBox>.DateTimeFormater(coldChainBox.ModifiedAt)
                    },
                    ResponseType = Common.Enums.HttpResponseType.OK
                };
                return responseModel;
            }
            return null;
        }

        public async Task<ResponseModel<List<ColdChainBoxDto>>> GetColdChainBoxesByCompanyIdAsync(Guid companyId, int skip, int take)
        {

            var coldChainBoxes = await _coldChainBoxRepository.Queryable(x => x.CompanyId.Equals(companyId))
                .Skip(skip).Take(take)
                .Select(x => new ColdChainBoxDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    CreatedAtString = EntityUtilities<ColdChainBox>.DateTimeFormater(x.CreatedAt),
                    ModifiedAtString = EntityUtilities<ColdChainBox>.DateTimeFormater(x.ModifiedAt)
                }).ToListAsync();
            if (coldChainBoxes != null)
            {
                return new ResponseModel<List<ColdChainBoxDto>>()
                {
                    ResponseValue = coldChainBoxes,
                    IsSuccessFull = true,
                    ResponseDateTime = DateTime.Now,
                    ResponseType = Common.Enums.HttpResponseType.OK,
                    ResponseMessage = "Veriler başarıyla getirildi",
                    DataCount = await _coldChainBoxRepository.DataCountAsync(x => x.CompanyId.Equals(companyId))
                };

            }
            return null;
        }

        
    }
}
