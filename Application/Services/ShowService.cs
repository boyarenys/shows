using Application.Interface;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Application.Services
{
    public class ShowService : IShowService
    {
        private readonly IRepository<Network> _networkRepository;
        private readonly IRepository<Show> _showRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<WebChannel> _webChannelRepository;
        private readonly IRepository<Externals> _externalsRepository;
        private readonly IRepository<Link> _linkRepository;
        private readonly IRepository<Self> _selfRepository;
        private readonly IRepository<Previousepisode> _previousRepository;
        private readonly IRepository<Image> _imageRepository;
        private readonly IRepository<Schedule> _scheduleRepository;
        private readonly IRepository<Rating> _ratingRepository;
        private readonly IMapper _mapper;
        public ShowService(IRepository<Show> showRepository, IRepository<Network> networkRepository, IRepository<Country> countryRepository,
                       IRepository<WebChannel> webChannelRepository, IRepository<Externals> externalsRepository, IRepository<Link> linkRepository,
                       IRepository<Self> selfRepository, IRepository<Previousepisode> previousRepository, IRepository<Image> imageRepository,
                       IRepository<Schedule> scheduleRepository, IRepository<Rating> ratingRepository, IMapper mapper)
        {
            _networkRepository = networkRepository;
            _showRepository = showRepository;
            _countryRepository = countryRepository;
            _webChannelRepository = webChannelRepository;
            _externalsRepository = externalsRepository;
            _linkRepository = linkRepository;
            _selfRepository = selfRepository;
            _previousRepository = previousRepository;
            _imageRepository = imageRepository;
            _scheduleRepository = scheduleRepository;
            _ratingRepository = ratingRepository;
            _mapper = mapper;
        }

        public async Task ProcessShowsAsync(IEnumerable<Show> showsDto)
        {
            var createdCountries = new Dictionary<string, Country>();
            foreach (var jsonShow in showsDto)
            {
                try
                {
                    var showEntity = await ProcessShowAsync(jsonShow, createdCountries);
                    if (showEntity != null)
                    {
                        await _showRepository.AddAsync(showEntity);
                        await _showRepository.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al procesar el Show con Id: {jsonShow}");
                    Console.WriteLine($"Mensaje de excepción: {ex.Message}");
                }
            }
            //En este caso prefiero guadar cada show 1x1 para tener mas control de cada show a insertar
            //Pero si hubiera muchos mas datos esto no seria eficiente y se deberia usar SaveChangesAsync()
            //fuera del bucle
            //
            //try
            //{
            //    await _showRepository.SaveChangesAsync();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Error al guardar los cambios en el repositorio: {ex.Message}");
            //}           
        }


        private async Task<Show> ProcessShowAsync(Show jsonShow, Dictionary<string, Country> createdCountries)
        {
            var showEntity = _mapper.Map<Show>(jsonShow);

            var existingShow = await _showRepository.GetByIdAsync(showEntity.Id);
            if (existingShow == null)
            {

                // Mapping de Link y PreviousEpisode
                var linkEntity = new Link();
                if (jsonShow.Links.Self != null)
                {
                    linkEntity.Self = jsonShow.Links.Self;
                }

                if (jsonShow.Links.Previousepisode != null)
                {
                    linkEntity.Previousepisode = jsonShow.Links.Previousepisode;
                }
                showEntity.Links = linkEntity;


                // Verificar y crear la Network si es necesaria
                if (showEntity.Network != null)
                {
                    //Comprobar si existe Network
                    var existingNetwork = await _networkRepository.GetByIdAsync(showEntity.Network.Id);
                    if (existingNetwork == null)
                    {
                        if (showEntity.Network.Country != null)
                        {
                            showEntity.Network.Country = await GetOrCreateCountryAsync(showEntity.Network.Country, createdCountries);
                        }
                        await _networkRepository.AddAsync(showEntity.Network);
                    }
                    else
                    {
                        showEntity.Network = existingNetwork;
                    }
                }

                // WebChannel
                var entityWebChannel = showEntity.WebChannel;
                if (entityWebChannel != null)
                {
                    var existingWebChannel = await _webChannelRepository.GetByIdAsync(entityWebChannel.Id);
                    if (existingWebChannel != null)
                    {
                        showEntity.WebChannel = existingWebChannel;  
                        if (showEntity.WebChannel.Country != null)
                        {
                            showEntity.WebChannel.Country = await GetOrCreateCountryAsync(showEntity.WebChannel.Country, createdCountries);
                        }
                    }
                    else
                    {
                        // Si el WebChannel no existe, crearlo si el Country está definido
                        if (showEntity.WebChannel.Country != null)
                        {
                            showEntity.WebChannel.Country = await GetOrCreateCountryAsync(showEntity.WebChannel.Country, createdCountries);
                        }
                        await _webChannelRepository.AddAsync(entityWebChannel);
                    }
                }
                return showEntity;
            }
            return null;
        }

        private async Task<Country> GetOrCreateCountryAsync(Country countryDto, Dictionary<string, Country> createdCountries)
        {
            var countryCode = countryDto.Code;

            if (!createdCountries.ContainsKey(countryCode))
            {
                var existingCountry = await _countryRepository.FindAsync(c => c.Code == countryCode);

                if (existingCountry == null)
                {
                    var newCountry = new Country
                    {
                        Name = countryDto.Name,
                        Code = countryDto.Code,
                        Timezone = countryDto.Timezone
                    };

                    await _countryRepository.AddAsync(newCountry);
                    createdCountries.Add(countryCode, newCountry);

                    return newCountry;
                }
                else
                {
                    createdCountries.Add(countryCode, existingCountry);
                    return existingCountry;
                }
            }
            else
            {
                return createdCountries[countryCode];
            }
        }

        public async Task<JArray> GetAllAsync()
        {
            try
            {
                var shows = await _showRepository.GetAllAsync(); 

                // Para cada show, cargar las entidades relacionadas
                foreach (var show in shows)
                {
                    await PopulateRelatedEntitiesAsync(show);
                }

                var jsonSettings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                // Convertir la lista de shows a un arreglo JSON
                var showsJsonArray = new JArray();
                foreach (var show in shows)
                {
                    var showJson = JObject.FromObject(show, JsonSerializer.CreateDefault(jsonSettings));
                    var cleanedJson = CleanJson(JObject.FromObject(showJson)); 

                    showsJsonArray.Add(cleanedJson);
                }

                return showsJsonArray;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error GetAllAsync(): {ex.Message}");
                return null;
            }
        }
        
        public async Task<JObject> GetByIdAsync(int id)
        {
            try
            {
                var show = await _showRepository.GetByIdAsync(id);
                await PopulateRelatedEntitiesAsync(show);

                var jsonSettings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                var showJson = JObject.FromObject(show, JsonSerializer.CreateDefault(jsonSettings));
               

                return CleanJson(JObject.FromObject(showJson));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error GetByIdAsync(): {ex.Message}");
                return null;
            }
        }

        #region Methods return Show by DB

        private async Task LoadRelatedEntityAsync<TEntity>(int? entityId, Func<int, Task<TEntity>> repositoryMethod, Action<TEntity> setEntity)
          where TEntity : class
        {
            if (entityId.HasValue && entityId.Value > 0)
            {
                var entity = await repositoryMethod(entityId.Value);
                setEntity(entity);

                // Link
                if (typeof(TEntity) == typeof(Link))
                {
                    var link = entity as Link;
                    if (link != null)
                    {
                        // Load Self
                        if (link.SelfId > 0)
                        {
                            var self = await _selfRepository.GetByIdAsync(link.SelfId);
                            link.Self = self;
                        }

                        // Load Previousepisode
                        if (link.PreviousepisodeId > 0)
                        {
                            var previousepisode = await _previousRepository.GetByIdAsync(link.PreviousepisodeId);
                            link.Previousepisode = previousepisode;
                        }
                    }
                }

                // network
                if (typeof(TEntity) == typeof(Network))
                {
                    var network = entity as Network;
                    if (network != null && network.CountryId != null)
                    {
                        network.Country = await _countryRepository.GetByIdAsync(network.CountryId.Value);
                    }
                }
            }
        }
        private void RemoveNestedProperties(JObject jsonObject, string propertyName, string[] nestedProperties)
        {
            var property = jsonObject[propertyName];
            if (property != null && property.Type == JTokenType.Object)
            {
                var nestedObject = (JObject)property;
                foreach (var nestedProperty in nestedProperties)
                {
                    nestedObject.Remove(nestedProperty);
                }
            }
        }

        private async Task PopulateRelatedEntitiesAsync(Show show)
        {
            await LoadRelatedEntityAsync(show.NetworkId, _networkRepository.GetByIdAsync, network => show.Network = network);
            await LoadRelatedEntityAsync(show.ExternalsId, _externalsRepository.GetByIdAsync, externals => show.Externals = externals);
            await LoadRelatedEntityAsync(show.RatingId, _ratingRepository.GetByIdAsync, rating => show.Rating = rating);
            await LoadRelatedEntityAsync(show.ImageId, _imageRepository.GetByIdAsync, image => show.Image = image);
            await LoadRelatedEntityAsync(show.ScheduleId, _scheduleRepository.GetByIdAsync, schedule => show.Schedule = schedule);
            await LoadRelatedEntityAsync(show.WebChannelId, _webChannelRepository.GetByIdAsync, webChannel => show.WebChannel = webChannel);
            await LoadRelatedEntityAsync(show.LinkId, _linkRepository.GetByIdAsync, link => show.Links = link);
            await LoadRelatedEntityAsync(show.CountryId, _countryRepository.GetByIdAsync, dvdcountry => show.DvdCountry = dvdcountry);
        }

      

        private JObject CleanJson(JObject showJson)
        {
            var propertiesToRemove = new[] { "NetworkId", "LinkId", "WebChannelId", "CountryId", "RatingId", "ImageId", "ScheduleId", "ExternalsId" };

            foreach (var property in propertiesToRemove)
            {
                showJson.Remove(property);
            }

            RemoveNestedProperties(showJson, "rating", new[] { "Id" });
            RemoveNestedProperties(showJson, "image", new[] { "Id", "Shows" });
            RemoveNestedProperties(showJson, "webchannel", new[] { "Id", "CountryId" });
            RemoveNestedProperties(showJson, "externals", new[] { "Id" });
            RemoveNestedProperties(showJson, "schedule", new[] { "Id" });
            RemoveNestedProperties(showJson, "dvdCountry", new[] { "Id", "Networks", "Shows" });

            // Link
            var linkProperty = showJson["_links"];
            if (linkProperty != null && linkProperty.Type == JTokenType.Object)
            {
                var linkObject = (JObject)linkProperty;
                linkObject.Remove("Id");
                linkObject.Remove("SelfId");
                linkObject.Remove("PreviousepisodeId");

                var selfProperty = linkObject["Self"];
                if (selfProperty != null && selfProperty.Type == JTokenType.Object)
                {
                    var selfObject = (JObject)selfProperty;
                    selfObject.Remove("Id");
                    selfObject.Remove("Links");
                }
                var previousProperty = linkObject["Previousepisode"];
                if (previousProperty != null && previousProperty.Type == JTokenType.Object)
                {
                    var previousObject = (JObject)previousProperty;
                    previousObject.Remove("Id");
                    previousObject.Remove("Links");
                }
            }

            // Network
            var networkProperty = showJson["network"];
            if (networkProperty != null && networkProperty.Type == JTokenType.Object)
            {
                var networkObject = (JObject)networkProperty;
                networkObject.Remove("CountryId");

                var countryProperty = networkObject["Country"];
                if (countryProperty != null && countryProperty.Type == JTokenType.Object)
                {
                    var countryObject = (JObject)countryProperty;
                    countryObject.Remove("Id");
                    countryObject.Remove("Networks");
                    countryObject.Remove("Shows");
                }
            }

            return showJson;
        }

        #endregion
        //public async Task<Network> GetNetworkByIdAsync(int id)
        //{
        //    try
        //    {
        //        //  var showsJson = shows.Select(show => _mapper.Map<JObject>(show));
        //        return await _networkRepository.GetByIdAsync(id);

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error fetching shows from database: {ex.Message}");
        //        return null;
        //    }
        //}

    }
}
