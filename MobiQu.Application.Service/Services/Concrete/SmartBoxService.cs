using Microsoft.EntityFrameworkCore;
using MobiQu.Application.Service.Abstraction;
using MobiQu.Services.Application.Common.Enums;
using MobiQu.Services.Application.Common.Models.Responses;
using MobiQu.Services.Application.Common.Utilities;
using MobiQu.Services.Application.Dto;
using MobiQu.Services.Core.Domain.Entitites;
using MobiQu.Services.Core.Persistence.EntityFramework.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobiQu.Application.Service.Concrete
{
    public class SmartBoxService : ISmartBoxService
    {
        private readonly IRepository<SmartBox> _smartBoxRepository;
        public SmartBoxService(IRepository<SmartBox> smartBoxRepository)
        {
            _smartBoxRepository = smartBoxRepository;
        }

        public async Task<ResponseModel<SmartBoxDto>> GetSmartBoxByIdAsync(Guid boxId)
        {
            var smartBox = await _smartBoxRepository.FindAsync(x => x.Id == boxId);
            if (smartBox != null)
            {
                var responseModel = new ResponseModel<SmartBoxDto>
                {
                    ResponseValue = new SmartBoxDto
                    {
                        Id = smartBox.Id,
                        Title = smartBox.Title,
                        CreatedAtString = EntityUtilities<SmartBox>.DateTimeFormater(smartBox.CreatedAt),
                        ModifiedAtString = EntityUtilities<SmartBox>.DateTimeFormater(smartBox.ModifiedAt),
                    },
                    ResponseDateTime = DateTime.Now,
                    IsSuccessFull = true,
                    ResponseType = HttpResponseType.OK
                };
                return responseModel;
            }
            return new ResponseModel<SmartBoxDto>
            {
                ResponseType = HttpResponseType.NotFound,
                IsSuccessFull = false,
                ResponseMessage = $"{boxId} ile aradığınız veriyi bulamadık!",
            };
        }

        public async Task<ResponseModel<SmartBoxDto>> GetSmartBoxByNumberAsync(string boxNumber)
        {
            var smartBox = await _smartBoxRepository.FindAsync(x => x.Title.Equals(boxNumber));
            if (smartBox != null)
            {
                var responseModel = new ResponseModel<SmartBoxDto>
                {
                    ResponseValue = new SmartBoxDto
                    {
                        Id = smartBox.Id,
                        Title = smartBox.Title,
                        CreatedAtString = EntityUtilities<SmartBox>.DateTimeFormater(smartBox.CreatedAt),
                        ModifiedAtString = EntityUtilities<SmartBox>.DateTimeFormater(smartBox.ModifiedAt),
                    },
                    ResponseDateTime = DateTime.Now,
                    IsSuccessFull = true,
                    ResponseType = HttpResponseType.OK
                };
                return responseModel;
            }
            return new ResponseModel<SmartBoxDto>
            {
                ResponseType = HttpResponseType.NotFound,
                IsSuccessFull = false,
                ResponseMessage = $"{boxNumber} ile aradığınız veriyi bulamadık!",
            };
        }

        public async Task<ResponseModel<List<SmartBoxDto>>> GetSmartBoxesAsync(int skip, int take, Guid companyId)
        {
            var coldChainBoxes = await _smartBoxRepository.Queryable(x => x.CompanyId.Equals(companyId))
               .Skip(skip).Take(take)
               .Select(x => new SmartBoxDto
               {
                   Id = x.Id,
                   Title = x.Title,
                   CreatedAtString = EntityUtilities<SmartBox>.DateTimeFormater(x.CreatedAt),
                   ModifiedAtString = EntityUtilities<SmartBox>.DateTimeFormater(x.ModifiedAt),
               }).ToListAsync();
            if (coldChainBoxes != null)
            {
                return new ResponseModel<List<SmartBoxDto>>()
                {
                    ResponseValue = coldChainBoxes,
                    IsSuccessFull = true,
                    ResponseDateTime = DateTime.Now,
                    ResponseType = HttpResponseType.OK,
                    ResponseMessage = "Veriler başarıyla çekildi",
                    DataCount = await _smartBoxRepository.DataCountAsync(x => x.CompanyId.Equals(companyId))
                };
            }
            return null;
        }
    }
}
