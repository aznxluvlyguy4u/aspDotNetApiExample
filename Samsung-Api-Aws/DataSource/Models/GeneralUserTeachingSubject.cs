using SamsungApiAws.DataSource.Models;

namespace samsung.api.DataSource.Models
{
    public class GeneralUserTeachingSubject : BaseEntity
    {
        public int GeneralUserId { get; set; }
        public GeneralUser GeneralUser { get; set; }

        public int TeachingSubjectId { get; set; }
        public TeachingSubject TeachingSubject { get; set; }
    }
}