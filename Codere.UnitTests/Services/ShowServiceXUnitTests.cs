using Application.Services;
using AutoMapper;
using Codere.UnitTests.Mock;
using Domain.Entities;
using Domain.Interface;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Codere.UnitTests.Services
{
    public class ShowServiceXUnitTests
    {
        private readonly Mock<IRepository<Show>> _mockShowRepository;
        private readonly Mock<IRepository<Network>> _mockNetworkRepository;
        private readonly Mock<IRepository<Country>> _mockCountryRepository;
        private readonly Mock<IRepository<WebChannel>> _mockWebChannelRepository;
        private readonly Mock<IRepository<Externals>> _mockExternalsRepository;
        private readonly Mock<IRepository<Link>> _mockLinkRepository;
        private readonly Mock<IRepository<Self>> _mockSelfRepository;
        private readonly Mock<IRepository<Previousepisode>> _mockPreviousRepository;
        private readonly Mock<IRepository<Image>> _mockImageRepository;
        private readonly Mock<IRepository<Schedule>> _mockScheduleRepository;
        private readonly Mock<IRepository<Rating>> _mockRatingRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ShowService _showService;

        public ShowServiceXUnitTests()
        {
            _mockShowRepository = MockShowRepository.Get();
            _mockNetworkRepository = new Mock<IRepository<Network>>();
            _mockCountryRepository = new Mock<IRepository<Country>>();
            _mockWebChannelRepository = new Mock<IRepository<WebChannel>>();

            _mockExternalsRepository = new Mock<IRepository<Externals>>();
            _mockLinkRepository = new Mock<IRepository<Link>>();
            _mockSelfRepository = new Mock<IRepository<Self>>();
            _mockPreviousRepository = new Mock<IRepository<Previousepisode>>();
            _mockImageRepository = new Mock<IRepository<Image>>();
            _mockScheduleRepository = new Mock<IRepository<Schedule>>();
            _mockRatingRepository = new Mock<IRepository<Rating>>();

            _mockMapper = new Mock<IMapper>();

            _showService = new ShowService(
                _mockShowRepository.Object,
                _mockNetworkRepository.Object,
                _mockCountryRepository.Object,
                _mockWebChannelRepository.Object,
                _mockExternalsRepository.Object,
                _mockLinkRepository.Object,
                _mockSelfRepository.Object,
                _mockPreviousRepository.Object,
                _mockImageRepository.Object,
                _mockScheduleRepository.Object,
                _mockRatingRepository.Object,
                _mockMapper.Object
            );
        }

        [Fact]
        public async Task ProcessShowsAsyncTest()
        {

            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var jsonFilePath = Path.Combine(basePath, "TestData", "shows1.json");
            var jsonContent = File.ReadAllText(jsonFilePath);
            var showDtos = JsonConvert.DeserializeObject<List<Show>>(jsonContent);

            _mockMapper.Setup(m => m.Map<Show>(It.IsAny<Show>())).Returns((Show s) => s);

            _mockShowRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Show)null);
            _mockShowRepository.Setup(r => r.AddAsync(It.IsAny<Show>())).Returns(Task.CompletedTask);
            _mockShowRepository.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            _mockNetworkRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Network)null);
            _mockNetworkRepository.Setup(r => r.AddAsync(It.IsAny<Network>())).Returns(Task.CompletedTask);

            _mockWebChannelRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((WebChannel)null);
            _mockWebChannelRepository.Setup(r => r.AddAsync(It.IsAny<WebChannel>())).Returns(Task.CompletedTask);

            _mockCountryRepository.Setup(r => r.FindAsync(It.IsAny<Expression<Func<Country, bool>>>())).ReturnsAsync((Country)null);
            _mockCountryRepository.Setup(r => r.AddAsync(It.IsAny<Country>())).Returns(Task.CompletedTask);

            _mockExternalsRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Externals)null);
            _mockExternalsRepository.Setup(r => r.AddAsync(It.IsAny<Externals>())).Returns(Task.CompletedTask);

            _mockLinkRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Link)null);
            _mockLinkRepository.Setup(r => r.AddAsync(It.IsAny<Link>())).Returns(Task.CompletedTask);

            _mockSelfRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Self)null);
            _mockSelfRepository.Setup(r => r.AddAsync(It.IsAny<Self>())).Returns(Task.CompletedTask);

            _mockPreviousRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Previousepisode)null);
            _mockPreviousRepository.Setup(r => r.AddAsync(It.IsAny<Previousepisode>())).Returns(Task.CompletedTask);

            _mockImageRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Image)null);
            _mockImageRepository.Setup(r => r.AddAsync(It.IsAny<Image>())).Returns(Task.CompletedTask);

            _mockScheduleRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Schedule)null);
            _mockScheduleRepository.Setup(r => r.AddAsync(It.IsAny<Schedule>())).Returns(Task.CompletedTask);

            _mockRatingRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Rating)null);
            _mockRatingRepository.Setup(r => r.AddAsync(It.IsAny<Rating>())).Returns(Task.CompletedTask);


            await _showService.ProcessShowsAsync(showDtos);

            // Assert
            _mockShowRepository.Verify(r => r.AddAsync(It.IsAny<Show>()), Times.Exactly(showDtos.Count));

        }

      
        [Fact]
        public async Task GetAllShowAsyncTest()
        {
            var jsonContent = File.ReadAllText("TestData/shows1.json");
            var expectedShows = JsonConvert.DeserializeObject<List<Show>>(jsonContent);

            _mockShowRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(expectedShows);

       
            var actualShows = await _showService.GetAllAsync();

            // Assert
            Assert.NotNull(actualShows);
            Assert.Equal(expectedShows.Count, actualShows.Count);

            for (int i = 0; i < expectedShows.Count; i++)
            {
                CompareJObjects(JObject.FromObject(expectedShows[i]), JObject.FromObject(actualShows[i]));
            }
        }

        [Fact]
        public async Task GetShowByIdAsyncTest()
        {
            var jsonContent = File.ReadAllText("TestData/show1.json");
            var showDto = JsonConvert.DeserializeObject<Show>(jsonContent);

            _mockShowRepository.Setup(repo => repo.GetByIdAsync(showDto.Id)).ReturnsAsync(showDto);


            var result = await _showService.GetByIdAsync(showDto.Id);

            Assert.NotNull(result);
            Assert.Equal(showDto.Id, result["id"].Value<int>());
            Assert.Equal(showDto.Name, result["name"].Value<string>());
            Assert.Equal(showDto.Language, result["language"].Value<string>());
            if (showDto.DvdCountry != null)
            {
                Assert.Equal(showDto.DvdCountry.Name, result["network"]["Country"]["name"].Value<string>());
                Assert.Equal(showDto.DvdCountry.Code, result["network"]["Country"]["code"].Value<string>());
            }

            // Serializar el resultado a JSON
            var initialJson = JObject.Parse(jsonContent);
            var serializedResult = JObject.FromObject(result);

            // Comparar propiedades del Json
            CompareJObjects(initialJson, serializedResult);
        }

        private void CompareJObjects(JObject expected, JObject finalJson)
        {
            try
            {
                foreach (var property in finalJson.Properties())
                {
                    var expectedValue = property.Value;
                    var actualProperty = finalJson.Property(property.Name);

                    if (actualProperty == null)
                    {
                        Assert.True(false, $"La propiedad '{property.Name}' esperada no existe en actualJson.");
                    }
                    else
                    {
                        var actualValue = actualProperty.Value;

                        // Comparación de valores según el tipo de valor esperado
                        if (expectedValue.Type == JTokenType.Array)
                        {
                            // Comparación para arreglos
                            Assert.Equal(expectedValue.ToString(Formatting.None), actualValue.ToString(Formatting.None));
                        }
                        else if (expectedValue.Type == JTokenType.Object)
                        {
                            // Comparación para objetos
                            CompareJObjects((JObject)expectedValue, (JObject)actualValue);
                        }
                        else
                        {
                            // Comparación para otros tipos de valor
                            Assert.Equal(expectedValue.ToString(), actualValue.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.True(false, $"Error al comparar objetos JSON: {ex.Message}");
            }
        }


    }
}
