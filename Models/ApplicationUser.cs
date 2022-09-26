using Microsoft.AspNetCore.Identity;

namespace Mango.Services.Identity.Models;

public class ApplicationUser : IdentityUser
{
    public string FistName { get; set; }
    public string LastName { get; set; }
}