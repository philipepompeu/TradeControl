namespace TradeControl
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entidade, Guid id) : base($"Não foi possível localizar um registro com identificador {id} para entidade {entidade}") { }        
    }
}
