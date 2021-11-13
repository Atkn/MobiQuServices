using MobiQu.Services.Core.Domain.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobiQu.Services.Core.Domain.Entitites.Projects
{
    public class MobiQuBranchTableSettings : BaseEntity
    {
        public bool IsShowBoxNumber { get; set; }

        public bool IsShowDeliveryStatus { get; set; }

        public bool IsShowTemperature { get; set; }
        public bool IsShowHumidity { get; set; }
        public bool IsShowLocation { get; set; }
        public bool IsShowShock { get; set; }
        public bool IsShowBatteryState { get; set; }
        public bool IsShowLockState { get; set; }

        public Guid CompanyId { get; set; }
    }

}
