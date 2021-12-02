using Microsoft.EntityFrameworkCore;
using MobiQu.Application.Service.Abstraction;
using MobiQu.Services.Application.Common.Dto.Device;
using MobiQu.Services.Application.Common.Enums;
using MobiQu.Services.Application.Common.Models.Responses;
using MobiQu.Services.Application.Common.Utilities;
using MobiQu.Services.Application.Dto;
using MobiQu.Services.Core.Domain.Entitites;
using MobiQu.Services.Core.Domain.Entitites.Projects;
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
        private readonly IRepository<Device> _deviceRepository;
        public SmartBoxService(IRepository<SmartBox> smartBoxRepository, IRepository<Device> deviceRepository)
        {
            _deviceRepository = deviceRepository;
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
        public async Task<ResponseModel<SmartBoxDto>> GetSmartBoxdByDeviceIdAsync(Guid deviceId)
        {
            var smartBox = await _smartBoxRepository.FindAsync(x => x.DeviceId.Equals(deviceId));
            if (smartBox != null)
            {
                return new ResponseModel<SmartBoxDto>
                {
                    IsSuccessFull = true,
                    ResponseDateTime = DateTime.Now,
                    ResponseValue = new SmartBoxDto
                    {
                        Id = smartBox.Id,
                        Title = smartBox.Title,
                        CreatedAtString = EntityUtilities<SmartBox>.DateTimeFormater(smartBox.CreatedAt),
                        ModifiedAtString = EntityUtilities<SmartBox>.DateTimeFormater(smartBox.ModifiedAt),
                    }
                };
            }
            return new ResponseModel<SmartBoxDto>
            {
                ResponseMessage = $"{deviceId} ile aratılan veri bulunamadı",
                ResponseType = HttpResponseType.NotFound,
                IsSuccessFull = true
            };
        }

        public async Task<ResponseModel<DeviceDto>> GetDeviceInfoByDeviceNumberAsync(string deviceNumber)
        {
            var device = await _deviceRepository.FindAsync(x => x.DeviceNumber.Equals(deviceNumber));
            if (device != null)
            {
                return new ResponseModel<DeviceDto>
                {
                    IsSuccessFull = true,
                    ResponseDateTime = DateTime.Now,
                    ResponseMessage = "Başarıyla Veri Bulundu",
                    ResponseValue = new DeviceDto
                    {
                        Id = device.Id,
                        //SmartBoxId = device.SmartBoxId,
                        Title = device.DeviceNumber,
                        CreatedAtString = EntityUtilities<SmartBox>.DateTimeFormater(device.CreatedAt),
                        ModifiedAtString = EntityUtilities<SmartBox>.DateTimeFormater(device.ModifiedAt),


                    }
                };
            }
            return new ResponseModel<DeviceDto>
            {
                ResponseMessage = $"{deviceNumber} ile aradığınız veriyi bulamadık",
                ResponseDateTime = DateTime.Now,
                IsSuccessFull = true
            };
        }


        public async Task<ResponseModel<DeviceDto>> GetDeviceInfoByDeviceIdAsync(Guid deviceId)
        {
            var device = await _deviceRepository
                .Queryable(x => x.Id.Equals(deviceId))
                .Include(x => x.SmartBox)
                .FirstOrDefaultAsync();
            if (device != null)
            {
                var response = new ResponseModel<DeviceDto>
                {
                    IsSuccessFull = true,
                    ResponseDateTime = DateTime.Now,
                    ResponseMessage = "Başarıyla Veri Bulundu",
                    ResponseValue = new DeviceDto
                    {
                        Id = device.Id,
                        //SmartBoxId = device.SmartBoxId,
                        Title = device.DeviceNumber,
                        CreatedAtString = EntityUtilities<SmartBox>.DateTimeFormater(device.CreatedAt),
                        ModifiedAtString = EntityUtilities<SmartBox>.DateTimeFormater(device.ModifiedAt),
                        SmartBox = device.SmartBox != null ? new SmartBoxDto
                        {
                            Id = device.SmartBox.Id,
                            Title = device.SmartBox.Title,
                            CreatedAtString = EntityUtilities<SmartBox>.DateTimeFormater(device.SmartBox.CreatedAt),

                        } : null
                    }
                };
                return response;
            }
            return new ResponseModel<DeviceDto>
            {
                ResponseMessage = $"{deviceId} ile aradığınız veriyi bulamadık",
                ResponseDateTime = DateTime.Now,
                IsSuccessFull = true
            };
        }


        public async Task<ResponseModel<SmartBoxDto>> GetSmartBoxInformationByDeviceId(Guid deviceId)
        {
            var smartBox = await _smartBoxRepository.Queryable(x => x.DeviceId.Equals(deviceId)).Include(x => x.Device).FirstOrDefaultAsync();
            if (smartBox != null)
            {
                var response = new ResponseModel<SmartBoxDto>
                {
                    IsSuccessFull = true,
                    ResponseValue = new SmartBoxDto
                    {
                        Id = smartBox.Id,
                        Title = smartBox.Title,
                    }
                };
            }
            return null;
        }



    }
}




