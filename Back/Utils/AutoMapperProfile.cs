using System.Globalization;
using AutoMapper;
using Back.DTOs;
using Back.Models;

namespace Back.Utils;

public class AutoMapperProfile : Profile
{

  public AutoMapperProfile()
  {
    CreateMap<Articulo, ArticuloDTO>();
    CreateMap<ArticuloDTO, Articulo>()
      .ForMember(dest => dest.Id, opt => opt.Ignore())
      .ForMember(dest => dest.ArticuloTienda, opt => opt.Ignore())
      .ForMember(dest => dest.ClienteArticulos, opt => opt.Ignore());

    CreateMap<Cliente, ClienteDTO>();
    CreateMap<ClienteDTO, Cliente>()
      .ForMember(dest => dest.Id, opt => opt.Ignore())
      .ForMember(dest => dest.Compras, opt => opt.Ignore());

    CreateMap<Tiendum, TiendumDTO>();
    CreateMap<TiendumDTO, Tiendum>()
      .ForMember(dest => dest.Id, opt => opt.Ignore())
      .ForMember(dest => dest.ArticuloTienda, opt => opt.Ignore());

    CreateMap<Compra, CompraDTO>()
      .ForMember(dest => dest.Cliente, opt => opt.MapFrom(src => $"{src.IdClienteNavigation.Nombre} {src.IdClienteNavigation.Apellidos}"))
      .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => src.Fecha.ToString("dd/MM/yyyy HH:mm:ss")));
    CreateMap<CompraDTO, Compra>()
      .ForMember(dest => dest.Id, opt => opt.Ignore())
      .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => DateTime.ParseExact(src.Fecha, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)))
      .ForMember(dest => dest.ClienteArticulos, opt => opt.Ignore())
      .ForMember(dest => dest.IdClienteNavigation, opt => opt.Ignore());

    CreateMap<ArticuloTiendum, ArticuloTiendumDTO>()
      .ForMember(dest => dest.Articulo, opt => opt.MapFrom(src => $"{src.IdArticuloNavigation.Codigo} | {src.IdArticuloNavigation.Descripcion}"))
      .ForMember(dest => dest.Tienda, opt => opt.MapFrom(src => src.IdTiendaNavigation.Sucursal))
      .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => src.Fecha.ToString("dd/MM/yyyy HH:mm:ss")));
    CreateMap<ArticuloTiendumDTO, ArticuloTiendum>()
      .ForMember(dest => dest.IdArticuloNavigation, opt => opt.Ignore())
      .ForMember(dest => dest.IdTiendaNavigation, opt => opt.Ignore())
      .ForMember(dest => dest.Fecha, opt => opt.MapFrom(dto => DateTime.ParseExact(dto.Fecha, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)));

    CreateMap<ClienteArticulo, ClienteArticuloDTO>()
      .ForMember(dest => dest.Articulo, opt => opt.MapFrom(src => $"{src.IdArticuloNavigation.Codigo} | {src.IdArticuloNavigation.Descripcion}"));
    CreateMap<ClienteArticuloDTO, ClienteArticulo>()
      .ForMember(dest => dest.IdCompraNavigation, opt => opt.Ignore())
      .ForMember(dest => dest.IdArticuloNavigation, opt => opt.Ignore());
  }

}