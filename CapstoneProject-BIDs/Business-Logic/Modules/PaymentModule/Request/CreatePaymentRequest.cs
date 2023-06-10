using FluentValidation;
using System;

namespace Business_Logic.Modules.PaymentModule.Request
{
    public class CreatePaymentRequest
    {
        public Guid UserId { get; set; }
        public Guid ItemId { get; set; }
        public string Detail { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
    }
    public class CreatePaymentRequestValidator : AbstractValidator<CreatePaymentRequest>
    {
        public CreatePaymentRequestValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().NotNull();
            RuleFor(x => x.ItemId).NotEmpty().NotNull();
            RuleFor(x => x.Detail).NotEmpty().NotNull();
            RuleFor(x => x.Amount).NotEmpty().NotNull();
            RuleFor(x => x.Date).NotEmpty().NotNull();
        }
    }
}
