using AutoMapper;
using FitAIAPI.Application.DTOs;
using FitAIAPI.Application.Interfaces;
using FitAIAPI.Domain.Entities;
using FitAIAPI.Domain.Repositories;
using FitAIAPI.Domain.Repositories.WorkoutPlan;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Globalization;

namespace FitAIAPI.Application.Services
{
    public class UserService : BaseService<User, UserAccountDTO>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWorkoutPlanRepository _workoutPlanRepository;

        public UserService(IGenericRepository<User> repository, IMapper mapper, IUserRepository userRepository, IAuthService authService, IPasswordHasher<User> passwordHasher, IHttpContextAccessor httpContextAccessor, IWorkoutPlanRepository workoutPlanRepository) : base(repository, mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _authService = authService;
            _passwordHasher = passwordHasher;
            _httpContextAccessor = httpContextAccessor;
            _workoutPlanRepository = workoutPlanRepository;
        }



        private int GetUserIdFromToken()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("Id");
            if (userIdClaim == null)
                throw new UnauthorizedAccessException("User ID not found in token");

            return int.Parse(userIdClaim.Value);
        }



        public async Task<LoginResponse> RegisterAndLoginUserAsync(UserRegisterRequest requestModel)
        {
            var userCheck = await _userRepository.UserExistenceCheckAsync(requestModel.Email);
            if (!userCheck)
            {
                var entity = _mapper.Map<User>(requestModel);
                entity.Password = _passwordHasher.HashPassword(entity, requestModel.Password);
                await _userRepository.AddAsync(entity);

                
                var loginRequest = new UserLoginRequest
                {
                    Email = requestModel.Email,
                    Password = requestModel.Password
                };

                return await LoginUserAsync(loginRequest);
            }
            else
            {
                throw new UserAlreadyExistsException(requestModel.Email);
            }
        }


        public async Task<LoginResponse> LoginUserAsync(UserLoginRequest requestModel)
        {
            var user = await _userRepository.UserCheckAsync(requestModel.Email);
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, requestModel.Password);

            if (result != PasswordVerificationResult.Success)
                throw new UserPasswordIncorrectException(user.Email);

            var token = _authService.Token(user);
            if (user.IsFirstLogin)
            {
                user.IsFirstLogin = false;
                await _userRepository.UpdateAsync(user);
            }

            return new LoginResponse
            {
                Token = token,
                IsFirstLogin = user.IsFirstLogin
            };
        }


        public async Task<bool> SaveUserFirstLoginDetailsAsync(UserFirstLoginDTO userFirstLoginDTO)
        {
            int userId = GetUserIdFromToken();
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
                throw new UserNotFoundException(userId.ToString());

            
            user.UserDetail ??= new UserDetail { UserId = user.Id };
            

            _mapper.Map(userFirstLoginDTO, user.UserDetail);
            user.UserDetail.CurrentWeight = userFirstLoginDTO.FirstWeight; 


            user.UserDetail.BasalMetabolism = CalculateBasalMetabolism(user.UserDetail);

            return await _userRepository.UpdateAsync(user);
        }




        public async Task SaveWorkoutDetailsAsync(UserWorkoutDetailsDTO workoutDetailsDTO)
        {
            int userId = GetUserIdFromToken();
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
                throw new UserNotFoundException(userId.ToString());

            user.UserDetail ??= new UserDetail { UserId = userId };

            _mapper.Map(workoutDetailsDTO, user.UserDetail);

            

            if (!user.UserDetail.BasalMetabolism.HasValue)
                throw new InvalidOperationException("Basal Metabolism değeri null. Lütfen ilk giriş bilgilerini kaydedin.");
            
            user.UserDetail.DailyKcalGoal = CalculateDailyKcalGoal(user.UserDetail);
            await _userRepository.UpdateAsync(user);
        }


        private DateTime ParseDateOfBirth(string dateOfBirth)
        {
            var format = "yyyy-MM-dd";
            if (DateTime.TryParseExact(dateOfBirth, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                return parsedDate;
            }
            throw new FormatException("Invalid date format. Please use the format: yyyy-MM-dd.");
        }


        private int CalculateBasalMetabolism(UserDetail userDetail)
        {
            
            var dateOfBirth = ParseDateOfBirth(userDetail.DateOfBirth);
            int age = DateTime.UtcNow.Year - dateOfBirth.Year;
            if (userDetail.Gender.ToLower() == "erkek")
            {
                return (int)(88.36 + (13.4 * userDetail.CurrentWeight.Value) + (4.8 * userDetail.Height.Value) - (5.7 * age));
            }
            else
            {
                return (int)(447.6 + (9.2 * userDetail.CurrentWeight.Value) + (3.1 * userDetail.Height.Value) - (4.3 * age));
            }
        }


        private int CalculateDailyKcalGoal(UserDetail userDetail)
        {

            int workoutMultiplier = userDetail.WorkoutFrequency.ToLower() switch
            {
                "onerilen" => 3,
                "haftada 1-2" => 1,
                "haftada 2-3" => 1,
                "haftada 3-4" => 2,
                "haftada 4-5" => 2,
                "haftada 5-6" => 3,
                _ => 1
            };

            return userDetail.BasalMetabolism.Value * workoutMultiplier;
        }


        public async Task<UserDetailDTO> GetUserDetailsAsync()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("Id");
            if (userIdClaim == null)
                throw new UnauthorizedAccessException("User ID not found in token");
            
            
            int userId = int.Parse(userIdClaim.Value);
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
                throw new UserNotFoundException(userId.ToString());
            
            
            if (user.UserDetail == null)
                throw new Exception("UserDetail Not Found");
            
            

            var userDetailDTO = _mapper.Map<UserDetailDTO>(user);


            var workoutPlan = await _workoutPlanRepository.GetByUserIdAsync(userId);
            if (workoutPlan != null)
            {
                userDetailDTO.WorkoutPlan = JsonConvert.DeserializeObject<FitnessPlan>(workoutPlan.Program);
            }

            return userDetailDTO;
        }

        

        public async Task<bool> UpdateUserAsync(UserAccountDTO requestModel)
        {
            
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("Id");
            
            if (userIdClaim == null) 
                throw new UnauthorizedAccessException("User ID not found in token");



            int userId = int.Parse(userIdClaim.Value);
            var user = await _userRepository.GetByIdAsync(userId);
            
            if (user == null)
                throw new UserNotFoundException(userId.ToString());
            

            _mapper.Map(requestModel, user);


            if (!string.IsNullOrEmpty(requestModel.Password))
                user.Password = _passwordHasher.HashPassword(user, requestModel.Password);
            
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<UserProfileDTO> UpdateUserProfileAsync(UserProfileDTO userProfileDTO)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("Id");
            if (userIdClaim == null)
                throw new UnauthorizedAccessException("User ID not found in token");
            
            int userId = int.Parse(userIdClaim.Value);
            var user = await _userRepository.GetByIdAsync(userId);
            
            if (user == null)
                throw new UserNotFoundException(userId.ToString());
            


            user.UserDetail ??= new UserDetail { UserId = user.Id };
            _mapper.Map(userProfileDTO, user.UserDetail);

            await _userRepository.UpdateAsync(user);

            return _mapper.Map<UserProfileDTO>(user.UserDetail);
        }



        public async Task EditUserRoleToAdminAsync(int id)
        {
            await _userRepository.EditUserRoleToAdminAsync(id);
        }

        public async Task<UserAccountDTO> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserAccountDTO>(user);
        }

        public async Task<List<UserGetRequest>> GetUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<List<UserGetRequest>>(users);
        }


    }
}