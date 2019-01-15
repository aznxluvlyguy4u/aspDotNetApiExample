namespace samsung_api.Models.Interfaces
{
    public interface IGeneralUser
    {
        int id { get; set; }
        string firstName { get; set; }
        string lastName { get; set; }
        string email { get; set; }
        string password { get; set; }
        string city { get; set; }
        string phoneNumber { get; set; }
        int techLevel { get; set; }
        int linkedInId { get; set; }
        int facebookId { get; set; }
        string identityId { get; set; }
        string location { get; set; }
        string locale { get; set; }
        string gender { get; set; }
    }
}