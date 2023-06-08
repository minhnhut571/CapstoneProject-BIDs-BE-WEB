using FluentValidation;
using System;

namespace Business_Logic.Modules.ItemTypeModule.Request
{
    public class UpdateItemTypeRequest
    {
        public Guid ItemTypeId { get; set; }

        public string ItemTypeName { get; set; }

        public bool Status { get; set; }
    }
    public class UpdateItemTypeRequestValidator : AbstractValidator<UpdateItemTypeRequest>
    {
        public UpdateItemTypeRequestValidator()
        {
            RuleFor(x => x.ItemTypeId).NotEmpty().NotNull();
            RuleFor(x => x.ItemTypeName).NotEmpty().NotNull();
            RuleFor(x => x.Status).NotEmpty().NotNull();
        }
    }
}
