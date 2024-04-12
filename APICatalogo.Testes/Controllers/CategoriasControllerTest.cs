using APICatalogo.Controllers;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
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
        uow = Substitute.For<IUnitOfWork>();
        mapper = Substitute.For<IMapper>();
        controller = new CategoriasController(uow, mapper);
        repository = Substitute.For<ICategoriaRepository>();
    }

    [Test]
    public void GetOK()
    {   
        IEnumerable<CategoriaDTO> categoriaDTOs= new List<CategoriaDTO>();
        repository.GetAll().Returns(new List<Categoria>());
        mapper.Map<IEnumerable<CategoriaDTO>>(Arg.Any<Categoria>()).Returns(categoriaDTOs);
        var retorno = controller.Get();

        Assert.That(retorno.Result, Is.EqualTo(categoriaDTOs));
    }
}