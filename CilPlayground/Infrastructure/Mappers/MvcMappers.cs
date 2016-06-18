using BLL.Interface.Entities;
using CilPlayground.Models;

namespace CilPlayground.Infrastructure.Mappers
{
    public static class MvcMappers
    {
        public static InstructionViewModel ToModel(this InstructionEntity entity)
        {
            return new InstructionViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                IsSupported = entity.IsSupported
            };
        }

        public static InstructionEntity ToBll(this InstructionViewModel model)
        {
            return new InstructionEntity()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                IsSupported = model.IsSupported
            };
        }
    }
}