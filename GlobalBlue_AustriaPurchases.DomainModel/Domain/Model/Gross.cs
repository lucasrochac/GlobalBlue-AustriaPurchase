using GlobalBlue_AustriaPurchases.DomainModel.ValueObject.Enum;

namespace GlobalBlue_AustriaPurchases.DomainModel.Domain.Model
{
    public class Gross : Purchase
    {
        public override decimal? Value { get; }

        public Gross(decimal? value) => Value = value;

        public override ResultDTO Get(PurchaseRateEnum rate) {
            
            var vatRate = (decimal)rate / 100;
            var n = Value / (vatRate + 1);
            var v = Value * vatRate / (vatRate + 1);

            var net = Math.Round(n.GetValueOrDefault(),1);
            var vat = Math.Round(v.GetValueOrDefault(),1);

            var resultDto = new ResultDTO();

            resultDto.Gross = Value;
            resultDto.Vat = vat;
            resultDto.Net = net;            

            return resultDto;
        }
    }
}
