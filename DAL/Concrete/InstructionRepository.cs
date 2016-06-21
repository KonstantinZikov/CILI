using System.Data.Entity;
using DAL.Interfaces.DTO;
using ORM;
using System;

namespace DAL.Concrete
{
    public class InstructionRepository : BaseRepositpry<DalInstruction, Instruction>
    {
        public InstructionRepository(DbContext context) : base(context)
        {
            ToDal = (instruction) =>
                new DalInstruction()
                {
                    Id = instruction.Id,
                    Name = instruction.Name,
                    Description = instruction.Description,
                    IsSupported = instruction.IsSupported
                };
        }

        protected override Instruction ToOrm(DalInstruction instruction)
        {
            if (instruction == null)
                throw new ArgumentNullException("instruction is null.");
            return new Instruction
            {
                Id = instruction.Id,
                Name = instruction.Name,
                Description = instruction.Description,
                IsSupported = instruction.IsSupported
            };
        }

        protected override void Update(DalInstruction dal, Instruction instruction)
        {
            if (dal == null)
                throw new ArgumentNullException("dal is null.");
            if (instruction == null)
                throw new ArgumentNullException("instruction is null.");
            instruction.Name = dal.Name;
            instruction.Description = dal.Description;
            instruction.IsSupported = dal.IsSupported;
        }
    }
}