namespace TradeControl.Domain.Repository
{
    public class AssetRepository : Repository<Model.Asset>, IAssetRepository
    {
        public AssetRepository(TradeControlDbContext context) : base(context)
        {
        }
    }
}
