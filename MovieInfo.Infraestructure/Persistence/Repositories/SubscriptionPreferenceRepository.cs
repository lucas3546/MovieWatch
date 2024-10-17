using Microsoft.EntityFrameworkCore;
using MovieInfo.Application.Common.Interfaces.Repositories;
using MovieInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieInfo.Infraestructure.Persistence.Repositories
{
    public class SubscriptionPreferenceRepository : GenericRepository<SubscriptionPreference>, ISubscriptionPreferenceRepository
    {
        private readonly DbSet<SubscriptionPreference> _subscriptionsPreferences;
        public SubscriptionPreferenceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _subscriptionsPreferences = dbContext.Set<SubscriptionPreference>();
        }
    }
}
