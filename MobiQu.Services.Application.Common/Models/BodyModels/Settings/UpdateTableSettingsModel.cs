using System;
using System.Collections.Generic;
using System.Text;

namespace MobiQu.Services.Application.Common.Models.BodyModels.Settings
{
    public class UpdateTableSettingsModel
    {
        public bool IsShowBoxNumber { get; set; }

        public bool IsShowDeliveryStatus { get; set; }

        public bool IsShowTemperature { get; set; }
        public bool IsShowHumidity { get; set; }
        public bool IsShowLocation { get; set; }
        public bool IsShowShock { get; set; }
        public bool IsShowBatteryState { get; set; }
        public bool IsShowLockState { get; set; }
    }
}
