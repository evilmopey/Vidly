using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [DisplayName("Subscribed to newslatter?")]
        public bool IsSubcribedToNewsLetter { get; set; }
        [DisplayName("Membership Type?")]
        public MembershipType MembershipType { get; set; }
        public byte MembershipTypeId { get; set; }
        [DisplayName("Birthdate")]
        [Min18YearsIfAMember]
        public DateTime? DateTime { get; set; }

    }
}
