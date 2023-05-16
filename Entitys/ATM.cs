using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitys.Entity
{
    public class ATM
    {
        public Guid Id { get; set; }
        public string Region { get; set; }
        public string Address { get; set; }
        public bool Operative { get; set; } = true;
        public IdentityUser AppUser { get; set; }
    }
}
