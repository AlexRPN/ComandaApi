using Comanda.Aplicacao.Transacoes.Interfaces;
using Comanda.Infra.Data;
using Microsoft.EntityFrameworkCore.Storage;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext appDbContext;
    private IDbContextTransaction? transaction;

    public UnitOfWork(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        return await appDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken)
    {
        transaction = await appDbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        if (transaction != null)
        {
            await transaction.CommitAsync(cancellationToken);
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
    {
        if (transaction != null)
        {
            await transaction.RollbackAsync(cancellationToken);
        }
    }
}