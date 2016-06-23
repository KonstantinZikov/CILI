using System;
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

        public static UserViewModel ToModel(this UserEntity entity)
        {
            return new UserViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Mail = entity.Mail,
                RegistrationTime = entity.RegistrationTime,
                RoleId = entity.RoleId
            };
        }

        public static UserEntity ToBll(this UserViewModel model)
        {
            return new UserEntity()
            {
                Id = model.Id,
                Name = model.Name,
                Mail = model.Mail,
                Password = model.Password,
                RegistrationTime = model.RegistrationTime,
                RoleId = model.RoleId
            };
        }

        public static RoleViewModel ToModel(this RoleEntity entity)
        {
            return new RoleViewModel()
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public static RoleEntity ToBll(this RoleViewModel model)
        {
            return new RoleEntity()
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public static DocumentViewModel ToModel(this DocumentEntity entity)
        {
            return new DocumentViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                UserId = entity.UserId,
                LastChangeTime = entity.LastChangeTime,
                Code = entity.Code,
                Description = entity.Description,
                IsExample = entity.IsExample
            };
        }

        public static DocumentEntity ToBll(this DocumentViewModel model)
        {
            return new DocumentEntity()
            {
                Id = model.Id,
                Name = model.Name,
                UserId = model.UserId,
                LastChangeTime = model.LastChangeTime,
                Code = model.Code,
                Description = model.Description,
                IsExample = model.IsExample
            };
        }

    }
}