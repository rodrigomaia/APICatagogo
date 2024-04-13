using APICatalogo.DTOs;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

[Route("api/[controller]")]
[ApiController]
[ServiceFilter(typeof(ApiLoggingFilter))]
public class ProdutosController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
{
    private IProdutoRepository ProdutoRepository => unitOfWork.ProdutoRepository;

    [HttpGet]
    public ActionResult<IEnumerable<ProdutoDTO>> Get()
    {
        var produtos = mapper.Map<IEnumerable<ProdutoDTO>>(ProdutoRepository.GetAll());
        if (produtos is null)
        {
            return NotFound();
        }
        return Ok(produtos);
    }

    [HttpGet("{id:int}", Name = "ObterProduto")]
    public ActionResult<ProdutoDTO> Get(int id)
    {
        var produto = ProdutoRepository.Get(p => p.Id == id);
        if (produto is null)
        {
            return NotFound("Produto não encontrado...");
        }
        return Ok(mapper.Map<ProdutoDTO>(produto));
    }

    [HttpPost]
    public ActionResult<ProdutoDTO> Post(ProdutoDTO produtoDTO)
    {
        if (produtoDTO is null)
            return BadRequest();

        var produto = mapper.Map<Produto>(produtoDTO);

        ProdutoRepository.Create(produto);
        unitOfWork.Commit();

        return new CreatedAtRouteResult("ObterProduto",
            new { id = produtoDTO.Id }, produtoDTO);
    }

    [HttpPut("{id:int}")]
    public ActionResult<ProdutoDTO> Put(int id, ProdutoDTO produtoDTO)
    {
        if (id != produtoDTO.Id)
        {
            return BadRequest();
        }

        var produto = mapper.Map<Produto>(produtoDTO);

        ProdutoRepository.Update(produto);
        unitOfWork.Commit();

        return Ok(produtoDTO);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<ProdutoDTO> Delete(int id)
    {

        var produto = ProdutoRepository.Get(p => p.Id == id);
        if (produto is null) return NotFound("Produto não encontrado...");

        ProdutoRepository.Delete(produto);
        unitOfWork.Commit();

        return Ok(mapper.Map<ProdutoDTO>(produto));
    }
}
