using Microsoft.EntityFrameworkCore;
using MovieInfo.Application.Common.Interfaces.Repositories;
using MovieInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Infraestructure.Persistence.Repositories;
public class SubscriptionRepository : GenericRepository<Subscription>, ISubscriptionRepository
{
    private readonly DbSet<Subscription> _subscriptions;
    public SubscriptionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _subscriptions = dbContext.Set<Subscription>();
    }
}
