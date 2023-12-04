using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreFront.DATA.EF.Models
{
    public class CategoryMetadata
    {
        [Key]
        public int ItemCategoryId { get; set; }

        
        [StringLength( 500)]
        [Display(Name = "Description")]
        [Unicode(false)]
        [UIHint("MultilineText")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "* Required")]
        [StringLength(25)]
        [Display(Name = "Category")]
        [Unicode(false)]
        public string CategoryName { get; set; } = null!;

        [Display(Name = "Image")]
        public byte[]? Picture { get; set; }

        public virtual ICollection<Item>? Items { get; set; }
    }
    
    public class ItemMetadata
    {
        [Key]
        [Display(Name = "Item ID")]
        public int ItemId { get; set; }

        [Required]
        public int ItemTypeId { get; set; }

        [Required]
        [StringLength( 50)]
        [Display(Name = "Item Name")]
        public string Name { get; set; } = null!;

        [Required]
        [Display(Name = "Required Level")]
        public int RequiredLevel { get; set; }

        [Required]
        [Display(Name = "Max Stack Size")]
        public int MaxStackSize { get; set; }

        [Display(Name = "Unique?")]
        public bool? IsUnique { get; set; }

        [Display(Name = "Set Item?")]
        public bool? IsSetItem { get; set; }

        [Display(Name = "Socketed?")]
        public bool? IsSocketed { get; set; }

        [Required]
        [StringLength ( 50)]
        [Display(Name = "Rarity")]
        public string Rarity { get; set; } = null!;

        [Required]
        [Display(Name = "Item Category")]
        public int ItemCategoryId { get; set; }

        [Required]
        [Display(Name = "Server Id")]
        public int ServerId { get; set; }

        [Required]
        [Display(Name = "Items in Stock")]
        public short ItemsInStock { get; set; }


        [DataType(DataType.Currency)]
        [Display(Name = "Price")]
        [DefaultValue(0)]
        public decimal Price { get; set; }

        [StringLength(75)]
        [Display(Name = "Image")]
        public string? ItemImage { get; set; }
    }


    public class ItemTypeMetadata
    {
        [Key]
        public int ItemTypeId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Item Type")]
        public string ItemType { get; set; } = null!;

        public virtual ICollection<Item>? Items { get; set; }
    }

    public class OrdersMetadata
    {
        [Key]
        [Display(Name = "Order ID")]
        public int OrderId { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "User Id")]
        public string UserId { get; set; } = null!;

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Ship Date")]
        public DateTime ShipDate { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Server")]
        public string ShipServer { get; set; } = null!;

        [Required]
        [StringLength(50)]
        [Display(Name = "Account Name")]
        public string ShipAccountName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        [Display(Name = "Country")]
        public string ShipAccountCountry { get; set; } = null!;


        public virtual User? User { get; set; } = null!;


        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    }

    public class OrderDetailMetadata
    {
        [Key]
        [Display(Name = "Order ID")]
        public int OrderId { get; set; }

        [Required]
        [Display(Name = "Item ID")]
        public int ItemId { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal ItemPrice { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public short Quantity { get; set; }

        [Required]
        [Display(Name = "Discount")]
        public float Discount { get; set; }

        [Required]
        public int OrderDetailsId { get; set; }


        public virtual Item? Item { get; set; } = null!;


        public virtual Order? Order { get; set; } = null!;
    }

    public class ServerModeMetadata
    {
        [Key]
        public int ServerId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Server")]
        public string ServerName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        [Display(Name = "Mode")]
        public string ModeName { get; set; } = null!;

    }

    public class UserMetadata
    {
        [Key]
        [StringLength(128)]
        [Display(Name = "Description")]
        public string UserId { get; set; } = null!;

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(25)]
        [Display(Name = "Account Name")]
        public string AccountName { get; set; } = null!;

        [Required]
        [StringLength(15)]
        [Display(Name = "Server")]
        public string Server { get; set; } = null!;

        [Required]
        [StringLength(50)]
        [Display(Name = "Country")]
        public string Country { get; set; } = null!;


        public virtual ICollection<Order>? Orders { get; set; }
    }
}



