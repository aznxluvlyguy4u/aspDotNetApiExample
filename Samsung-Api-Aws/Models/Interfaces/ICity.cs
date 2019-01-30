namespace samsung_api.Models.Interfaces
{
    public interface ICity
    {
        int Id { get; set; }
        string CountryCode { get; set; }
        string CityName { get; set; }
        string CityAccentName { get; set; }
    }
}