using System;
using System.Collections.Generic;

namespace Sugarbakers.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Address { get; set; }

    public string? Zipcode { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Extension { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual ICollection<Payment> Payments { get; } = new List<Payment>();

    public virtual Zip? ZipcodeNavigation { get; set; }
}
