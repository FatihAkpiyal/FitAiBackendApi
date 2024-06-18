using AutoMapper;
using FitAIAPI.Application.DTOs;
using FitAIAPI.Application.Interfaces.ChatGPT;
using FitAIAPI.Domain.Entities;
using FitAIAPI.Domain.Repositories;
using FitAIAPI.Domain.Repositories.WorkoutPlan;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json;

namespace FitAIAPI.Application.Services.ChatGPT
{
    public class ChatGPTService : IChatGPTService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IWorkoutPlanRepository _workoutPlanRepository;

        public ChatGPTService(HttpClient httpClient, IMapper mapper, IOptionsSnapshot<ChatGPTSettings> settings, IConfiguration configuration, IUserRepository userRepository, IWorkoutPlanRepository workoutPlanRepository)
        {
            _httpClient = httpClient;
            _apiKey = settings.Value.ApiKey;
            _userRepository = userRepository;
            _workoutPlanRepository = workoutPlanRepository;
            _mapper = mapper;
        }

        
        public async Task<string> GenerateWorkoutPlan(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null || user.UserDetail == null)
            {
                throw new Exception("Kullanıcı veya kullanıcı detayları bulunamadı.");
            }

            var workoutDetailsDTO = _mapper.Map<UserDetailDTO>(user.UserDetail);

            string prompt = $@"
            Bir kullanıcı için haftalık antrenman programı oluştur. Sen sporla alakalı her şeyi bilen çok başarılı bir personal trainersın. 
            Kullanıcı hakkında aşağıdaki bilgilere sahipsin:
            - Cinsiyet: {workoutDetailsDTO.Gender}
            - Boy: {workoutDetailsDTO.Height} cm
            - İlk Kilo: {workoutDetailsDTO.FirstWeight} kg
            - Hedef Kilo: {workoutDetailsDTO.TargetWeight} kg
            - Doğum Tarihi: {workoutDetailsDTO.DateOfBirth}
            - Hedefler: {workoutDetailsDTO.Goals}
            - Tercih Edilen Aktiviteler: {workoutDetailsDTO.PreferredActivities}
            - Spor Yapma Sıklığı: {workoutDetailsDTO.WorkoutFrequency}
            - Odaklanılan Bölgeler: {workoutDetailsDTO.FocusAreas}
            - Sağlık Problemi: {workoutDetailsDTO.HealthProblem}

            Kullanıcının hedeflerine ve spor yapma sıklığına göre antrenman programını günlere dağıtarak oluştur. Her gün için en az 6 farklı egzersiz ve detaylı tekrar sayıları ve set bilgileri belirt. Kullanıcının sağlık problemlerini göz önünde bulundur ve ona uygun bir program oluştur.

            Lütfen programı aşağıdaki JSON formatında döndür:

            {{
                ""fitness_antrenman"": [
                    {{
                        ""day"": ""day_one"",
                        ""program"": {{
                            ""exercise_1"": ""details"",
                            ""exercise_2"": ""details"",
                            ""exercise_3"": ""details"",
                            ""exercise_4"": ""details"",
                            ""exercise_5"": ""details"",
                            ""exercise_6"": ""details""
                        }}
                    }},
                    {{
                        ""day"": ""day_two"",
                        ""program"": {{
                            ""exercise_1"": ""details"",
                            ""exercise_2"": ""details"",
                            ""exercise_3"": ""details"",
                            ""exercise_4"": ""details"",
                            ""exercise_5"": ""details"",
                            ""exercise_6"": ""details""
                        }}
                    }}
                    ...
                ]
            }}
            ";

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "system", content = "You are a helpful assistant." },
                    new { role = "user", content = prompt }
                }
            };

            using var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
            request.Headers.Add("Authorization", $"Bearer {_apiKey}");
            request.Content = JsonContent.Create(requestBody);

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var jsonDocument = JsonDocument.Parse(responseBody);
            var content = jsonDocument.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            var fitnessProgram = JsonConvert.DeserializeObject<FitnessProgramResponse>(content);
            if (fitnessProgram?.FitnessAntrenman == null)
            {
                throw new Exception("Antrenman programı oluşturulamadı.");
            }

            // Yeni workout plan kaydet
            var fullProgram = new
            {
                fitness_antrenman = fitnessProgram.FitnessAntrenman
            };

            var workoutPlan = new UserWorkoutPlan
            {
                UserId = userId,
                Program = JsonConvert.SerializeObject(fullProgram),
                CreatedOn = DateTime.UtcNow
            };

            await _workoutPlanRepository.AddAsync(workoutPlan);

            return content;

           
        }

        public List<Dictionary<string, string>> ParseWorkoutPlan(string rawPlan)
        {
            var jsonDocument = JsonDocument.Parse(rawPlan);
            var fitnessAntrenman = jsonDocument.RootElement.GetProperty("fitness_antrenman");

            // burası linq ile tekrar yazılacak

            var parsedPlan = new List<Dictionary<string, string>>();

            foreach (var day in fitnessAntrenman.EnumerateArray())
            {
                var dayPlan = new Dictionary<string, string>();
                var dayObject = day.GetProperty("program");

                foreach (var exercise in dayObject.EnumerateObject())
                {
                    dayPlan[exercise.Name] = exercise.Value.GetString();
                }

                parsedPlan.Add(dayPlan);
            }

            return parsedPlan;
        }


        public async Task<List<ExerciseAlternative>> GetExerciseAlternatives(ExerciseAlternative exerciseAlternative)
        {
            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
            new { role = "system", content = "You are a helpful assistant." },
            new { role = "user", content = $@"
            Kullanıcının şu an yaptığı egzersiz: {exerciseAlternative.Name}, şu kadar set yapıyor: {exerciseAlternative.Sets}. 
            Lütfen bu egzersiz için 6 farklı alternatif egzersiz öner. 
            Çıktıyı JSON formatında döndürmeni istiyorum. Şu formatta olacak:
            [
                {{
                    ""exercise_name"": ""example_exercise_1"",
                    ""exercise_frequency"": ""details_1""
                }},
                {{
                    ""exercise_name"": ""example_exercise_2"",
                    ""exercise_frequency"": ""details_2""
                }},
                {{
                    ""exercise_name"": ""example_exercise_3"",
                    ""exercise_frequency"": ""details_3""
                }},
                {{
                    ""exercise_name"": ""example_exercise_4"",
                    ""exercise_frequency"": ""details_4""
                }},
                {{
                    ""exercise_name"": ""example_exercise_5"",
                    ""exercise_frequency"": ""details_5""
                }},
                {{
                    ""exercise_name"": ""example_exercise_6"",
                    ""exercise_frequency"": ""details_6""
                }}
            ]
            " }
        }
            };

            using var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
            request.Headers.Add("Authorization", $"Bearer {_apiKey}");
            request.Content = JsonContent.Create(requestBody);

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var jsonDocument = JsonDocument.Parse(responseBody);
            var content = jsonDocument.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            try
            {
                var alternatives = JsonConvert.DeserializeObject<List<ExerciseAlternative>>(content);
                return alternatives ?? new List<ExerciseAlternative>();
            }
            catch (JsonSerializationException)
            {
                throw new Exception("JSON formatı beklenilen formatta değil. Yanıtı kontrol edin.");
            }
        }

        public Dictionary<string, Dictionary<string, string>> ParseWorkoutPlanForUpdate(string rawPlan)
        {
            var jsonDocument = JsonDocument.Parse(rawPlan);
            var fitnessAntrenman = jsonDocument.RootElement.GetProperty("fitness_antrenman");

            var parsedPlan = new Dictionary<string, Dictionary<string, string>>();

            foreach (var day in fitnessAntrenman.EnumerateArray())
            {
                var dayName = day.GetProperty("day").GetString();
                var program = day.GetProperty("program");

                var exercises = new Dictionary<string, string>();
                foreach (var exercise in program.EnumerateObject())
                {
                    exercises[exercise.Name] = exercise.Value.GetString();
                }

                parsedPlan[dayName] = exercises;
            }

            return parsedPlan;
        }


    }


    
}

