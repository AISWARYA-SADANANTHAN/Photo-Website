using System.ComponentModel.DataAnnotations;

namespace PhotographyWebsite.Models
{
    public class orderplaced
    {

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter username")]
        [MaxLength(50, ErrorMessage = "Maximum 50 character allowed")]
        public string Customer_name { get; set; }
        [Required]
        [EmailAddress]
        public string Email_id { get; set; }
        public string type_photo { get; set; }
        public string book_date_st { get; set; }
        public string date_end { get; set; }
    }
}
