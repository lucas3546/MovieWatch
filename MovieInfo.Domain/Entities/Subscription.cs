using MovieInfo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Domain.Entities
{
    public class Subscription
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public SubscriptionState State { get; set; }

        public List<Payment>? Payments { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
