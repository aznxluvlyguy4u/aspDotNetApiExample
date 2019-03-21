using SamsungApiAws.DataSource.Models;

namespace samsung.api.DataSource.Models
{
    public class GeneralUserInterest : BaseEntity
    {
        public int GeneralUserId { get; set; }
        public GeneralUser GeneralUser { get; set; }

        public int InterestId { get; set; }
        public Interest Interest { get; set; }
    }
}