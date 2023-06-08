using FluentValidation;
using System;

namespace Business_Logic.Modules.ItemTypeModule.Request
{
    public class CreateItemTypeRequest
    {
        public string ItemTypeName { get; set; }
    }
    public class CreateItemTypeRequestValidator : AbstractValidator<CreateItemTypeRequest>
    {
        public CreateItemTypeRequestValidator()
        {
            RuleFor(x => x.ItemTypeName).NotEmpty().NotNull();
        }
    }
}
