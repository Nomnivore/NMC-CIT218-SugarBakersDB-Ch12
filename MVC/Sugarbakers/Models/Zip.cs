using System;
using System.Collections.Generic;

namespace Sugarbakers.Models;

public partial class Zip
{
    public string Zipcode { get; set; } = null!;

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; } = new List<Customer>();
}
