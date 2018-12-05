using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using samsung.api.Controllers;

namespace samsung_api.Models.Interfaces
{
    public interface IContactsService
    {
        Task<List<IContact>> GetContactsAsync(ClaimsPrincipal user, ContactRequestState state);
    }
}