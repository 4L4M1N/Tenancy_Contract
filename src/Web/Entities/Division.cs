using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TenancyContract.Entities
{
    public class Division
    {
       [Key]
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        [NotMapped]
        public int DistrictId { get; set; }
        [NotMapped]
        public int ThanaId { get; set; }

    }
}