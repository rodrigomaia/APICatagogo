using APICatalogo.Controllers;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace APICatalogo.Testes.Controllers;

public class CategoriasControllerTest
{
    IUnitOfWork uow;
    IMapper mapper;
    CategoriasController controller;
    ICategoriaRepository repository;

    [SetUp]
    public void Setup()
    {        
        mapper = Substitute.For<IMapper>();
        repository = Substitute.For<ICategoriaRepository>();
        uow = new Doubles.UnitOfWork {CategoriaRepository = repository};
        controller = new CategoriasController(uow, mapper);
    }

    [Test]
    public void GetOK()
    {
        IEnumerable<CategoriaDTO> categoriaDTOs = new List<CategoriaDTO>(){
            new CategoriaDTO(){Nome = "Teste", ImagemUrl = "Teste"}
        };
        var categorias = new List<Categoria>();

        repository.GetAll().Returns(categorias);
        mapper.Map<IEnumerable<CategoriaDTO>>(categorias).Returns(categoriaDTOs);
        var retorno = controller.Get().Result as OkObjectResult;

        Assert.That(retorno!.Value, Is.EqualTo(categoriaDTOs));
    }
}