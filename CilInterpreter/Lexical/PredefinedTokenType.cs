using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CilInterpreter
{
    class PredefinedTokenType : ITokenType
    {
        IEnumerable<string> _validValues;

        public PredefinedTokenType(IEnumerable<string> validValues)
        {
            if (validValues == null)
                throw new ArgumentNullException($"{nameof(validValues)} is null.");
            _validValues = validValues;
        }

        public bool Is(string word)
        {
            foreach (var value in _validValues)
                if (Equals(value, word))
                    return true;
            return false;
        }
    }
}
