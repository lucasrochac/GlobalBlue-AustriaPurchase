using GlobalBlue_AustriaPurchases.DomainModel.Domain.Model;
using GlobalBlue_AustriaPurchases.DomainModel.Domain;
using GlobalBlue_AustriaPurchases.API.Presentation;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace GlobalBlue_AustriaPurchases.API.Queries.CalculatePurchase
{
    public class CalculatePurchaseHandler : IRequestHandler<CalculatePurchaseQuery, CalculatePurchasePresentation>
    {
        public async Task<CalculatePurchasePresentation> Handle(CalculatePurchaseQuery request, CancellationToken cancellationToken)
        {
            var context = new ValidationContext(request, null, null);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(request, context, results, true))
            {
                throw new ValidationException(string.Join(", ", results.Select(r => r.ErrorMessage)));
            }

            Purchase purchase;

            if (request.Net.HasValue)
                purchase = new Net(request.Net.Value);
            else if (request.Gross.HasValue)
                purchase = new Gross(request.Gross.Value);
            else if (request.Vat.HasValue)
                purchase = new Vat(request.Vat.Value);
            else
                return null;

            return ToPresentation(purchase.Get(request.PurchaseRate));
        }

        private CalculatePurchasePresentation ToPresentation(ResultDTO dto)
        {
            return new CalculatePurchasePresentation {
                Net=dto.Net,
                Gross=dto.Gross,
                Vat=dto.Vat,
            };
        }
    }
}
