using Microsoft.AspNetCore.Mvc;
using MobiQu.Services.Application.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobiQu.API.Controllers
{
    public class ColdChainBoxController : Controller
    {

        private IColdChainBoxService _coldChainBoxService;
        public ColdChainBoxController(IColdChainBoxService coldChainBoxService)
        {
            _coldChainBoxService = coldChainBoxService;
        }
        public IActionResult Index()
        {
            return View();
        }



        /// <summary>
        /// Müşteriye ait kutuları getirmeye yarayan fonksiyon
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoute.ApiRoute.ColdChainBox.GetColdChainBoxes)]
        public async Task<IActionResult> GetColdChainBoxes(string API_KEY, int skip, int pageSize)
        {
            var companyId = Guid.Parse("8BB239F1-530D-4E8A-943E-7D1248385F05");

            var response = await _coldChainBoxService.GetColdChainBoxesByCompanyIdAsync(companyId, skip, pageSize);
            if(response != null)
            {
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Kutu numarasıyla kutuyu bulmaya yarayan fonksiyon
        /// <param name="boxNumber">Kutu Numarası</param>
        /// </summary>
        [HttpGet(ApiRoute.ApiRoute.ColdChainBox.GetColdChainBoxDetailByNumber)]
        public async Task<IActionResult> GetColdChainBoxByNumber(string boxNumber)
        {
            var response = await _coldChainBoxService.GetColdChainBoxByNumber(boxNumber);
            if(response != null)
            {
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
            
        }

        /// <summary>
        /// Kutu id'si ile kutuyu bulmaya yarayan fonksiyon
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(ApiRoute.ApiRoute.ColdChainBox.GetColdChainBoxDetailById)]
        public async Task<IActionResult> GetColdChainBoxById(Guid boxId)
        {
            var response = await _coldChainBoxService.GetColdChainBoxById(boxId);
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NotFound();
            }

        }



    }
}
