using Business_Logic.Modules.PaymentModule.Interface;
using Business_Logic.Modules.PaymentModule.Request;
using Business_Logic.Modules.UserModule.Interface;
using Data_Access.Constant;
using Data_Access.Entities;
using FluentValidation.Results;

namespace Business_Logic.Modules.PaymentModule
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _PaymentRepository;
        public PaymentService(IPaymentRepository PaymentRepository)
        {
            _PaymentRepository = PaymentRepository;
        }

        public async Task<ICollection<Payment>> GetAll()
        {
            return await _PaymentRepository.GetAll(options: o => o.OrderByDescending(x => x.Date).ToList());
        }

        public Task<ICollection<Payment>> GetPaymentsIsValid()
        {
            return _PaymentRepository.GetPaymentsBy(x => x.Status == true, options: o => o.OrderByDescending(x => x.Date).ToList());
        }

        public async Task<Payment> GetPaymentByID(Guid? id)
        {
            if (id == null)
            {
                throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
            }
            var Payment = await _PaymentRepository.GetFirstOrDefaultAsync(x => x.PaymentId == id);
            if (Payment == null)
            {
                throw new Exception(ErrorMessage.PaymentError.PAYMENT_NOT_FOUND);
            }
            return Payment;
        }

        public async Task<Payment> GetPaymentByUserID(Guid? id)
        {
            if (id == null)
            {
                throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
            }
            var Payment = await _PaymentRepository.GetFirstOrDefaultAsync(x => x.UserId == id);
            if (Payment == null)
            {
                throw new Exception(ErrorMessage.PaymentError.PAYMENT_NOT_FOUND);
            }
            return Payment;
        }

        public async Task<Guid?> AddNewPayment(CreatePaymentRequest PaymentRequest)
        {

            ValidationResult result = new CreatePaymentRequestValidator().Validate(PaymentRequest);
            if (!result.IsValid)
            {
                throw new Exception(ErrorMessage.CommonError.INVALID_REQUEST);
            }

            //Payment PaymentCheck = _PaymentRepository.GetFirstOrDefaultAsync(x => x.PaymentName == PaymentRequest.PaymentName).Result;

            //if (PaymentCheck != null)
            //{
            //    throw new Exception(ErrorMessage.PaymentError.ITEM_TYPE_EXISTED);
            //}

            var newPayment = new Payment();

            newPayment.PaymentId = Guid.NewGuid();
            newPayment.ItemId = PaymentRequest.ItemId;
            newPayment.UserId = PaymentRequest.UserId;
            newPayment.Detail = PaymentRequest.Detail;
            newPayment.Amount = PaymentRequest.Amount;
            newPayment.Date = DateTime.Now;
            newPayment.Status = true;

            await _PaymentRepository.AddAsync(newPayment);
            return newPayment.PaymentId;
        }

        public async Task UpdatePayment(UpdatePaymentRequest PaymentRequest)
        {
            try
            {
                var PaymentUpdate = GetPaymentByID(PaymentRequest.PaymentId).Result;

                if (PaymentUpdate == null)
                {
                    throw new Exception(ErrorMessage.PaymentError.PAYMENT_NOT_FOUND);
                }

                ValidationResult result = new UpdatePaymentRequestValidator().Validate(PaymentRequest);
                if (!result.IsValid)
                {
                    throw new Exception(ErrorMessage.CommonError.INVALID_REQUEST);
                }

                //Payment PaymentCheck = _PaymentRepository.GetFirstOrDefaultAsync(x => x.PaymentName == PaymentRequest.PaymentName).Result;

                //if (PaymentCheck != null)
                //{
                //    throw new Exception(ErrorMessage.PaymentError.ITEM_TYPE_EXISTED);
                //}

                PaymentUpdate.ItemId = PaymentRequest.ItemId;
                PaymentUpdate.UserId = PaymentRequest.UserId;
                PaymentUpdate.Detail = PaymentRequest.Detail;
                PaymentUpdate.Amount = PaymentRequest.Amount;
                PaymentUpdate.Date = PaymentRequest.Date;
                PaymentUpdate.Status = PaymentRequest.Status;

                await _PaymentRepository.UpdateAsync(PaymentUpdate);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at update type: " + ex.Message);
                throw new Exception(ex.Message);
            }

        }

        public async Task DeletePayment(Guid? PaymentDeleteID)
        {
            try
            {
                if (PaymentDeleteID == null)
                {
                    throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
                }

                Payment PaymentDelete = _PaymentRepository.GetFirstOrDefaultAsync(x => x.PaymentId == PaymentDeleteID && x.Status == true).Result;

                if (PaymentDelete == null)
                {
                    throw new Exception(ErrorMessage.PaymentError.PAYMENT_NOT_FOUND);
                }

                PaymentDelete.Status = false;
                await _PaymentRepository.UpdateAsync(PaymentDelete);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at delete type: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

    }
}
