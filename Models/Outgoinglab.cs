using System;
using System.ComponentModel.DataAnnotations;

namespace OutGoingLab.Models
{
    public class Outgoinglab
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        public bool IsSync_Bit { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Enter valid email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        //[Display(Name = "Mobile No *")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be of 10 digits")]
        public string MobileNo { get; set; }
        public string ContactPerson { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        public bool IsActive_Bit { get; set; }
        public string Details { get; set; }
    }
}