using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobiQu.Services.Application.Common.Dto.MobiQuBranchTableSettings;
using MobiQu.Services.Application.Common.Models.BodyModels.Settings;
using MobiQu.Services.Application.Common.Models.Responses;
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
        private readonly ICompanyService _companyService;
        public SettingsController(IMobiQuBranchTableSettingsService tableSettings, ICompanyService companyService)
        {
            _tableSettings = tableSettings;
            _companyService = companyService;
        }

        /// <summary>
        /// Id ile sorgulatılan mağaza bilgilerini getirir
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        [HttpGet(ApiRoute.ApiRoute.Settings.GetCompanyTableSettings)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<TableSettingsDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        public async Task<IActionResult> UpdateTableSettings(string API_KEY, [FromBody]UpdateTableSettingsModel tableSettings)
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="API_KEY">Kullanıcının anahtarı</param>
        /// <param name="oldPassword">Kullanıcının eski şifresi</param>
        /// <param name="newPassword">Kullanıcının yeni şifresi</param>
        /// <param name="confirmPassword">Doğruladığı Şifre</param>
        /// <returns></returns>
        [HttpPost(ApiRoute.ApiRoute.Settings.UpdateCompanyPassword)]
        public async Task<IActionResult> UpdatePassword(string API_KEY,string oldPassword, string newPassword, string confirmPassword)
        {
            var response = await _companyService.UpdatePasswordAsync(API_KEY, oldPassword, newPassword, confirmPassword);
            return Ok(response);
        }
    }
}
