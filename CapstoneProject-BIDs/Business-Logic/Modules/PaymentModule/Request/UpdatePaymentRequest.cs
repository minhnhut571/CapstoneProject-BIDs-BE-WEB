using FluentValidation;
using System;

namespace Business_Logic.Modules.PaymentModule.Request
{
    public class UpdatePaymentRequest
    {
        public Guid PaymentId { get; set; }
        public Guid UserId { get; set; }
        public Guid ItemId { get; set; }
        public string Detail { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
    }
    public class UpdatePaymentRequestValidator : AbstractValidator<UpdatePaymentRequest>
    {
        public UpdatePaymentRequestValidator()
        {
            RuleFor(x => x.PaymentId).NotEmpty().NotNull();
            RuleFor(x => x.UserId).NotEmpty().NotNull();
            RuleFor(x => x.ItemId).NotEmpty().NotNull();
            RuleFor(x => x.Detail).NotEmpty().NotNull();
            RuleFor(x => x.Amount).NotEmpty().NotNull();
            RuleFor(x => x.Date).NotEmpty().NotNull();
            RuleFor(x => x.Status).NotEmpty().NotNull();
        }
    }
}
