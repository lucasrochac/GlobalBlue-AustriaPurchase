using GlobalBlue_AustriaPurchases.DomainModel.ValueObject.Enum;


namespace GlobalBlue_AustriaPurchases.DomainModel.Domain.Model
{
    public abstract class Purchase
    {
        public abstract decimal? Value { get; }
        public abstract ResultDTO Get(PurchaseRateEnum rate);
    }
}
