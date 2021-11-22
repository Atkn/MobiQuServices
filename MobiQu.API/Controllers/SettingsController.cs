using Microsoft.AspNetCore.Mvc;
using MobiQu.Services.Application.Common.Models.BodyModels.Settings;
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
            return Ok(response);
        }

        /// <summary>
        /// Login olan şirketin tablo ayarlarını günceller
        /// </summary>
        /// <param name="tableSettings"></param>
        /// <param name="API_KEY"></param>
        /// <returns></returns>
        [HttpPost(ApiRoute.ApiRoute.Settings.UpdateCompanyTableSettings)]
        public async Task<IActionResult> UpdateTableSettings([FromBody]UpdateTableSettingsModel tableSettings, string API_KEY)
        {
            var result = await _tableSettings.UpdateTableSettingsAsync(tableSettings, API_KEY);
            if (result)
            {
                var responseMessage = new { messageValue = "Tablo Ayarları Başarıyla Güncellendi", IsSuccessFull = true };
                return Ok(responseMessage);
            }
            var respMessage = new { messageValue = $"Tablo Ayarları Güncellenirken Problem Oluştu {API_KEY} ile başlayan anahtarınızı kontrol edin", IsSuccessFull = false };
            return Ok(respMessage);
        }
    }
}
