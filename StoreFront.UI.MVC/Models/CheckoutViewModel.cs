using System.ComponentModel.DataAnnotations;

namespace StoreFront.UI.MVC.Models

{
    public class CheckoutViewModel
    {

        [Required]
        public string AccountName { get; set; }

        [Required]
        public string ServerName { get; set; }
        [Required]
        public string AccountCountry { get; set; }
        //[Required]
        //public string City { get; set; }
        //[Required]
        //public string? State { get; set; }
        //[Required]
        //public string Zip { get; set; }
    }
}

