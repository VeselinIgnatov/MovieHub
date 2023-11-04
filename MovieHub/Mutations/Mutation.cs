using HotChocolate.Authorization;
using MovieHub.Data.Models;
using MovieHub.Data.Repositories.Interfaces;
using MovieHub.Types.User;
using MovieHub.Utilities.Services;

namespace MovieHub.Mutations
{
    public class Mutation
    {
        public async Task<bool> CreateUser(IUserRepository userRepository, CreateUserInput userInput)
        {
            var user = new User
            {
                FirstName = userInput.FirstName,
                LastName = userInput.LastName,
                Email = userInput.Email,
                Password = userInput.Password,
            };

            return await userRepository.AddAsync(user);
        }

        public async Task<LoginUserOutput> LoginUser(
            IUserRepository userRepository,
            IAuthenticationService authenticationService,
            LoginUserInput userInput)
        {
            var user = await userRepository.LoginAsync(userInput.Email, userInput.Password);

            if (user == null) return null;

            var token = authenticationService.IssueJwtToken(user.Id, user.FirstName, user.LastName);

            var userOutput = new LoginUserOutput
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = token
            };

            return userOutput;
        }
    }
}