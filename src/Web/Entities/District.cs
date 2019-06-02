using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TenancyContract.Entities
{
    public class District
    {
        [Key]
        public int DistrictId { get; set; }
        public int DivisionId { get; set; }
        public string DistrictName { get; set; }

    }
}