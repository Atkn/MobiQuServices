using Microsoft.AspNetCore.Mvc;
using MobiQu.Services.Application.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobiQu.API.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Id ile sorgulatılan mağaza bilgilerini getirir
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        [HttpGet(ApiRoute.ApiRoute.Company.GetCompanyInformationById)]
        public async Task<IActionResult> GetCompanyInformation(Guid companyId)
        {
            var response = await _companyService.GetCompanyInformationAsync(companyId);
            if(response is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(response);
            }
            
        }

        [HttpGet(ApiRoute.ApiRoute.Company.CompanyDetectForLogin)]
        public async Task<IActionResult> GetCompanyInformation(string email, string password)
        {
            var result = await _companyService.CompanyDetectInformationAsync(email, password);
            if (result.IsSuccessFull)
            {
                return Ok(result);
            }
            else
            {
                return Ok(new { message = $"{email} ile aradığın bilgilerini bulamadık!" });
            }
        }

        [HttpGet(ApiRoute.ApiRoute.Company.GetCompanyInformationByApiKey)]

        public async Task<IActionResult> GetCompanyInformationyApiKey(string API_KEY)
        {
            var response = await _companyService.GetCompanyInfomartionByApiKeyAsync(API_KEY);
            if (response is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(response);
            }

        }
    }
}
