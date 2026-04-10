using AutoMapper;
using FinanceTrackerApp.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using FinanceTrackerApp.Models;

namespace FinanceTrackerApp.Mappings
{
    public class MappingProfile : Profile
    { 
        public MappingProfile() {
            //READ
            CreateMap<Transaction, TransactionDto>();
            //CREATE
            CreateMap<TransactionCreateDto, Transaction>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());
            //EDIT
            CreateMap<TransactionEditDto, Transaction>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());
            //Pre populating edit form
            CreateMap<Transaction, TransactionEditDto>();
        }
        

        

    }
}
