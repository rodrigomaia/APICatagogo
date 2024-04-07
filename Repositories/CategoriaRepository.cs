using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    private readonly AppDbContext? context;

    public CategoriaRepository(AppDbContext context) : base(context)
    {
    }
}