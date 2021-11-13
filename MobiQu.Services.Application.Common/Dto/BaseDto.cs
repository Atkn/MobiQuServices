using System;
using System.Collections.Generic;
using System.Text;

namespace MobiQu.Services.Application.Dto
{
    public class BaseDto : IBaseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedAtString { get; set; }
        public string ModifiedAtString { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
