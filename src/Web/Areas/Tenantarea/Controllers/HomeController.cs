using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tenancy_Contract.DataAccessLayer;
using TenancyContract.Entities;
using TenancyContract.Models;
using System.Text;
using System.Data;
//using Rotativa.AspNetCore; 
//TODO: Localization Pending
namespace TenancyContract.Areas.Tenantarea.Controllers
{
    [Authorize(Policy = "OnlyTenant")]
    [Area(nameof(Tenantarea))]
    public class HomeController : Controller
    {
        private readonly TenancyContractDbContext _db;
        public HomeController(TenancyContractDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Message()
        {
            return View();
        }
        public IActionResult Numbers()
        {
            return View();
        }

        public async Task<IActionResult>  Agreements()
        {
            string nid = User.Claims.ElementAt(1).Value;
            var contracts = await _db.Contracts.Where(e=> e.TenantNID == nid).ToListAsync();
            return View(contracts);
        }
        // [HttpPost]
        // public async Task<IActionResult> Agreements(string donedone)
        // {
        //     string contractId = donedone;
        //     string nid = User.Claims.ElementAt(1).Value;
        //    if(contractId == null)
        //    {
        //        return View("Index");
        //    }
        //     var contracts = _db.Contracts.FirstOrDefault(e=> e.Id == contractId && e.TenantNID == nid);
        //     if(contracts!=null)
        //     {
        //         contracts.AcceptTenant = true;
        //         _db.Contracts.Update(contracts);
        //         await _db.SaveChangesAsync();
        //         return View("Agreements");
        //     }
        //     return View("Index");
        // }
        [HttpPost]
        public async Task<IActionResult> Confirm(string contractID)
        {
            //TODO: Confirm action not complete!! contractID = null 

            string contractId = contractID;
            string nid = User.Claims.ElementAt(1).Value;
            var contracts = _db.Contracts.FirstOrDefault(e=> e.Id == contractId && e.TenantNID == nid);
            if(contracts!=null)
            {
                contracts.AcceptTenant = true;
                _db.Contracts.Update(contracts);
                await _db.SaveChangesAsync();
                return Redirect("Agreements");
            }
            return View("Index");

        }
        [HttpPost]
        public async Task<IActionResult> ResultContract(string contractIDD)
        {
            string contractId = contractIDD;
            string nid = User.Claims.ElementAt(1).Value;
            var contracts = await _db.Contracts.FirstOrDefaultAsync(e=> e.Id == contractIDD && e.TenantNID == nid);
            string houseOwnerNid = contracts.HouseOwnerNID;
            var houseOwnerInfo = await _db.HouseOwners.FirstOrDefaultAsync(e=> e.NID == houseOwnerNid);
            if(contracts!=null && houseOwnerInfo!=null)
            {
                CPaperOneModel paperModel = new CPaperOneModel()
                {
                    TenantName = User.Claims.ElementAt(2).Value,
                    HouseOwnerName = houseOwnerInfo.Name,
                    StartDate = contracts.StartDate,
                    EndDate = contracts.EndDate,
                    AgreementDate = contracts.AgreementDate,
                    GasBill = contracts.GasBill,
                    ElectricityBill = contracts.ElectricityBill,
                    IncreaseRate = contracts.IncreaseRate,
                    IncreaseTime = contracts.IncreaseTime,
                    Advance = contracts.Advance,
                    MaintainCost = contracts.MaintainCost
                };
                return ViewPdf(paperModel);
            }
            return View();
        }
        [HttpPost]
        public IActionResult ViewPdf(CPaperOneModel model)
        {
            return new Rotativa.AspNetCore.ViewAsPdf("ViewPdf",model)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.Legal,
                PageMargins = {Right=20,Left=20},
                CustomSwitches = "--page-offset 0 --footer-center [page] --footer-font-size 12" 
            };
        }
        public IActionResult AccountSettings()
        {
            return View();
        }

    }
}