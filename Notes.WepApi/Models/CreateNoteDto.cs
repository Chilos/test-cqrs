using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Application.Notes.Commands.CreateNote;

namespace Notes.WepApi.Models
{
    public class CreateNoteDto : IMapWith<CreateNoteCommand>
    {
        [Required]
        public string Title { get; set; }
        public string Details { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateNoteDto, CreateNoteCommand>()
                .ForMember(command => command.Title,
                    opt => opt.MapFrom(dto => dto.Title))
                .ForMember(command => command.Detail,
                    opt => opt.MapFrom(dto => dto.Details));
        }
    }
}