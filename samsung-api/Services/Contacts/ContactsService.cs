using samsung.api.Controllers;
using samsung_api.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace samsung.api.Services.Contacts
{
    public class ContactsService : IContactsService
    {
        public Task<List<IContact>> GetContactsAsync(ClaimsPrincipal user, ContactRequestState state)
        {
            throw new NotImplementedException();
        }
    }
}
