using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using samsung.api.Repositories.GeneralUsers;
using samsung_api.Models.Interfaces;

namespace samsung.api.Services.GeneralUsers
{
    public class GeneralUsersService : IGeneralUsersService
    {
        private readonly IGeneralUsersRepository _generalUsersRepository;
        public GeneralUsersService(IGeneralUsersRepository generalUsersRepository)
        {
            _generalUsersRepository = generalUsersRepository;
        }

        public IGeneralUser CreateGeneralUser(IGeneralUser generalUser)
        {
            return _generalUsersRepository.CreateGeneralUser(generalUser);
        }
    }
}
