using AutoMapper;
using PMA_Core.DTOs;
using PMA_Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMA_Core.MappingProfiles
{
    public class DtoMappingProfiles : Profile
    {
        public DtoMappingProfiles()
        {
            CreateMap<UserRoleDTO, PMA_UserRole>().ReverseMap();

            //CreateMap<PMA_UserRole, UserRoleDTO>(); // Map from UserRole to UserRoleDTO
            //CreateMap<UserRoleDTO, PMA_UserRole>();
        }
    }
}
