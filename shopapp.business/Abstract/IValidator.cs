namespace shopapp.business.Abstract
{
    public interface IValidator<T>
    {
        string? Message { get; set; }
        bool Validation (T entity);
    }
}