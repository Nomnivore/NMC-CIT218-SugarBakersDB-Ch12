using System;
using System.Collections.Generic;

namespace Sugarbakers.Models;

public partial class ItemsonOrder
{
    public int OrdersId { get; set; }

    public int ProductsId { get; set; }

    public int? Quantity { get; set; }

    public decimal? UnitPrice { get; set; }

    public decimal? LineItemTotal { get; set; }

    public DateTime? ShipDate { get; set; }

    public virtual Order Orders { get; set; } = null!;

    public virtual Product Products { get; set; } = null!;
}
