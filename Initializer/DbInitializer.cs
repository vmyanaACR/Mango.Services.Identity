using System.Security.Claims;
using IdentityModel;
using Mango.Services.Identity.DbContexts;
using Mango.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.Identity.Initializer;

public class DbInitializer : IDbinitializer
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DbInitializer(ApplicationDbContext applicationDbContext,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _applicationDbContext = applicationDbContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void Initialize()
    {
        if (_roleManager.FindByNameAsync(SD.Admin).Result == null)
        {
            _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Customer)).GetAwaiter().GetResult();
        }
        else
        {
            return;
        }

        ApplicationUser adminuser = new ApplicationUser
        {
            UserName = "admin1@outlook.com",
            Email = "admin1@outlook.com",
            EmailConfirmed = true,
            PhoneNumber = "8978084894",
            FistName = "Vamshi",
            LastName = "Krishna"
        };

        _userManager.CreateAsync(adminuser, "Admin@1234").GetAwaiter().GetResult();
        _userManager.AddToRoleAsync(adminuser, SD.Admin).GetAwaiter().GetResult();

        var temp1 = _userManager.AddClaimsAsync(adminuser, new List<Claim>
        {
            new Claim(JwtClaimTypes.Name, adminuser.FistName + " " + adminuser.LastName),
            new Claim(JwtClaimTypes.GivenName, adminuser.FistName),
            new Claim(JwtClaimTypes.FamilyName, adminuser.LastName),
            new Claim(JwtClaimTypes.Role, SD.Admin)
        }).Result;


        ApplicationUser customeruser = new ApplicationUser
        {
            UserName = "customer1@outlook.com",
            Email = "customer1@outlook.com",
            EmailConfirmed = true,
            PhoneNumber = "8978084894",
            FistName = "Sai",
            LastName = "Myana"
        };

        _userManager.CreateAsync(customeruser, "Admin@1234").GetAwaiter().GetResult();
        _userManager.AddToRoleAsync(customeruser, SD.Customer).GetAwaiter().GetResult();

        var temp2 = _userManager.AddClaimsAsync(customeruser, new List<Claim>
        {
            new Claim(JwtClaimTypes.Name, customeruser.FistName + " " + customeruser.LastName),
            new Claim(JwtClaimTypes.GivenName, customeruser.FistName),
            new Claim(JwtClaimTypes.FamilyName, customeruser.LastName),
            new Claim(JwtClaimTypes.Role, SD.Customer)
        }).Result;
    }
}