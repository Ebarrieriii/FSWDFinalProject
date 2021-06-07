using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSWDFinalProject.DATA.EF
{
    public class ComputerMetadata
    {
        [Display(Name = "Computer ID")]
        public int ComputerId { get; set; }

        [Required(ErrorMessage = "* This field is required!")]
        [StringLength(50, ErrorMessage = "* Value must be 50 characters or less!")]
        [Display(Name = "Computer Model")]
        public string ComputerModel { get; set; }

        [Required(ErrorMessage = "* This field is required")]
        [StringLength(128, ErrorMessage = "* Value must be 5128 characters or less!")]
        [Display(Name = "Owner ID")]
        public string OwnerId { get; set; }

        [StringLength(50, ErrorMessage = "* Value must be 50 characters or less!")]
        [Display(Name = "Photo")]
        public string ComputerPhoto { get; set; }

        [StringLength(300, ErrorMessage = "* Value must be 300 characters or less!")]
        [UIHint("MultilineText")]
        public string Notes { get; set; }

        [Required(ErrorMessage = "* This field is required")]
        [Display(Name = "Active?")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "* This field is required")]
        [Display(Name = "Date Added")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true, NullDisplayText = "[-N/A-]")]
        public System.DateTime DateAdded { get; set; }
    }

    [MetadataType(typeof(ComputerMetadata))]
    public partial class Computer
    {

    }

    public class LocationMetadata
    {
        [Display(Name = "Location ID")]
        public int LocationId { get; set; }

        [Required(ErrorMessage = "* This field is required")]
        [StringLength(50, ErrorMessage = "* Value must be 50 characters or less!")]
        [Display(Name = "Location Name")]
        public string LocationName { get; set; }

        [Required(ErrorMessage = "* This field is required")]
        [StringLength(100, ErrorMessage = "* Value must be 100 characters or less!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "* This field is required")]
        [StringLength(100, ErrorMessage = "* Value must be 100 characters or less!")]
        public string City { get; set; }

        [Required(ErrorMessage = "* This field is required")]
        [StringLength(2, ErrorMessage = "* Value must be 2 characters or less!")]
        public string State { get; set; }

        [Required(ErrorMessage = "* This field is required")]
        [StringLength(5, ErrorMessage = "* Value must be 5 characters or less!")]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "* This field is required")]
        [Range(0, 2)]
        [Display(Name = "Reservation Limit")]
        public byte ReservationLimit { get; set; }
    }

    [MetadataType(typeof(LocationMetadata))]
    public partial class Location
    {

    }

    public class ReservationMetadata
    {
        [Display(Name = "Reservation ID")]
        [Required(ErrorMessage = "* This field is required")]
        public int ReservationId { get; set; }

        [Display(Name = "Computer ID")]
        [Required(ErrorMessage = "* This field is required")]
        public int ComputerId { get; set; }

        [Display(Name = "Location ID")]
        [Required(ErrorMessage = "* This field is required")]
        public int LocationId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true, NullDisplayText = "[-N/A-]")]
        [Display(Name = "Reservation Date")]
        public System.DateTime ReservationDate { get; set; }
    }

    [MetadataType(typeof(ReservationMetadata))]
    public partial class Reservation
    {

    }

}
