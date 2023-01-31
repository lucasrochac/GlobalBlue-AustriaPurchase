using GlobalBlue_AustriaPurchases.DomainModel.ValueObject.Enum;
using GlobalBlue_AustriaPurchases.API.Presentation;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace GlobalBlue_AustriaPurchases.API.Queries.CalculatePurchase
{
    public class CalculatePurchaseQuery : IRequest<CalculatePurchasePresentation>, IValidatableObject
    {
        public decimal? Net { get; set; }
        public decimal? Gross { get; set; }
        public decimal? Vat { get; set; }

        public PurchaseRateEnum PurchaseRate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            
            if(MissingOrInvalidParameterValidation())
                results.Add(new ValidationResult("At least one of the options must be filled."));

            if(MoreThanOneParameterFilledValidation())
                results.Add(new ValidationResult("Only one property (Net, Gross or VAT) should be filled."));

            if (FilledValueIsValid())
                results.Add(new ValidationResult("At least one of the options must be filled."));

            if(PurchaseRateIsValid())
                results.Add(new ValidationResult("Invalid Purchase Rate."));

            return results;
        }

        private bool MissingOrInvalidParameterValidation()
        {
            return (!Net.HasValue && !Gross.HasValue && !Vat.HasValue);
        }

        private bool MoreThanOneParameterFilledValidation()
        {
            int filledPropertiesCount = 0;

            if (Net.HasValue) filledPropertiesCount++;
            if (Gross.HasValue) filledPropertiesCount++;
            if (Vat.HasValue) filledPropertiesCount++;

            return (filledPropertiesCount != 1);
        }

        private bool FilledValueIsValid()
        {
            return (Net.HasValue && Net.Value <= 0) || (Gross.HasValue && Gross.Value <= 0) || (Vat.HasValue && Vat.Value <= 0);
        }

        private bool PurchaseRateIsValid()
        {
            var purchaseRateList = Enum.GetValues(typeof(PurchaseRateEnum)).Cast<PurchaseRateEnum>();
            return !purchaseRateList.Contains(PurchaseRate);
        }
    }
}
