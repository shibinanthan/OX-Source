using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace OpenXposure.Web.Models
{
    public class ProductViewModel : ModelBase
    {
        public List<Product> Product { get; set; }
        public ProductViewModel()
        {
            this.Product = new List<Product>();
            this.UserSession = new XposureSession();
        }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Product : ModelBase
    {
        public string Id { get; set; }

        [Display(Name="Mr Title")]
        [Required, StringLength(50, ErrorMessage = "Title cannot be longer than 50 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        //[AllowHtml]
        public string Description { get; set; }

        [Required(ErrorMessage = "StartPrice is required")]
        [Range(1, 1000, ErrorMessage = "StartPrice should be between 1 to 10000")]
        public decimal StartPrice { get; set; }

        public decimal CurrentPrice { get; set; }

        [Required(ErrorMessage = "StartTime is required")]
        //[Range(typeof(DateTime), "1/1/2012", "12/31/2016", ErrorMessage = "StartTime should be between 1/1/2012 to 12/31/2016")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "EndTime is required")]
        public DateTime EndTime { get; set; }

        public bool IsEdit { get; set; }

        public List<Category> Category { get; set; }
        public ICollection<int> CategoryID { get; set; }
    }

    public class Order : ModelBase
    {
        public string Id { get; set; }

        [Display(Name = "Mr Title")]
        [Required, StringLength(50, ErrorMessage = "Title cannot be longer than 50 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [AllowHtml]
        public string Description { get; set; }

        [Required(ErrorMessage = "StartPrice is required")]
        [Range(1, 1000, ErrorMessage = "StartPrice should be between 1 to 10000")]
        public decimal StartPrice { get; set; }

        public decimal CurrentPrice { get; set; }

        [Required(ErrorMessage = "StartTime is required")]
        //[Range(typeof(DateTime), "1/1/2012", "12/31/2016", ErrorMessage = "StartTime should be between 1/1/2012 to 12/31/2016")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "EndTime is required")]
        public DateTime EndTime { get; set; }

        public bool IsEdit { get; set; }

        public List<Category> Category { get; set; }
        public ICollection<int> CategoryID { get; set; }
    }

    public class XposureUser : XposureBase
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class XposureBase
    {
        public string ConnectionString { get; set; }
    }
}