﻿using SamsungApiAws.DataSource.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace samsung.api.DataSource.Models
{
    public class GeneralUserSeenGeneralUser : BaseEntity
    {
        public int LoggedInGeneralUserId { get; set; }

        [ForeignKey("LoggedInGeneralUserId")]
        public GeneralUser LoggedInGeneralUser { get; set; }

        public int? HasSeenGeneralUserId { get; set; }

        [ForeignKey("HasSeenGeneralUserId")]
        public GeneralUser HasSeenGeneralUser { get; set; }
    }
}