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
public class CategoriasController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
{
    private ICategoriaRepository CategoriaRepository => unitOfWork.CategoriaRepository;

    [HttpGet]    
    public ActionResult<IEnumerable<CategoriaDTO>> Get()
    {
        var categoriasDTO = mapper.Map<IEnumerable<CategoriaDTO>>(CategoriaRepository.GetAll());
        return Ok(categoriasDTO);
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<CategoriaDTO> Get(int id)
    {
        var categoria = CategoriaRepository.Get(c => c.Id == id);

        if (categoria == null)
        {
            return NotFound("Categoria não encontrada...");
        }

        var categoriaDTO = mapper.Map<CategoriaDTO>(categoria);

        return Ok(categoriaDTO);
    }

    [HttpPost]
    public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDTO)
    {
        if (categoriaDTO is null)
            return BadRequest();

        var categoria = mapper.Map<Categoria>(categoriaDTO);

        CategoriaRepository.Create(categoria);
        unitOfWork.Commit();

        return new CreatedAtRouteResult("ObterCategoria",
            new { id = categoriaDTO.Id }, categoriaDTO);
    }

    [HttpPut("{id:int}")]
    public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDTO)
    {
        if (id != categoriaDTO.Id)
        {
            return BadRequest();
        }
        
        var categoria = mapper.Map<Categoria>(categoriaDTO);
        
        CategoriaRepository.Update(categoria);
        unitOfWork.Commit();
        return Ok(categoriaDTO);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<CategoriaDTO> Delete(int id)
    {
        var categoria = CategoriaRepository.Get(c => c.Id == id);
        if (categoria is null) return NotFound("Categoria não encontrada...");

        CategoriaRepository.Delete(categoria);
        unitOfWork.Commit();
        var categoriaDTO = mapper.Map<CategoriaDTO>(categoria);

        return Ok(categoriaDTO);
    }
}