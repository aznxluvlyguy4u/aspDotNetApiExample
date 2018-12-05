namespace samsung.api.Controllers
{
    public interface IContact
    {
        string Image { get; set; }
        string FullName { get; set; }
        string Role { get; set; } // Biology teacher
        int Rating { get; set; }

        ContactRequestState ContactRequestState { get; set; }
    }
}