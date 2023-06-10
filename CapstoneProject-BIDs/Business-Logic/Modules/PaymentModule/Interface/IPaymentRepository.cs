using Common.Utils.Repository.Interface;
using Data_Access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Modules.PaymentModule.Interface
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        public Task<ICollection<Payment>> GetPaymentsBy(
               Expression<Func<Payment, bool>> filter = null,
               Func<IQueryable<Payment>, ICollection<Payment>> options = null,
               string includeProperties = null
           );
    }
}
