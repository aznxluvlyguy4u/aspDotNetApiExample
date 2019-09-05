using System;

namespace SamsungApiAws.DataSource.Models
{
    // TODO: Figure out if this should be saved to DB or not
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}