using MobiQu.Services.Application.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobiQu.Services.Application.Common.Dto.Company
{
    public class LoginResponseDto : BaseDto
    {
        public bool CanLogin { get; set; }

        public string API_KEY { get; set; }

        public string Email { get; set; }

    }
}
