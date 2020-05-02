using AutoMapper;
using X.Api.Entities;

namespace X.Api.Source.Domain.Requests.Command
{
    public class EditStateMapper : Profile
    {
        public EditStateMapper()
        {
            CreateMap<EditStateDto, State>();
        }
    }
}
