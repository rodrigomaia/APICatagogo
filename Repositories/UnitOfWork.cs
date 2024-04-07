
using APICatalogo.Context;

namespace APICatalogo.Repositories;

public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{   
    private IProdutoRepository? _produtoRepository;
    private ICategoriaRepository? _categoriaRepository;
    public IProdutoRepository ProdutoRepository 
        => _produtoRepository ??= new ProdutoRepository(dbContext);

    public ICategoriaRepository CategoriaRepository
        => _categoriaRepository ??= new CategoriaRepository(dbContext);

    public async Task Commit()
    {
        await dbContext.SaveChangesAsync();
    }

    public async Task Rollback()
    {
        await dbContext.DisposeAsync();
    }
}