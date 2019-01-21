namespace samsung.api.DataSource.Models
{
    public class GeneralUserTeachingLevel
    {
        public int GeneralUserId { get; set; }
        public GeneralUser GeneralUser { get; set; }

        public int TeachingLevelId { get; set; }
        public TeachingLevel TeachingLevel { get; set; }
    }
}