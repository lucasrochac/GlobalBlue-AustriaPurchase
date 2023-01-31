using GlobalBlue_AustriaPurchases.DomainModel.ValueObject.Enum;

namespace GlobalBlue_AustriaPurchases.DomainModel.Domain.Model
{
    public class Net : Purchase
    {
        public override decimal? Value { get; }

        public Net(decimal? value) => Value = value;

        public override ResultDTO Get(PurchaseRateEnum purchaseRate)
        {
            var v = Value * (decimal)purchaseRate / 100;
            var g = Value + v;

            var resultDto = new ResultDTO();           
            resultDto.Gross = g;
            resultDto.Vat = v;
            resultDto.Net = Value;

            return resultDto;
        }
    }
}
