using System.Linq.Expressions;
using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    private readonly AppDbContext? context;

    public ProdutoRepository(AppDbContext context) : base(context)
    {
    }
}