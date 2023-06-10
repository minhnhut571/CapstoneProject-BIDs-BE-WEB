using Business_Logic.Modules.PaymentModule.Interface;
using Common.Utils.Repository;
using Data_Access.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Modules.PaymentModule
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        private readonly BIDsContext _db;

        public PaymentRepository(BIDsContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ICollection<Payment>> GetPaymentsBy(
            Expression<Func<Payment, bool>> filter = null,
            Func<IQueryable<Payment>, ICollection<Payment>> options = null,
            string includeProperties = null
        )
        {
            IQueryable<Payment> query = DbSet;

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
