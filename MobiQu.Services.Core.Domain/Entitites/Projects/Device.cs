using MobiQu.Services.Core.Domain.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobiQu.Services.Core.Domain.Entitites.Projects
{
    public class Device : BaseEntity
    {
        public string DeviceNumber { get; set; }

        public Guid SmartBoxId { get; set; }

    }
}
