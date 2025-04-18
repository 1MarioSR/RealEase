using RealEase.Application.Dtos.Propertie;
using RealEase.Domain.Entities;
using RealEase.Infrastructure.Core;


namespace RealEase.Application.Services
{
    public class PropertieService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly PropertieRepository _propertieRepository;

        public PropertieService(UnitOfWork unitOfWork, PropertieRepository propertieRepository)
        {
            _unitOfWork = unitOfWork;
            _propertieRepository = propertieRepository;
        }

        public async Task<List<PropertieDto>> GetAllPropertiesAsync(string filter)
        {
            var properties = await _propertieRepository.GetAllAsync();

            var propertieDtos = new List<PropertieDto>();
            foreach (var propertie in properties)
            {
                propertieDtos.Add(new PropertieDto
                {
                    Id = propertie.Id,
                    Title = propertie.Title,
                    Image = propertie.Image,
                    Description = propertie.Description,
                    Address = propertie.Address,
                    Price = propertie.Price,
                    PropertyType = propertie.PropertyType,
                    Status = propertie.Status,
                    OwnerId = propertie.OwnerId
                });
            }

            return propertieDtos;
        }

        public async Task<PropertieDto> GetPropertieByIdAsync(int id)
        {
            var propertie = await _propertieRepository.GetByIdAsync(id);
            if (propertie == null) return null;

            return new PropertieDto
            {
                Id = propertie.Id,
                Title = propertie.Title,
                Image = propertie.Image,
                Description = propertie.Description,
                Address = propertie.Address,
                Price = propertie.Price,
                PropertyType = propertie.PropertyType,
                Status = propertie.Status,
                OwnerId = propertie.OwnerId
            };
        }

        public async Task<int> AddPropertieAsync(PropertieDto dto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var propertie = new Propertie
                {
                    Title = dto.Title,
                    Image = dto.Image,
                    Description = dto.Description,
                    Address = dto.Address,
                    Price = dto.Price,
                    PropertyType = dto.PropertyType,
                    Status = dto.Status,
                    OwnerId = dto.OwnerId
                };

                await _propertieRepository.CreateAsync(propertie);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();
                return propertie.Id;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                return 0;
            }
        }

        public async Task<bool> UpdatePropertieAsync(PropertieDto dto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var propertie = await _propertieRepository.GetByIdAsync(dto.Id);
                if (propertie == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return false;
                }

                propertie.Title = dto.Title;
                propertie.Image = dto.Image;
                propertie.Description = dto.Description;
                propertie.Address = dto.Address;
                propertie.Price = dto.Price;
                propertie.PropertyType = dto.PropertyType;
                propertie.Status = dto.Status;
                propertie.OwnerId = dto.OwnerId;

                await _propertieRepository.UpdateAsync(propertie);
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

        public async Task<bool> DeletePropertieAsync(int id)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var propertie = await _propertieRepository.GetByIdAsync(id);
                if (propertie == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return false;
                }

                await _propertieRepository.DeleteAsync(id);
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