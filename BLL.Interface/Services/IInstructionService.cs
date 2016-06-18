using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IInstructionService
    {
        InstructionEntity Get(int id);
        IEnumerable<InstructionEntity> GetAllEntities();
        void Create(InstructionEntity instruction);
        void Update(InstructionEntity instruction);
        void Delete(InstructionEntity instruction);
    }
}
