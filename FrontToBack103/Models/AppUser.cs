using Microsoft.AspNetCore.Identity;

namespace FrontToBack103.Models
{
    public class AppUser:IdentityUser
    {
        public string Fullname { get; set; }
    }
}
