using System;
using System.Collections.Generic;
using System.Text;

namespace MobiQu.Services.Application.Dto
{
    public class SmartBoxDto : BaseDto
    {
        public string MinTemperature { get; set; }
        public string MaxTemperature { get; set; }
        public string MinMoisture { get; set; }
        public string MaxMoisture { get; set; }
        public int? MinTemperatureValue { get; set; }
        public int? MaxTemperatureValue { get; set; }
        public int? MinMoistureValue { get; set; }
        public int? MaxMoistureValue { get; set; }

        public Guid CompanyId { get; set; }
    }

}
