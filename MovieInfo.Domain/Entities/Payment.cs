using MovieInfo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Domain.Entities;
public class Payment
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaidIn { get; set; }
    public PaymentStatus Status { get; set; }

    public int SubscriptionId { get; set; }
    public Subscription Subscription { get; set; }
}
