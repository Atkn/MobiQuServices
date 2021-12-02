using Microsoft.AspNetCore.Mvc;
using MobiQu.Application.Service.Abstraction;
using MobiQu.Services.Application.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobiQu.API.Controllers
{
    public class SmartBoxController : Controller
    {
        private readonly ISmartBoxService _smartBoxService;
        private readonly ICompanyService _companyService;
        public SmartBoxController(ISmartBoxService smartBoxService, ICompanyService companyService)
        {
            _smartBoxService = smartBoxService;
            _companyService = companyService;
        }

        /// <summary>
        /// API_KEY'e göre istek atılan kullanıcıların verilerini getirir
        /// </summary>
        /// <remarks>
        /// More elaborate description
        /// </remarks>
        /// <param name="API_KEY">Kullanıcıya verilen api_key'i ile sorgulama atılmalıdır.</param>
        /// <param name="pageNumber">Kaçıncı Sayfadaki Veri.</param>
        /// <param name="pageSize">Sayfada Kaç Veri Gösterilecek.</param>
        [HttpGet(ApiRoute.ApiRoute.SmartBox.GetSmartBoxes)]
        public async Task<IActionResult> Index(string API_KEY, int skip, int pageSize)
        {
            var company = await _companyService.GetCompanyInfomartionByApiKeyAsync(API_KEY);
            if (company != null)
            {
                var result = await _smartBoxService.GetSmartBoxesAsync(skip, pageSize, company.ResponseValue.Id);
                return Ok(result);

            }
            else
            {
                return Ok(new { message = "Anahatarınız Doğrulanamadı" });
            }




        }

        /// <summary>
        /// Kutu Numarasına göre veri getirir
        /// </summary>
        /// <param name="boxNumber"></param>
        /// <returns></returns>
        [HttpGet(ApiRoute.ApiRoute.SmartBox.GetSmartBoxDetailByNumber)]
        public async Task<IActionResult> GetSmartBoxDetailByNumber(string boxNumber)
        {
            var response = await _smartBoxService.GetSmartBoxByNumberAsync(boxNumber);
            if (response.IsSuccessFull)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }


        /// <summary>
        /// Kutu Numarasına göre veri getirir
        /// </summary>
        /// <param name="boxNumber"></param>
        /// <returns></returns>
        [HttpGet(ApiRoute.ApiRoute.SmartBox.GetSmartBoxDetailById)]
        public async Task<IActionResult> GetSmartBoxDetailById(Guid boxId)
        {
            var response = await _smartBoxService.GetSmartBoxByIdAsync(boxId);
            if (response.IsSuccessFull)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }

        /// <summary>
        /// Cihaz Id bilgisine göre veri getirir
        /// </summary>
        /// <param name="boxNumber"></param>
        /// <returns></returns>
        [HttpGet(ApiRoute.ApiRoute.SmartBox.GetSmartBoxDetailByDeviceId)]
        public async Task<IActionResult> GetSmartBoxByDeviceId(Guid deviceId)
        {
            var response = await _smartBoxService.GetSmartBoxdByDeviceIdAsync(deviceId);
            if (response.IsSuccessFull)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        
        







    }
}
