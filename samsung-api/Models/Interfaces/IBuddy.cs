using samsung.api.Enumerations;

namespace samsung.api.Controllers
{
    public interface IBuddy
    {
        string Image { get; set; }
        string FullName { get; set; }
        string Role { get; set; } // Biology teacher
        int Rating { get; set; }

        BuddyRequestState ContactRequestState { get; set; }
    }
}