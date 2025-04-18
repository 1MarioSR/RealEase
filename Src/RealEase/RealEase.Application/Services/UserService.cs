using RealEase.Infrastructure.Core;
using RealEase.Infrastructure.Repositories;
using RealEase.Application.Dtos.User;
using RealEase.Domain.Entities;

namespace RealEase.Application.Services
{
    public class UserService 
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly UserRepository _userRepository;

        public UserService(UnitOfWork unitOfWork, UserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<List<UserDto>> GetAllUsersAsync(string filter)
        {
            var users = await _userRepository.GetAllAsync();

            var userDtos = new List<UserDto>();
            foreach (var user in users)
            {
                userDtos.Add(new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    PasswordHash = user.PasswordHash,
                    Role = user.Role,
                    IsActive = user.IsActive
                });
            }

            return userDtos;
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                PasswordHash = user.PasswordHash,
                Role = user.Role,
                IsActive = user.IsActive
            };
        }

        public async Task<int> AddUserAsync(UserDto dto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var user = new User
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    PasswordHash = dto.PasswordHash,
                    Role = dto.Role,
                    IsActive = dto.IsActive
                };

                await _userRepository.AddAsync(user);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();
                return user.Id;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                return 0;
            }
        }

        public async Task<bool> UpdateUserAsync(UserDto dto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var user = await _userRepository.GetByIdAsync(dto.Id);
                if (user == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return false;
                }

                user.FirstName = dto.FirstName;
                user.LastName = dto.LastName;
                user.Email = dto.Email;
                user.PhoneNumber = dto.PhoneNumber;
                user.PasswordHash = dto.PasswordHash;
                user.Role = dto.Role;
                user.IsActive = dto.IsActive;

                await _userRepository.UpdateAsync(user);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return false;
                }

                await _userRepository.DeleteAsync(id); 
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                return false;
            }
        }

    }
}
