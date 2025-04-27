using FluentValidation.TestHelper;
using SampleCqrsDapperApi.Application.Commands;
using SampleCqrsDapperApi.Application.Validators;
using Xunit;

namespace SampleCqrsDapperApi.Tests.Validators
{
    public class CreateCustomerCommandValidatorTests
    {
        private readonly CreateCustomerCommandValidator _validator;

        public CreateCustomerCommandValidatorTests()
        {
            _validator = new CreateCustomerCommandValidator();
        }

        [Fact]
        public void Should_Pass_When_ValidData()
        {
            var model = new CreateCustomerCommand
            {
                Name = "Josh",
                Email = "josh@example.com"
            };

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Should_Fail_When_Name_Is_Empty()
        {
            var model = new CreateCustomerCommand
            {
                Name = "",
                Email = "josh@example.com"
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_Fail_When_Email_Is_Empty()
        {
            var model = new CreateCustomerCommand
            {
                Name = "Josh",
                Email = ""
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Fail_When_Email_Format_Is_Invalid()
        {
            var model = new CreateCustomerCommand
            {
                Name = "Josh",
                Email = "invalid-email"
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }
    }
}
