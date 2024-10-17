using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Domain.Entities
{
    public class SubscriptionPreference
    {
        public SubscriptionPreference() { }
        public SubscriptionPreference(string id, string title, string description, decimal price)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
        }
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
