﻿using BLL.Interface.Entities;
using BLL.Interface.Services;
using DAL.Interfaces.Repository;
using DAL.Interfaces.DTO;
using System;
using BLL.Interface.Exceptions;

namespace BLL.Services
{
    public class InstructionService : BaseService<InstructionEntity, DalInstruction>, IInstructionService
    {
        public InstructionService(IUnitOfWork unitOfWork, IRepository<DalInstruction> repository)
            : base(unitOfWork, repository)
        {
            ToBll = (dal)=> new InstructionEntity()
            {
                Id = dal.Id,
                Name = dal.Name,
                Description = dal.Description,
                IsSupported = dal.IsSupported
            };
        }

        protected override DalInstruction ToDal(InstructionEntity bll)
        {
            return new DalInstruction()
            {
                Id = bll.Id,
                Name = bll.Name,
                Description = bll.Description,
                IsSupported = bll.IsSupported              
            };
        }

        protected override void Check(InstructionEntity entity)
        {
            if (entity.Id < 0)
                throw new ServiceException(
                    $"Instruction id must be greater then zero, but it is {entity.Id}");
            if (entity.Name == null)
                throw new ServiceException("Instruction name is null.");

        }
    }
}