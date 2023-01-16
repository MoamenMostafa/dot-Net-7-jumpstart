using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dot_Net_7_jumpstart.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character,GetCharacterDto>().ReverseMap();
            // CreateMap<List<Character>,List<GetCharacterDto>>().ReverseMap();
            CreateMap<Character,AddCharacterDto>().ReverseMap();
            CreateMap<Character,UpdateCharacterDto>().ReverseMap();

        }
    }
}