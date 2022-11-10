using Microsoft.EntityFrameworkCore;
using MobiQu.Application.Service.Abstraction;
using MobiQu.Services.Application.Common.Dto.Device;
using MobiQu.Services.Application.Common.Dto.SmartBox;
using MobiQu.Services.Application.Common.Enums;
using MobiQu.Services.Application.Common.Models.Responses;
using MobiQu.Services.Application.Common.Utilities;
using MobiQu.Services.Application.Dto;
using MobiQu.Services.Core.Domain.Entitites;
using MobiQu.Services.Core.Domain.Entitites.Projects;
using MobiQu.Services.Core.Persistence.EntityFramework.Repository.Abstraction;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using Newtonsoft.Json;
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
        private readonly IRepository<Company> _companyRepository;
        private static IMqttClient _client;
        private static IMqttClientOptions _options;
        public SmartBoxService(IRepository<SmartBox> smartBoxRepository, IRepository<Device> deviceRepository, IRepository<Company> companyRepository)
        {
            _deviceRepository = deviceRepository;
            _smartBoxRepository = smartBoxRepository;
            _companyRepository = companyRepository;
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
                        MaxTemperature = string.IsNullOrEmpty(smartBox.MaxTemperature) ? "Bu Kutu İçin Tavan Sıcaklık Değeri Belirtilmedi" : smartBox.MaxTemperature,
                        MinTemperature = string.IsNullOrEmpty(smartBox.MinTemperature) ? "Bu Kutu İçin Taban Sıcaklık Değeri Belirtilmedi" : smartBox.MinTemperature,
                        MinMoisture = string.IsNullOrEmpty(smartBox.MinMoisture) ? "Bu Kutu İçin Taban Nem Değeri Belirtilmedi" : smartBox.MinMoisture,
                        MaxMoisture = string.IsNullOrEmpty(smartBox.MaxMoisture) ? "Bu Kutu İçin Tavan Nem Değeri Belirtilmedi" : smartBox.MaxMoisture,
                        MaxMoistureValue = smartBox.MaxMoistureValue,
                        MaxTemperatureValue = smartBox.MaxTemperatureValue,
                        MinMoistureValue = smartBox.MinMoistureValue,
                        MinTemperatureValue = smartBox.MinTemperatureValue,
                        CompanyId = smartBox.CompanyId
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
                IsSuccessFull = true,
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
                        MaxTemperature = string.IsNullOrEmpty(smartBox.MaxTemperature) ? "Bu Kutu İçin Tavan Sıcaklık Değeri Belirtilmedi" : smartBox.MaxTemperature,
                        MinTemperature = string.IsNullOrEmpty(smartBox.MinTemperature) ? "Bu Kutu İçin Taban Sıcaklık Değeri Belirtilmedi" : smartBox.MinTemperature,
                        MinMoisture = string.IsNullOrEmpty(smartBox.MinMoisture) ? "Bu Kutu İçin Taban Nem Değeri Belirtilmedi" : smartBox.MinMoisture,
                        MaxMoisture = string.IsNullOrEmpty(smartBox.MaxMoisture) ? "Bu Kutu İçin Tavan Nem Değeri Belirtilmedi" : smartBox.MaxMoisture,
                        MaxMoistureValue = smartBox.MaxMoistureValue,
                        MaxTemperatureValue = smartBox.MaxTemperatureValue,
                        MinMoistureValue = smartBox.MinMoistureValue,
                        MinTemperatureValue = smartBox.MinTemperatureValue,
                        CompanyId = smartBox.CompanyId
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

        public async Task<ResponseModel<SmartBoxDto>> GetSmartBoxCountCompanyAsync(string API_KEY)
        {
            var company = await _companyRepository.FindAsync(x => x.API_KEY.Equals(API_KEY));
            if (company != null)
            {
                var smartBoxCount = _smartBoxRepository.DataCount(x => x.CompanyId.Equals(company.Id));
                return new ResponseModel<SmartBoxDto>
                {
                    IsSuccessFull = true,
                    DataCount = smartBoxCount,
                    ResponseValue = null,
                    ResponseDateTime = DateTime.Now,
                    ResponseMessage = $"{company.Title} akıllı kutu sayısı {smartBoxCount}"
                };

            }
            return new ResponseModel<SmartBoxDto>
            {
                IsSuccessFull = true,
                ResponseMessage = $"{API_KEY} ile ilgili bir şirket bulunamadı",

            };


        }

        public async Task<SmartBoxLockStateModel> SmartBoxUnLockAsyncByDeviceNumber(int lockState, string deviceNumber)
        {
            bool connectedResult = false;
            string connectedStringMessage = "Başarıyla Gönderildi";
            object unlock = new { unlock = 1 };
            string jsonString = JsonConvert.SerializeObject(unlock);
            try
            {
                var factory = new MqttFactory();
                _client = factory.CreateMqttClient();
                _options = new MqttClientOptionsBuilder()
                    .WithClientId("easymqtt")
                    .WithTcpServer("91.194.54.44", 1883)
                    .WithCredentials("", "")
                    .WithCleanSession()
                    .Build();

                //handlers
                _client.UseConnectedHandler(e =>
                {
                    connectedResult = true;
                });
                _client.UseDisconnectedHandler(e =>
                {
                    connectedStringMessage = "Bağlantı Kesildi";
                    connectedResult = false;
                });
                _client.UseApplicationMessageReceivedHandler(e =>
                {
                    try
                    {
                        string topic = e.ApplicationMessage.Topic;
                        if (string.IsNullOrWhiteSpace(topic) == false)
                        {
                            string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message, ex);
                    }
                });
                _client.ConnectAsync(_options).Wait();
                var testMessage = new MqttApplicationMessageBuilder()
                    .WithTopic("0005/UNLOCK")
                    .WithPayload(jsonString)
                    .WithExactlyOnceQoS()
                    .WithRetainFlag()
                    .Build();
                if (_client.IsConnected)
                {
                    await _client.PublishAsync(testMessage);
                }
                return new SmartBoxLockStateModel
                {
                    IsSuccessFull = true,
                    Message = "Başaryıla Mesaj Gönderildi"
                };



            }
            catch (Exception ex)
            {
                return new SmartBoxLockStateModel
                {
                    IsSuccessFull = false,
                    Message = "Gönderilemedi" + ex.Message
                };

            }

        }
    }
}



