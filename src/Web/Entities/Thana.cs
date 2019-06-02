using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TenancyContract.Entities
{
    public class Thana
    {
        [Key]
        public int ThanaId { get; set; }
        public string ThanaName { get; set; }
        public int DistrictId { get; set; }
    }
}