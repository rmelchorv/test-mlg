using System.Globalization;
using AutoMapper;
using Back.DTOs;
using Back.Models;

namespace Back.Utils;

public class AutoMapperProfile : Profile {

  public AutoMapperProfile() {
    CreateMap<Articulo, ArticuloDTO>();
    CreateMap<ArticuloDTO, Articulo>()
      .ForMember(a => a.ArticuloTienda, opt => opt.Ignore())
      .ForMember(a => a.ClienteArticulos, opt => opt.Ignore());
    
    CreateMap<Cliente, ClienteDTO>().ReverseMap();
    CreateMap<Tiendum, TiendumDTO>().ReverseMap();
    
    CreateMap<Compra, CompraDTO>()
      .ForMember(dto => dto.Cliente, opt => opt.MapFrom(c => c.IdClienteNavigation.Nombre))
      .ForMember(dto => dto.Fecha, opt => opt.MapFrom(c => c.Fecha.ToString("dd/MM/yyyy HH:mm:ss")));
    CreateMap<CompraDTO, Compra>()
      .ForMember(c => c.IdClienteNavigation, opt => opt.Ignore())
      .ForMember(c => c.Fecha, opt => opt.MapFrom(dto => DateTime.ParseExact(dto.Fecha, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)));

    CreateMap<ArticuloTiendum, ArticuloTiendumDTO>()
      .ForMember(dto => dto.Articulo, opt => opt.MapFrom(at => at.IdArticuloNavigation.Descripcion))
      .ForMember(dto => dto.Tiendum, opt => opt.MapFrom(at => at.IdTiendaNavigation.Sucursal))
      .ForMember(dto => dto.Fecha, opt => opt.MapFrom(at => at.Fecha.ToString("dd/MM/yyyy HH:mm:ss")));
    CreateMap<ArticuloTiendumDTO, ArticuloTiendum>()
      .ForMember(at => at.IdArticuloNavigation, opt => opt.Ignore())
      .ForMember(at => at.IdTiendaNavigation, opt => opt.Ignore())
      .ForMember(at => at.Fecha, opt => opt.MapFrom(dto => DateTime.ParseExact(dto.Fecha, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)));

    CreateMap<ClienteArticulo, ClienteArticuloDTO>()
      .ForMember(dto => dto.Compra, opt => opt.MapFrom(ca => ca.IdCompraNavigation.Id.ToString()))
      .ForMember(dto => dto.Articulo, opt => opt.MapFrom(ca => ca.IdArticuloNavigation.Descripcion));
    CreateMap<ClienteArticuloDTO, ClienteArticulo>()
      .ForMember(ca => ca.IdCompraNavigation, opt => opt.Ignore())
      .ForMember(ca => ca.IdArticuloNavigation, opt => opt.Ignore());
  }

}