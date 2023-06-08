using FluentValidation;
using System;

namespace Business_Logic.Modules.SessionModule.Request
{
    public class UpdateSessionRequest
    {
        public Guid SessionId { get; set; }

        public string SessionName { get; set; }

        public Guid ItemTypeId { get; set; }

        public DateTime BeginTime { get; set; }

        public int AuctionTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool Status { get; set; }
    }
    public class UpdateSessionRequestValidator : AbstractValidator<UpdateSessionRequest>
    {
        public UpdateSessionRequestValidator()
        {
            RuleFor(x => x.SessionId).NotEmpty().NotNull();
            RuleFor(x => x.SessionName).NotEmpty().NotNull();
            RuleFor(x => x.ItemTypeId).NotEmpty().NotNull();
            RuleFor(x => x.BeginTime).NotEmpty().NotNull();
            RuleFor(x => x.AuctionTime).NotEmpty().NotNull();
            RuleFor(x => x.EndTime).NotEmpty().NotNull();
            RuleFor(x => x.Status).NotEmpty().NotNull();
        }
    }
}
