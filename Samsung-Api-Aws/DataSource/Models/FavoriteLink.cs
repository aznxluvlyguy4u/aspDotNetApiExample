namespace samsung.api.DataSource.Models
{
    public class FavoriteLink
    {
        public int GeneralUserId { get; set; }
        public GeneralUser GeneralUser { get; set; }

        public int LinkId { get; set; }
        public Link Link { get; set; }
    }
}