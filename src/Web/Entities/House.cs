using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TenancyContract.Entities
{
    public class House
    {
        public string Id { get; set; }

        public string DagNo { get; set; }

        public string HouseNo { get; set; }

        public string AddressDivision { get; set; }

        public string AddressDistrict { get; set; }

        public string AddressThana { get; set; }

        public string AddressPO { get; set; }

        public string AddressVillage { get; set; }
        public string AddressRoad { get; set; }
        public string ServiceCentres { get; set; }
        public string CityCorporation { get; set; }
       
        public string NID { get; set; }
    }
}
