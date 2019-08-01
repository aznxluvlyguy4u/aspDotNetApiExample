namespace samsung.api.DataSource.Models
{
    public class GeneralUserTeachingSubject
    {
        public int GeneralUserId { get; set; }
        public GeneralUser GeneralUser { get; set; }

        public int TeachingSubjectId { get; set; }
        public TeachingSubject TeachingSubject { get; set; }
    }
}