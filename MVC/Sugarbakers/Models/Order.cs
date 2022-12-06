using System;
using System.Collections.Generic;

namespace Sugarbakers.Models;

public partial class Order
{
    public int OrdersId { get; set; }

    public int CustomerId { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal? FreightCharge { get; set; }

    public decimal? TotalDue { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<ItemsonOrder> ItemsonOrders { get; } = new List<ItemsonOrder>();
}
