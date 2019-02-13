namespace SamsungApiAws.Controllers
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}