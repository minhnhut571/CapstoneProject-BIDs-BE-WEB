using FluentValidation;
using System;

namespace Business_Logic.Modules.SessionModule.Request
{
    public class CreateSessionRequest
    {
        public string SessionName { get; set; }

        public Guid ItemTypeId { get; set; }

        public DateTime BeginTime { get; set; }

        public int AuctionTime { get; set; }

        public DateTime EndTime { get; set; }
    }

        public class CreateSessionRequestValidator : AbstractValidator<CreateSessionRequest>
    {
        public CreateSessionRequestValidator()
        {
            RuleFor(x => x.SessionName).NotEmpty().NotNull();
            RuleFor(x => x.ItemTypeId).NotEmpty().NotNull();
            RuleFor(x => x.BeginTime).NotEmpty().NotNull();
            RuleFor(x => x.AuctionTime).NotEmpty().NotNull();
            RuleFor(x => x.EndTime).NotEmpty().NotNull();
        }
    }
}
