using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tenancy_Contract.DataAccessLayer;
using TenancyContract.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using TenancyContract.Models;
using System.Text;
using System.Data;
using Microsoft.AspNetCore.Hosting;
using Web.Extensions;
using Web.Entities;
using Rotativa.AspNetCore;
using Web.Repository;
using Web.Models;

//TODO: Localization Pending
namespace TenancyContract.Areas.Houseowner.Controllers
{

    [Authorize(Policy = "OnlyHO")]
    [Area(nameof(Houseowner))]
    //[Route(nameof(Houseowner) + "/[controller]")]
    public class HomeController : Controller
    {
        private readonly TenancyContractDbContext  _db;
        private readonly IHostingEnvironment _environment;
        private INotificationRepository _notificationrepository;
        public HomeController(TenancyContractDbContext db, IHostingEnvironment environment,INotificationRepository notificationrepository)
        {
            _db = db;
            _environment = environment;
            _notificationrepository = notificationrepository;
            
        }

        public IActionResult Index()
        {
            //return View();
            var userNID = User.Claims.ElementAt(1).Value;
            var hoInfo = new HouseOwnerInfo();
            hoInfo = _db.HouseOwners
                        .Where(p => p.NID == userNID)
                        .Select(p => new HouseOwnerInfo()
                        {
                            Name = p.Name,
                            Email = p.Email,
                            Phone = p.Mobile,
                            NID = p.NID,
                            Image = p.Image
                        }
                        ).SingleOrDefault();
            return View();
        }
        public IActionResult AccountSettings()
        {
            return View();
        }
        public IActionResult ContractPaper()
        {
            return View();
        }
        public async Task<IActionResult> HouseDescription()
        {
            var userNid = User.Claims.ElementAt(1).Value;
            var houses = await _db.Houses.Where(e => e.NID == userNid).ToListAsync();

            return View(houses);
        }
        public IActionResult Houses()
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
        public IActionResult PersonalData()
        {
            return View();
        }
        public IActionResult FormOne()
        {
            
            var userNID = User.Claims.ElementAt(1).Value;
            List<HouseOption> houselist = new List<HouseOption>();
            houselist = _db.Houses
            .Where(p => p.NID == userNID)
            .Select(p=> new HouseOption()
            {
                Id = p.Id,
                DagNo = p.DagNo
            }).ToList();
            ViewBag.HouseList = houselist;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> FormOne(CPaperOneModel model)
        {
            //TODO: Convert DataTime to only Date
            //TODO: Validation needed. Task isn't complete
            string houseId = HttpContext.Request.Form["houseId"];
            if (ModelState.IsValid)
            {
                var contract = new Contract
                {
                    Id = Guid.NewGuid().ToString(),
                    HouseId = houseId,
                    TenantId = model.TenantId,
                    HouseOwnerId = User.Claims.ElementAt(0).Value,
                    TenantNID = model.TenantNID,
                    HouseOwnerNID = User.Claims.ElementAt(1).Value,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    AgreementDate = model.AgreementDate,
                    Advance = model.Advance,
                    GasBill = model.GasBill,
                    ElectricityBill = model.ElectricityBill,
                    MaintainCost = model.MaintainCost,
                    IncreaseTime = model.IncreaseTime,
                    IncreaseRate = model.IncreaseRate,
                    AcceptTenant = false,
                    AcceptHO = true
                };
                var result = _db.Contracts.FirstOrDefault(e=> e.TenantNID == model.TenantNID);
                if(result!=null)
                {
                    ViewBag.TenantError = 0;
                    ViewBag.TenantErrorID = model.TenantNID;
                    return View("Search");
                }
                _db.Contracts.Add(contract);
                await _db.SaveChangesAsync();
                var notification = new Notification{
                    Text = $"{User.Claims.ElementAt(2).Value} make a contract with you! Check your agreement."
                };
                _notificationrepository.Create(notification,model.TenantNID,User.Claims.ElementAt(1).Value);
                return View("Success");
                
            }
            return View();
        }
        public IActionResult Search()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult>Search(string searchString)
        {
            //TODO: Improve Search and also get history!!!
            //IEnumerable<TenancyContract.Entities.Tenant> tenant = null;
            //var result = from m in _db.Tenants select m;
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    result = result.Where(s => s.NID == searchString);
            //}
            //tenant = await result.ToListAsync();
            if(!String.IsNullOrEmpty(searchString))
            {
                var tenant = await (from m in _db.Tenants where m.NID == searchString select m).FirstOrDefaultAsync<Tenant>();
                if(tenant == null)
                {
                    ViewBag.Tenant = "true";
                }else
                {
                    var sstenant = new Tenant();
                    sstenant = tenant;
                    HttpContext.Session.Set("sstenant", sstenant);
                    return View(tenant);
                }
            }

            return View();

        }
      
        public IActionResult AddHouse()
        {
            
            List<Division> divisionList = new List<Division>();
            // ------- Getting Data from Database Using EntityFrameworkCore -------
            divisionList = (from division in _db.Divisions select division).ToList();
            // ------- Inserting Select Item in List -------
            divisionList.Insert(0, new Division {DivisionId = 0, DivisionName = "Select"});
            //Division dv = _db.Divisions.FirstOrDefault();
            
            // ------- Assigning divisionList to ViewBag.ListofDivision -------
            ViewBag.ListofDivision = divisionList;
            
            return View(); 
        }

        public JsonResult GetDistrict(int DivisionID)
        {
            List<District> districtlist = new List<District>();

            // ------- Getting Data from Database Using EntityFrameworkCore -------
            districtlist = (from district in _db.Districts
                               where district.DivisionId == DivisionID
                            select district).ToList();

            // ------- Inserting Select Item in List -------
            districtlist.Insert(0, new District { DistrictId = 0, DistrictName = "Select" });


            return Json(new SelectList(districtlist, "DistrictId", "DistrictName"));
        }

        public JsonResult GetThana(int DistrictId)
        {
            List<Thana> thanaList = new List<Thana>();

            // ------- Getting Data from Database Using EntityFrameworkCore -------
            thanaList = (from thana in _db.Thanas
                           where thana.DistrictId == DistrictId
                           select thana).ToList();

            // ------- Inserting Select Item in List -------
            thanaList.Insert(0, new Thana { ThanaId = 0, ThanaName = "Select" });

            return Json(new SelectList(thanaList, "ThanaId", "ThanaName"));
        }
        [HttpPost]
        
        public async Task<IActionResult> AddHouse(HouseModel divisionobj, IFormCollection formCollection)
        {

            if (divisionobj.DivisionId == 0)
            {
                ModelState.AddModelError("", "Division Required");
            }
            else if (divisionobj.DistrictId == 0)
            {
                ModelState.AddModelError("", "District Required");
            }
            else if (divisionobj.ThanaId == 0)
            {
                ModelState.AddModelError("", "District Required");
            }
            string div = HttpContext.Request.Form["divisionName"];
            string dis = HttpContext.Request.Form["districtName"];
            string thana = HttpContext.Request.Form["thanaName"];
            var userId = User.Claims.ElementAt(1).Value;
            
            
            if (ModelState.IsValid)
            {
                var house = new House
                {

                    Id = Guid.NewGuid().ToString(),
                    AddressDivision = div,
                    AddressDistrict = dis,
                    AddressThana = thana,
                    AddressRoad = divisionobj.AddressRoad,
                    AddressVillage = divisionobj.AddressVillage,
                    DagNo = divisionobj.DagNo,
                    HouseNo = divisionobj.HouseNo,
                    NID = userId.ToString()
                    
                };
                _db.Houses.Add(house);
                await _db.SaveChangesAsync();
                return View("Success");


            }
            List<Division> divisionList = new List<Division>();
            divisionList = (from division in _db.Divisions
                            select division).ToList();
            divisionList.Insert(0, new Division { DivisionId = 0, DivisionName = "Select" });
            ViewBag.ListofDivision = divisionList;
            //TODO: Little Bug Exixts
            return View();
        }
        [HttpPost]
        public IActionResult FormPDF(CPaperOneModel model)
        {
            return new Rotativa.AspNetCore.ViewAsPdf("FormPDF", model)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.Legal,
                PageMargins = {Right=20,Left=20},
                CustomSwitches = "--page-offset 0 --footer-center [page] --footer-font-size 12" 

            };

        }
        [HttpPost]
        public IActionResult ViewPDF(CPaperOneModel model)
        {
            return new Rotativa.AspNetCore.ViewAsPdf("ViewPDF", model)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.Legal,
                PageMargins = {Right=20,Left=20},
                CustomSwitches = "--page-offset 0 --footer-center [page] --footer-font-size 12" 
            };

        }
        public IActionResult Tenants()
        {
            string HouseOwnerNID = User.Claims.ElementAt(1).Value;
            List<House> houseList = new List<House>();
            houseList = (from house in _db.Houses where house.NID == HouseOwnerNID select house).ToList();
            houseList.Insert(0, new House{Id="0",DagNo="Select"});
            ViewBag.HouseList = houseList;
            return View();
        }
        public JsonResult GetTenantContract(string HouseID)
        {
            List<Contract> tenantContractList = new List<Contract>();
            tenantContractList = (from tenant in _db.Contracts 
                            where tenant.HouseId == HouseID
                                select tenant).ToList(); 
            tenantContractList.Insert(0,new Contract{HouseId="0",TenantNID="Select"});
            return Json(new SelectList(tenantContractList,"HouseId","TenantNID"));
        }

        public JsonResult GetTenantInfo(string TenantNID)
        {
            TenantInfo result = new TenantInfo();
            result = (from tenant in _db.Tenants
                        where tenant.NID == TenantNID
                        select new TenantInfo {NID = tenant.NID,Name = tenant.Name,Phone = tenant.Mobile,Email = tenant.Email}).FirstOrDefault();
            
            return Json(new TenantInfo{NID = result.NID, Name = result.Name, Phone = result.Phone,Email=result.Email});
        }

        public JsonResult GetContractInfo(string TenantNID, string HouseID)
        {
            Contract contractInfo = new Contract();
            contractInfo = (from contract in _db.Contracts
                                where contract.HouseId == HouseID && contract.TenantNID == TenantNID
                                select contract).FirstOrDefault<Contract>();
            //TODO: Bug neeed new objcet
            return Json(new Contract {
                Id = contractInfo.Id,
                HouseId = contractInfo.HouseId,
                AgreementDate = contractInfo.AgreementDate,
                StartDate = contractInfo.StartDate,
                EndDate = contractInfo.EndDate,
                Advance = contractInfo.Advance,
                GasBill = contractInfo.GasBill,
                MaintainCost = contractInfo.MaintainCost,
                ElectricityBill = contractInfo.ElectricityBill,
                TenantNID = contractInfo.TenantNID,
                AcceptHO = contractInfo.AcceptHO,
                AcceptTenant = contractInfo.AcceptTenant,
                IncreaseTime = contractInfo.IncreaseTime,
                IncreaseRate = contractInfo.IncreaseRate
            });
            
        }


    }
}