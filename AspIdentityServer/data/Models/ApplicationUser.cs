using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspIdentityServer.data.Models
{
    public class ApplicationUser :  IdentityUser
    {
        public string lievelingskleur { get; set; }

        public string voornaam { get; set; }

        public string achternaam { get; set; }
    }
}
