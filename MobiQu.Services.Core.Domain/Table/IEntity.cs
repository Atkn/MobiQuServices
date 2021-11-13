using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MobiQu.Services.Core.Domain.Table
{
    public interface IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public int State { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}
