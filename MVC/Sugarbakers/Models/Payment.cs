using System;
using System.Collections.Generic;

namespace Sugarbakers.Models;

public partial class Payment
{
    public int CustomerId { get; set; }

    public DateTime PmtDate { get; set; }

    public decimal? Amt { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
