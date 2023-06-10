using Business_Logic.Modules.PaymentModule.Request;
using Data_Access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Modules.PaymentModule.Interface
{
    public interface IPaymentService
    {
        public Task<Guid?> AddNewPayment(CreatePaymentRequest PaymentCreate);

        public Task UpdatePayment(UpdatePaymentRequest PaymentUpdate);

        public Task DeletePayment(Guid? PaymentDeleteID);

        public Task<ICollection<Payment>> GetAll();

        public Task<Payment> GetPaymentByID(Guid? id);

        public Task<Payment> GetPaymentByUserID(Guid? id);
    }
}
