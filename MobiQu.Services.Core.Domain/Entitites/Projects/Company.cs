using MobiQu.Services.Core.Domain.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobiQu.Services.Core.Domain.Entitites
{
    public class Company : BaseEntity
    {
        public string API_KEY { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
