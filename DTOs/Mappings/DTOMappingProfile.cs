using APICatalogo.Models;
using AutoMapper;

namespace APICatalogo.DTOs.Mappings;

public class DTOMappingProfile : Profile
{
    public DTOMappingProfile()
    {
        CreateMap<ProdutoDTO, Produto>().ReverseMap();
        CreateMap<CategoriaDTO, Categoria>().ReverseMap();
    }
}