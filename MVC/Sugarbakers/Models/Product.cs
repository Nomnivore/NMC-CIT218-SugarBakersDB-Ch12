using System;
using System.Collections.Generic;

namespace Sugarbakers.Models;

public partial class Product
{
    public int ProductsId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<ItemsonOrder> ItemsonOrders { get; } = new List<ItemsonOrder>();
}
