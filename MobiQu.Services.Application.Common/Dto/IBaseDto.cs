using System;
using System.Collections.Generic;
using System.Text;

namespace MobiQu.Services.Application.Dto
{
    public interface IBaseDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }
    }
}
