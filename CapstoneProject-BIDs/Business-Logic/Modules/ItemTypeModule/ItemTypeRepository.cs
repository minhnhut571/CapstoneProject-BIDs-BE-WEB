using Business_Logic.Modules.ItemTypeModule.Interface;
using Common.Utils.Repository;
using Data_Access.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Modules.ItemTypeModule
{
    public class ItemTypeRepository : Repository<ItemType>, IItemTypeRepository
    {
        private readonly BidsContext _db;

        public ItemTypeRepository(BidsContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ICollection<ItemType>> GetItemTypesBy(
            Expression<Func<ItemType, bool>> filter = null,
            Func<IQueryable<ItemType>, ICollection<ItemType>> options = null,
            string includeProperties = null
        )
        {
            IQueryable<ItemType> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return options != null ? options(query).ToList() : await query.ToListAsync();
        }
    }
}
