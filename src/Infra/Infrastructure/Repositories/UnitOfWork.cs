using Application.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        RefreshTokensRepository = new RefreshTokenRepository(_context);
        TrailsRepository = new TrailsRepository(_context);
    }

    public IRefreshTokenRepository RefreshTokensRepository { get; }
    public ITrailsRepository TrailsRepository { get; }

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}