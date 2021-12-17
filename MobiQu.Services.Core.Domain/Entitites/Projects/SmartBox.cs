using MobiQu.Services.Core.Domain.Entitites.Projects;
using MobiQu.Services.Core.Domain.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobiQu.Services.Core.Domain.Entitites
{
    public class SmartBox : BaseEntity
    {
        public Guid CompanyId { get; set; }
        public Guid? DeviceId { get; set; }
        public int? MaxTemperatureValue { get; set; }
        public int? MinTemperatureValue { get; set; }
        public int? MaxShockValue { get; set; }
        public int? MinShockValue { get; set; }
        public int? MinMoistureValue { get; set; }
        public int? MaxMoistureValue { get; set; }
        public string MaxTemperature { get; set; }
        public string MinTemperature { get; set; }
        public string MaxMoisture { get; set; }
        public string MinMoisture { get; set; }
        public virtual Device Device { get; set; }
    }
}
