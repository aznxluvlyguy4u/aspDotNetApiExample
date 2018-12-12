using Microsoft.AspNetCore.Identity;
using samsung_api.DataSource.Models;
using samsung_api.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace samsung.api.DataSource.Models
{

    public class AppRole : IdentityRole<Guid>
    {

    }
}