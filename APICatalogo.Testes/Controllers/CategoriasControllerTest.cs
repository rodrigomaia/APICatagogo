using System;
using System.Linq.Expressions;
using APICatalogo.Controllers;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace APICatalogo.Testes.Controllers;

public class CategoriasControllerTest
{
    Doubles.UnitOfWork uow;
    IMapper mapper;
    CategoriasController controller;
    ICategoriaRepository repository;

    [SetUp]
    public void Setup()
    {
        mapper = Substitute.For<IMapper>();
        repository = Substitute.For<ICategoriaRepository>();
        uow = new Doubles.UnitOfWork { CategoriaRepository = repository };
        controller = new CategoriasController(uow, mapper);
    }

    [Test]
    public void Get_OK()
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

    [Test]
    public void Get_Id_OK()
    {
        var categoria = new Categoria() { Nome = "Teste", ImagemUrl = "Teste" };
        var categoriaDTO = new CategoriaDTO() { Nome = "Teste", ImagemUrl = "Teste" };

        repository.Get(Arg.Any<Expression<Func<Categoria, bool>>>()).Returns(categoria);
        mapper.Map<CategoriaDTO>(categoria).Returns(categoriaDTO);
        var retorno = controller.Get(1).Result as OkObjectResult;

        Assert.That(retorno!.Value, Is.EqualTo(categoriaDTO));
    }

    [Test]
    public void Get_Id_CategoriaInexistente()
    {
        repository.Get(Arg.Any<Expression<Func<Categoria, bool>>>()).ReturnsNull();

        var retorno = controller.Get(1).Result;
        Assert.That(retorno, Is.InstanceOf<NotFoundObjectResult>());
    }

    [Test]
    public void Post_OK()
    {
        var categoriaDTO = new CategoriaDTO() { Nome = "Teste", ImagemUrl = "Teste" };
        var categoria = new Categoria() { Nome = "Teste", ImagemUrl = "Teste" };

        mapper.Map<Categoria>(categoriaDTO).Returns(categoria);
        repository.Create(categoria);

        var retorno = controller.Post(categoriaDTO).Result as CreatedAtRouteResult;

        Assert.That(retorno!.Value, Is.EqualTo(categoriaDTO));
        Assert.That(uow.commitInvocado, Is.EqualTo(1));
    }

    [Test]
    public void Post_Null()
    {
        var retorno = controller.Post(null).Result;

        Assert.That(retorno, Is.InstanceOf<BadRequestResult>());
        Assert.That(uow.commitInvocado, Is.Zero);
    }

    [Test]
    public void Put_OK()
    {
        var categoriaDTO = new CategoriaDTO() { Id = 1, Nome = "Teste", ImagemUrl = "Teste" };
        var categoria = new Categoria() { Nome = "Teste", ImagemUrl = "Teste" };

        mapper.Map<Categoria>(categoriaDTO).Returns(categoria);
        repository.Update(categoria);

        var retorno = controller.Put(1, categoriaDTO).Result as OkObjectResult;

        Assert.That(retorno!.Value, Is.EqualTo(categoriaDTO));
        Assert.That(uow.commitInvocado, Is.EqualTo(1));
    }

    [Test]
    public void Put_IdDiferente()
    {
        var categoriaDTO = new CategoriaDTO() { Id = 1, Nome = "Teste", ImagemUrl = "Teste" };
        var retorno = controller.Put(2, categoriaDTO).Result;

        Assert.That(retorno, Is.InstanceOf<BadRequestResult>());
        Assert.That(uow.commitInvocado, Is.Zero);
    }

    [Test]
    public void Delete_OK()
    {
        var categoriaDTO = new CategoriaDTO() { Id = 1, Nome = "Teste", ImagemUrl = "Teste" };
        var categoria = new Categoria() { Nome = "Teste", ImagemUrl = "Teste" };

        repository.Get(Arg.Any<Expression<Func<Categoria, bool>>>()).Returns(categoria);
        mapper.Map<CategoriaDTO>(categoria).Returns(categoriaDTO);
        repository.Delete(categoria);

        var retorno = controller.Delete(1).Result as OkObjectResult;

        Assert.That(retorno!.Value, Is.EqualTo(categoriaDTO));
        Assert.That(uow.commitInvocado, Is.EqualTo(1));
    }

    [Test]
    public void Delete_IdInexistente()
    {
        repository.Get(Arg.Any<Expression<Func<Categoria, bool>>>()).ReturnsNull();

        var retorno = controller.Delete(1).Result;

        Assert.That(retorno, Is.InstanceOf<NotFoundObjectResult>());
        Assert.That(uow.commitInvocado, Is.Zero);
    }

}