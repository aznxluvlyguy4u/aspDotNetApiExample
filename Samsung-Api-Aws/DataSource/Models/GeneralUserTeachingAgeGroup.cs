using SamsungApiAws.DataSource.Models;

namespace samsung.api.DataSource.Models
{
    public class GeneralUserTeachingAgeGroup : BaseEntity
    {
        public int GeneralUserId { get; set; }
        public GeneralUser GeneralUser { get; set; }

        public int TeachingAgeGroupId { get; set; }
        public TeachingAgeGroup TeachingAgeGroup { get; set; }
    }
}