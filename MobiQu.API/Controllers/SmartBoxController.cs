using Microsoft.AspNetCore.Mvc;
using MobiQu.Application.Service.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobiQu.API.Controllers
{
    public class SmartBoxController : Controller
    {
        private readonly ISmartBoxService _smartBoxService;
        public SmartBoxController(ISmartBoxService smartBoxService)
        {
            _smartBoxService = smartBoxService;
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
            var result = await _smartBoxService.GetSmartBoxesAsync(skip, pageSize, Guid.Parse("8BB239F1-530D-4E8A-943E-7D1248385F05"));

            if (result.IsSuccessFull)
            {
                return Ok(result);
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
        [HttpGet(ApiRoute.ApiRoute.SmartBox.GetSmartBoxDetailByNumber)]
        public async Task<IActionResult> GetSmartBoxDetailByNumber(string boxNumber)
        {
            var response = await _smartBoxService.GetSmartBoxByNumberAsync(boxNumber);
            if (response.IsSuccessFull)
            {
                return Ok(response);
            }else
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






    }
}
