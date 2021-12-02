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

        public Guid DeviceId { get; set; }

        public virtual Device Device { get; set; }
    }
}
