using AutoMapper;

namespace Contacts.BusinessObjects
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Person, Contacts.Person>().ReverseMap();
            CreateMap<PersonPhone, Contacts.PersonPhone>().ReverseMap();
        }
    }
}
