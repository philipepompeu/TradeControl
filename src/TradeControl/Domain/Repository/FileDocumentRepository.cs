namespace TradeControl.Domain.Repository
{
    public class FileDocumentRepository : Repository<Model.FileDocument>, IFileDocumentRepository
    {
        public FileDocumentRepository(TradeControlDbContext context) : base(context)
        {
        }
    }
}
