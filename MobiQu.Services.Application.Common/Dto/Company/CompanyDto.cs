using MobiQu.Services.Application.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobiQu.Services.Application.Common.Dto.Company
{
    public class CompanyDto : BaseDto
    {
        public string Email { get; set; }

        public string Api_Key { get; set; }
    }
}
