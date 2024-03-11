namespace RiaTest.Application.DTOs
{
    public class GenericResultDTO<TEntity> : ResultDTO
    {
        public TEntity Result { get; set; }
    }
}
