using AutoMapper;
using Domain.Entities;
using Newtonsoft.Json.Linq;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<JObject, Show>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (int)src["id"]))
           .ForMember(dest => dest.Url, opt => opt.MapFrom(src => (string)src["url"]))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => (string)src["name"]))
           .ForMember(dest => dest.Type, opt => opt.MapFrom(src => (string)src["type"]))
           .ForMember(dest => dest.Runtime, opt => opt.MapFrom(src => (int)src["runtime"]))
           .ForMember(dest => dest.Language, opt => opt.MapFrom(src => (string)src["language"]))
           .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src["genres"].ToObject<List<string>>()))
           .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (string)src["status"]))           
           .ForMember(dest => dest.AverageRuntime, opt => opt.MapFrom(src => (int)src["averageRuntime"]))
           .ForMember(dest => dest.Premiered, opt => opt.MapFrom(src => (DateTime?)src["premiered"]))
           .ForMember(dest => dest.Ended, opt => opt.MapFrom(src => (DateTime?)src["ended"]))
           .ForMember(dest => dest.OfficialSite, opt => opt.MapFrom(src => (string)src["officialSite"]))
           .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => (int)src["weight"]))
           .ForMember(dest => dest.Network, opt => opt.MapFrom(src => src["network"].ToObject<Network>()))
           .ForMember(dest => dest.WebChannel, opt => opt.MapFrom(src => src["webChannel"].ToObject<WebChannel>()))
           .ForMember(dest => dest.DvdCountry, opt => opt.MapFrom(src => new Country
           {
               Name = (string)src["dvdCountry"]["name"],
               Code = (string)src["dvdCountry"]["code"],
               Timezone = (string)src["dvdCountry"]["timezone"]
           }))
           .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => (string)src["summary"]))
           .ForMember(dest => dest.Updated, opt => opt.MapFrom(src => (long)src["updated"]))           
           .ForMember(dest => dest.Links, opt => opt.MapFrom(src => new Link
           {
               Self = new Self
               {
                   href = (string)src["_links"]["self"]["href"]
               },
               Previousepisode = new Previousepisode
               {
                   Href = (string)src["_links"]["previousepisode"]["href"],
                   Name = (string)src["_links"]["previousepisode"]["name"]
               }
           }));


            CreateMap<JObject, WebChannel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (int)src["webChannel"]["id"]))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => (string)src["webChannel"]["name"]))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src["webChannel"]["country"].ToObject<Country>()))
            .ForMember(dest => dest.OfficialSite, opt => opt.MapFrom(src => (int)src["webChannel"]["officialSite"]));

            CreateMap<JObject, Schedule>()
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => (string)src["schedule"]["time"]))
                .ForMember(dest => dest.Days, opt => opt.MapFrom(src => src["schedule"]["days"].ToObject<List<string>>()));

            CreateMap<JObject, Rating>()
                .ForMember(dest => dest.Average, opt => opt.MapFrom(src => (double)src["rating"]["average"]));

            CreateMap<JObject, Country>()
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => (string)src["country"]["name"]))
             .ForMember(dest => dest.Code, opt => opt.MapFrom(src => (string)src["country"]["code"]))
             .ForMember(dest => dest.Timezone, opt => opt.MapFrom(src => (string)src["country"]["timezone"]));

            CreateMap<JObject, Network>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (int)src["network"]["id"]))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => (string)src["network"]["name"]))
                .ForMember(dest => dest.OfficialSite, opt => opt.MapFrom(src => (string)src["network"]["officialSite"]))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src["network"]["country"].ToObject<Country>()));


            CreateMap<JObject, Externals>()
                .ForMember(dest => dest.Tvrage, opt => opt.MapFrom(src => (int)src["externals"]["tvrage"]))
                .ForMember(dest => dest.Thetvdb, opt => opt.MapFrom(src => (int)src["externals"]["thetvdb"]))
                .ForMember(dest => dest.Imdb, opt => opt.MapFrom(src => (string)src["externals"]["imdb"]));

            CreateMap<JObject, Image>()
                .ForMember(dest => dest.Medium, opt => opt.MapFrom(src => (string)src["image"]["medium"]))
                .ForMember(dest => dest.Original, opt => opt.MapFrom(src => (string)src["image"]["original"]));


        }
    }
}
