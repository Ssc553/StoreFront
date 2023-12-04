using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace StoreFront.DATA.EF.Models
{
    [ModelMetadataType(typeof(CategoryMetadata))]
    public partial class Category{ }


    [ModelMetadataType(typeof(ItemMetadata))]
    public partial class Item { }


    [ModelMetadataType(typeof(ItemType))]
    public partial class ItemType { }

    [ModelMetadataType(typeof(Order))]
    public partial class Order { }

    [ModelMetadataType(typeof(OrderDetail))]
    public partial class OrderDetail { }

    [ModelMetadataType(typeof(ServerMode))]
    public partial class ServerMode { }

    [ModelMetadataType(typeof(User))]
    public partial class User { }



}

