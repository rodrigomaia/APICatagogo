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

public class ProdutosControllerTest
{
    Doubles.UnitOfWork uow;
    IMapper mapper;
    ProdutosController controller;
    IProdutoRepository repository;

    [SetUp]
    public void Setup()
    {
        mapper = Substitute.For<IMapper>();
        repository = Substitute.For<IProdutoRepository>();
        uow = new Doubles.UnitOfWork { ProdutoRepository = repository };
        controller = new ProdutosController(uow, mapper);
    }

    [Test]
    public void Get_OK()
    {
        IEnumerable<ProdutoDTO> produtoDTOs = new List<ProdutoDTO>(){
            new ProdutoDTO(){Nome = "Teste", ImagemUrl = "Teste"}
        };
        var produtos = new List<Produto>();

        repository.GetAll().Returns(produtos);
        mapper.Map<IEnumerable<ProdutoDTO>>(produtos).Returns(produtoDTOs);
        var retorno = controller.Get().Result as OkObjectResult;

        Assert.That(retorno!.Value, Is.EqualTo(produtoDTOs));
    }

    [Test]
    public void Get_Nok()
    {
        IEnumerable<ProdutoDTO> produtoDTOs = new List<ProdutoDTO>(){
            new ProdutoDTO(){Nome = "Teste", ImagemUrl = "Teste"}
        };
        var produtos = new List<Produto>();

        repository.GetAll().Returns(produtos);
        mapper.Map<IEnumerable<ProdutoDTO>>(produtos).ReturnsNull();
        var retorno = controller.Get().Result;

        Assert.That(retorno, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public void Get_Id_OK()
    {
        var produto = new Produto() { Nome = "Teste", ImagemUrl = "Teste" };
        var produtoDTO = new ProdutoDTO() { Nome = "Teste", ImagemUrl = "Teste" };

        repository.Get(Arg.Any<Expression<Func<Produto, bool>>>()).Returns(produto);
        mapper.Map<ProdutoDTO>(produto).Returns(produtoDTO);
        var retorno = controller.Get(1).Result as OkObjectResult;

        Assert.That(retorno!.Value, Is.EqualTo(produtoDTO));
    }

    [Test]
    public void Get_Id_ProdutoInexistente()
    {
        repository.Get(Arg.Any<Expression<Func<Produto, bool>>>()).ReturnsNull();

        var retorno = controller.Get(1).Result;
        Assert.That(retorno, Is.InstanceOf<NotFoundObjectResult>());
    }

    [Test]
    public void Post_OK()
    {
        var produtoDTO = new ProdutoDTO() { Nome = "Teste", ImagemUrl = "Teste" };
        var produto = new Produto() { Nome = "Teste", ImagemUrl = "Teste" };

        mapper.Map<Produto>(produtoDTO).Returns(produto);
        repository.Create(produto);

        var retorno = controller.Post(produtoDTO).Result as CreatedAtRouteResult;

        Assert.That(retorno!.Value, Is.EqualTo(produtoDTO));
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
        var produtoDTO = new ProdutoDTO() { Id = 1, Nome = "Teste", ImagemUrl = "Teste" };
        var produto = new Produto() { Nome = "Teste", ImagemUrl = "Teste" };

        mapper.Map<Produto>(produtoDTO).Returns(produto);
        repository.Update(produto);

        var retorno = controller.Put(1, produtoDTO).Result as OkObjectResult;

        Assert.That(retorno!.Value, Is.EqualTo(produtoDTO));
        Assert.That(uow.commitInvocado, Is.EqualTo(1));
    }

    [Test]
    public void Put_IdDiferente()
    {
        var produtoDTO = new ProdutoDTO() { Id = 1, Nome = "Teste", ImagemUrl = "Teste" };
        var retorno = controller.Put(2, produtoDTO).Result;

        Assert.That(retorno, Is.InstanceOf<BadRequestResult>());
        Assert.That(uow.commitInvocado, Is.Zero);
    }

    [Test]
    public void Delete_OK()
    {
        var produtoDTO = new ProdutoDTO() { Id = 1, Nome = "Teste", ImagemUrl = "Teste" };
        var produto = new Produto() { Nome = "Teste", ImagemUrl = "Teste" };

        repository.Get(Arg.Any<Expression<Func<Produto, bool>>>()).Returns(produto);
        mapper.Map<ProdutoDTO>(produto).Returns(produtoDTO);
        repository.Delete(produto);

        var retorno = controller.Delete(1).Result as OkObjectResult;

        Assert.That(retorno!.Value, Is.EqualTo(produtoDTO));
        Assert.That(uow.commitInvocado, Is.EqualTo(1));
    }

    [Test]
    public void Delete_IdInexistente()
    {
        repository.Get(Arg.Any<Expression<Func<Produto, bool>>>()).ReturnsNull();

        var retorno = controller.Delete(1).Result;

        Assert.That(retorno, Is.InstanceOf<NotFoundObjectResult>());
        Assert.That(uow.commitInvocado, Is.Zero);
    }

}