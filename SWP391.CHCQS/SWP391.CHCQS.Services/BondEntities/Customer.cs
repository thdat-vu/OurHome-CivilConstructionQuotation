using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWP391.CHCQS.Services.BondEntities
{
    public class Customer
    {
        [Key]
        public string  Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string FullName { get => FirstName + LastName; }
        public string PhoneNumber { get; set; }
        public string? Email {  get; set; }
        public int Gender { get; set; }
        //Acount
        [ForeignKey("Account")]
        public string?  AccountId { get; set; }
        public Account? UserAccount { get; set; }

    }
}