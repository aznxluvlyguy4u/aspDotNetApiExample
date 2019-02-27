namespace samsung.api.DataSource.Models
{
    public class LinkInterest
    {
        public int LinkId { get; set; }
        public Link Link { get; set; }

        public int InterestId { get; set; }
        public Interest Interest { get; set; }
    }
}