﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Data.Entity;

namespace AllReady.Models
{
    public partial class AllReadyDataAccessEF7 : IAllReadyDataAccess
    {
        IEnumerable<Campaign> IAllReadyDataAccess.Campaigns
        {
            get
            {
                return _dbContext.Campaigns
                   .Include(x => x.ManagingTenant)
                   .Include(x => x.Activities)
                   .Include(x => x.ParticipatingTenants)
                   .ToList();
            }
        }

        Campaign IAllReadyDataAccess.GetCampaign(int campaignId)
        {
            return _dbContext.Campaigns
                .Include(x => x.ManagingTenant)
                .Include(x => x.Activities)
                .Include(x => x.ParticipatingTenants)
                .SingleOrDefault(x => x.Id == campaignId);
        }

        Task IAllReadyDataAccess.AddCampaign(Campaign value)
        {
            _dbContext.Campaigns.Add(value);
            return _dbContext.SaveChangesAsync();
        }

        Task IAllReadyDataAccess.DeleteCampaign(int id)
        {
            var toDelete = _dbContext.Campaigns.Where(c => c.Id == id).SingleOrDefault();

            if (toDelete != null)
            {
                _dbContext.Campaigns.Remove(toDelete);
                return _dbContext.SaveChangesAsync();
            }
            return null;
        }

        Task IAllReadyDataAccess.UpdateCampaign(Campaign value)
        {
            _dbContext.Campaigns.Update(value);
            return _dbContext.SaveChangesAsync();
        }
    }
}
