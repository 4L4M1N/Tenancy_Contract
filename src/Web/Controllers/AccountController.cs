using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using TenancyContract.Models;
using Microsoft.AspNetCore.Identity;
using TenancyContract.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IStringLocalizer<AccountController> _localizer;
        private readonly UserManager<Tenant> userManager;
        private readonly UserManager<HouseOwner> userManagerHO;
        public AccountController(IStringLocalizer<AccountController> localizer, UserManager<Tenant> userManage, UserManager<HouseOwner> userManageho)
        {
            _localizer = localizer;
            this.userManager = userManage;
            this.userManagerHO = userManageho;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Tenant()
        {
            return View();
        }
        public IActionResult HouseOwner()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Exception()
        {
            return View();
        }
      
       
       
        public async Task<IActionResult>TenantRegister(TenantRegisterModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.NID);
                //var nid = await userManager.FindbyNidAsync(model.NID);
                
                if(user == null)
                {
                    user = new Tenant
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = model.Name,
                        Mobile = model.Mobile,
                        NID = model.NID,
                        Email = model.Email
                        
                    };
                    var result = await userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        return View("Success");
                    }
                   
                }
                else
                {
                   
                    //ModelState.AddModelError("NID", _localizer["NID already exists"]);
                    ViewBag.TenantError = "Tenant";
                }
                
            }
            return View("Tenant");
        }
        public async Task<IActionResult> HouseOwnerRegister(HouseOwnerRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManagerHO.FindByNameAsync(model.NID);
                //var nid = await userManager.FindbyNidAsync(model.NID);

                if (user == null)
                {
                    user = new HouseOwner
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = model.Name,
                        Mobile = model.Mobile,
                        NID = model.NID,
                        Email = model.Email

                    };
                    var result = await userManagerHO.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        return View("Success");
                    }

                }
                else
                {
                    
                    //ModelState.AddModelError("NID", _localizer["NID already exists"]);
                    ViewBag.HOError = "HO";
                }

            }
            return View("HouseOwner");
        }
        //TODO: Check Tenants, HouseOwner DB both!!!!!!!
         public async Task<IActionResult> LoginEnter(LoginModel model)
         {
            string accType = Request.Form["acctype"].ToString();
            
            //TODO: Check selection and show error message!!!
            if (ModelState.IsValid)
            {
                if(accType == "1")
                {
                    var user = await userManager.FindByNameAsync(model.UserName);
                    if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
                    {
                        var identity = new ClaimsIdentity("cookies");
                        
                        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        identity.AddClaim(new Claim(ClaimTypes.Surname, user.NID));
                        identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
                        identity.AddClaim(new Claim(ClaimTypes.Role, "Tenant"));
                        await HttpContext.SignInAsync("cookies", new ClaimsPrincipal(identity));
                        return RedirectToAction("index", "home", new { area = "Tenantarea" });
                        //return RedirectToAction("TenantDashboard");

                    }
                    //ModelState.AddModelError("", "Username or Password Incorrect");
                }else if(accType == "2")
                {
                    var userHO = await userManagerHO.FindByNameAsync(model.UserName);
                    if (userHO != null && await userManagerHO.CheckPasswordAsync(userHO, model.Password))
                    {
                        var identity = new ClaimsIdentity("cookies");
                        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userHO.Id));
                        identity.AddClaim(new Claim(ClaimTypes.Surname, userHO.NID));
                        identity.AddClaim(new Claim(ClaimTypes.Name, userHO.Name));
                        identity.AddClaim(new Claim(ClaimTypes.Role, "Houseowner"));
                        await HttpContext.SignInAsync("cookies", new ClaimsPrincipal(identity));
                        //return RedirectToAction("HouseOwnerDashboard");
                        return RedirectToAction("index", "home", new { area = "Houseowner" });
                    }
                    //ModelState.AddModelError("", "Username or Password Incorrect");
                }
                ViewBag.LoginError = "wrong";
                
            }
            return View("Login");

        }
        public async Task<IActionResult>Logout()
        {
            await HttpContext.SignOutAsync("cookies");
            return RedirectToAction("Index","Home");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}