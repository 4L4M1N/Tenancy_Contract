using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenancyContract.Areas.Houseowner.Views.Shared
{
    //TODO: Check anypage is missing
    public class ManageNavPages
    {
        public static string Index => "Index";

        public static string HouseDescription => "HouseDescription";

        public static string Message => "Message";

        public static string PersonalData => "PersonalData";

        public static string Tenants => "Tenants";
        public static string ContractPaper => "ContractPaper";
        public static string AccountSettings => "AccountSettings";
        public static string Numbers => "Numbers";

        public static string Houses => "Houses";
        public static string AddHouse => "AddHouse";
        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string HouseDescriptionNavClass(ViewContext viewContext) => PageNavClass(viewContext, HouseDescription);

        public static string MessageNavClass(ViewContext viewContext) => PageNavClass(viewContext, Message);

        public static string PersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, PersonalData);
        public static string TenantsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Tenants);

        public static string ContractPaperNavClass(ViewContext viewContext) => PageNavClass(viewContext, ContractPaper);

        public static string AccountSettingsNavClass(ViewContext viewContext) => PageNavClass(viewContext, AccountSettings);
        public static string NumbersNavClass(ViewContext viewContext) => PageNavClass(viewContext, Numbers);
        public static string HousesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Houses);
        public static string AddHouseNavClass(ViewContext viewContext) => PageNavClass(viewContext, AddHouse);

        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}

