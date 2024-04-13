using APICatalogo.Repositories;

namespace APICatalogo.Testes.Doubles;

public class UnitOfWork : IUnitOfWork
{
    public int commitInvocado {get; set;} = 0;
    public IProdutoRepository ProdutoRepository {get; set;}

    public ICategoriaRepository CategoriaRepository {get; set;}

    public Task Commit(){
        commitInvocado++;
        return Task.CompletedTask;
    } 

    public Task Rollback() => Task.CompletedTask;
}