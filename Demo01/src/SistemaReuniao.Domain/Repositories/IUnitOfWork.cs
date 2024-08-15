namespace SistemaReuniao.Domain.Repositories;

    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
