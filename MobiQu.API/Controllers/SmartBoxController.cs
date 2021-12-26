using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobiQu.Application.Service.Abstraction;
using MobiQu.Services.Application.Common.Models.Responses;
using MobiQu.Services.Application.Dto;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<SmartBoxDto>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<SmartBoxDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<SmartBoxDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<SmartBoxDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSmartBoxByDeviceId(Guid deviceId)
        {
            var response = await _smartBoxService.GetSmartBoxdByDeviceIdAsync(deviceId);
            if (response.IsSuccessFull)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        /// <summary>
        /// Şirkete bağlı akıllı kutu sayısını döndürür
        /// </summary>
        /// <param name="API_KEY"></param>
        /// <returns></returns>

        [HttpGet(ApiRoute.ApiRoute.SmartBox.GetSmartBoxCountCompany)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<SmartBoxDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSmartBoxCountCompany(string API_KEY)
        {
            var result = await _smartBoxService.GetSmartBoxCountCompanyAsync(API_KEY);
            if (result.IsSuccessFull)
            {
                return Ok(result);
            }
            return BadRequest();
        }


        [HttpPost(ApiRoute.ApiRoute.SmartBox.SmartBoxUnLock)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
        public async Task<IActionResult> UnLockBox(int lockState, string deviceNumber)
        {
            if (lockState == 1)
            {
                var result = await _smartBoxService.SmartBoxUnLockAsyncByDeviceNumber(lockState, deviceNumber);
                return Ok(result);
            }
            return Ok(new
            {
                message = "Kutu Açılması İçin Yanlış Değer Girildi",
                Result = false,
            });


        }









    }
}
