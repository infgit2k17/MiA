﻿using AutoMapper;
using MiA_projekt.Dto;
using MiA_projekt.Models;
using MiA_projekt.Models.AccountViewModels;
using System;

namespace MiA_projekt
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, AppUserDto>();

            CreateMap<AppUserDto, AppUser>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Address, AddressDto>();

            CreateMap<AddressDto, Address>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Apartment, ApartmentDto>()
                .ForMember(dest => dest.From, opt => opt.MapFrom(src => src.From.ToString("d")))
                .ForMember(dest => dest.To, opt => opt.MapFrom(src => src.To.ToString("d")));

            CreateMap<ApartmentDto, Apartment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.From, opt => opt.MapFrom(src => DateTime.Parse(src.From)))
                .ForMember(dest => dest.To, opt => opt.MapFrom(src => DateTime.Parse(src.To)));

            CreateMap<Apartment, MyOfferVM>()
                .ForMember(dest => dest.From, opt => opt.MapFrom(src => src.From.ToString("d")))
                .ForMember(dest => dest.To, opt => opt.MapFrom(src => src.To.ToString("d")))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode));

            CreateMap<Comment, CommentDto>();

            CreateMap<CommentDto, Comment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}