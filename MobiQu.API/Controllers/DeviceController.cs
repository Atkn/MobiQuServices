using Microsoft.AspNetCore.Mvc;
using MobiQu.Application.Service.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobiQu.API.Controllers
{
    public class DeviceController : Controller
    {
        private readonly ISmartBoxService _smartBoxService;
        public DeviceController(ISmartBoxService smartBoxService)
        {
            _smartBoxService = smartBoxService;
        }
        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Aygıt Numarasına göre verileri getirir
        /// </summary>
        /// <param name="deviceNumber"></param>
        /// <returns></returns>

        [HttpGet(ApiRoute.ApiRoute.Device.GetDeviceInformationByDeviceNumber)]
        public async Task<IActionResult> GetDeviceInformationByDeviceNumber(string deviceNumber)
        {
            var result = await _smartBoxService.GetDeviceInfoByDeviceNumberAsync(deviceNumber);
            if (result.IsSuccessFull)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpGet(ApiRoute.ApiRoute.Device.GetDeviceInformationByDeviceId)]
        public async Task<IActionResult> GetDeviceInformationByDeviceId(Guid deviceId)
        {
            var result = await _smartBoxService.GetDeviceInfoByDeviceIdAsync(deviceId);
            if (result.IsSuccessFull)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}
