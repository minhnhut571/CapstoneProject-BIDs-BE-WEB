﻿using FluentValidation;
using System;

namespace Business_Logic.Modules.ItemModule.Request
{
    public class CreateItemRequest
    {
        public Guid UserId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public Guid ItemTypeId { get; set; }
        public Guid Quantity { get; set; }
        public string Image { get; set; }
        public double FristPrice { get; set; }
        public double StepPrice { get; set; }
    }
    public class CreateItemRequestValidator : AbstractValidator<CreateItemRequest>
    {
        public CreateItemRequestValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().NotNull();
            RuleFor(x => x.ItemName).NotEmpty().NotNull();
            RuleFor(x => x.Description).NotEmpty().NotNull();
            RuleFor(x => x.ItemTypeId).NotEmpty().NotNull();
            RuleFor(x => x.Quantity).NotEmpty().NotNull();
            RuleFor(x => x.Image).NotEmpty().NotNull();
            RuleFor(x => x.FristPrice).NotEmpty().NotNull();
            RuleFor(x => x.StepPrice).NotEmpty().NotNull();
        }
    }
}
