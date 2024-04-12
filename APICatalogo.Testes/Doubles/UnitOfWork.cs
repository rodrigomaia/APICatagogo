using APICatalogo.Repositories;

namespace APICatalogo.Testes.Doubles;

public class UnitOfWork : IUnitOfWork
{
    public IProdutoRepository ProdutoRepository {get; set;}

    public ICategoriaRepository CategoriaRepository {get; set;}

    public Task Commit() => Task.CompletedTask;

    public Task Rollback() => Task.CompletedTask;
}