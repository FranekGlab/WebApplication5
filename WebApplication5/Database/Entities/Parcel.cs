using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Database.Entities
{
    public class Parcel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Adresse's name and surname is required!")]
        public string Addressee { get; set; }
        [Required(ErrorMessage = "Address is required!")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City is required!")]
        public string City { get; set; }
        [Required(ErrorMessage = "Postal code is required!")]
        [DataType(DataType.PostalCode,ErrorMessage = "Invalid Postal Code!")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Country is required!")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Weight is required!")]
        public double Weight { get; set; }
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number Format!")]
        public string ContactNumber { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTimeOffset RegisteredDate { get; set; }
        [DataType(DataType.Date)]
        public DateTimeOffset ShippedDate { get; set; }
        [DataType(DataType.Date)]
        public DateTimeOffset DeliveredDate { get; set; }


        public Parcel()
        {

        }

    }
}
