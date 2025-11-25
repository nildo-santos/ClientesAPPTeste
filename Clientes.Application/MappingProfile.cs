using AutoMapper;
using Clientes.Application.DTOs;
using Clientes.Domain.Entidade;

namespace Clientes.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EnderecoRequestDTO, Endereco>();

            CreateMap<Endereco, EnderecoResponseDTO>();


            CreateMap<ClienteRequestDTO, Cliente>()
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.Endereco));

            
            CreateMap<Cliente, ClienteResponseDTO>()
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.Endereco));
        }
    }
}