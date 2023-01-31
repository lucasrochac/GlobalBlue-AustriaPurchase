using GlobalBlue_AustriaPurchases.DomainModel.ValueObject.Enum;

namespace GlobalBlue_AustriaPurchases.DomainModel.Domain.Model
{
    public class Vat : Purchase
    {
        public override decimal? Value { get; }       

        public Vat(decimal? value) => Value = value;

        public override ResultDTO Get(PurchaseRateEnum purchaseRate)
        {
            var net = Value * 100 / (decimal)purchaseRate;
            var gross = net + Value;
            
            var r = new ResultDTO();            
            r.Gross = gross;
            r.Vat = Value;
            r.Net = net;
            
            return r;
        }
    }
}
