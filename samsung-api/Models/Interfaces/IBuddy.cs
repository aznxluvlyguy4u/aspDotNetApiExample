using samsung.api.Enumerations;

namespace samsung_api.Models.Interfaces
{
    public interface IBuddy
    {
        string image { get; set; }
        string fullName { get; set; }
        string role { get; set; } // Biology teacher
        int rating { get; set; }

        BuddyRequestState contactRequestState { get; set; }
    }
}