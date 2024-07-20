using AutoMapper;
using MedicalCenter.Mapping;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCenter.Tests.Mocks
{
    public static class MockMapper
    {

        public static IMapper Instance
        {
            get 
            {
                var profile = new MedicalCenterProfile();

                var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));

                var mapper = new Mapper(configuration);

                return mapper;
            }
        }
    }
}
