using Book4H2Ten.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.EntityFrameWorkCore.EFExtensions
{
    public static class ChangeTrackerExtensions
    {
        public static void SetAuditProperties(this ChangeTracker changeTracker)
        {
            changeTracker.DetectChanges();
            IEnumerable<EntityEntry> entities =
                changeTracker
                    .Entries()
                    .Where(t => t.Entity is BaseEntity && t.State == EntityState.Deleted);

            if (entities.Any())
            {
                foreach (EntityEntry entry in entities)
                {
                    BaseEntity entity = (BaseEntity)entry.Entity;
                    entity.IsDeleted = true;
                    entity.DeletionTime = DateTime.UtcNow;
                    entry.State = EntityState.Modified;
                }
            }
        }
    }
}
