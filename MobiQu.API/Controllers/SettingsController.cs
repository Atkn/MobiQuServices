using Microsoft.AspNetCore.Mvc;
using MobiQu.Services.Application.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobiQu.API.Controllers
{
    public class SettingsController : Controller
    {
        private readonly IMobiQuBranchTableSettingsService _tableSettings;
        public SettingsController(IMobiQuBranchTableSettingsService tableSettings)
        {
            _tableSettings = tableSettings;
        }

        /// <summary>
        /// Id ile sorgulatılan mağaza bilgilerini getirir
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        [HttpGet(ApiRoute.ApiRoute.Settings.GetCompanyTableSettings)]
        public async Task<IActionResult> Index(string API_KEY)
        {
            var response = await _tableSettings.GetTableSettingsAsync(API_KEY);
            if (response != null)
                return Ok(response);
            else
                return NotFound();
        }
    }
}
