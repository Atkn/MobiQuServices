using MobiQu.Services.Application.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobiQu.Services.Application.Common.Dto.Device
{
    public class DeviceDto :BaseDto
    {
        public SmartBoxDto SmartBox { get; set; }

        public Guid SmartBoxId { get; set; }

    }
}
