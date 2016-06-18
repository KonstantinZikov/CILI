using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CilInterpreter.Syntaxis.ProgramParts
{
    class SimpleAtomType : IAtomType
    {
        public SimpleAtomType(Atom atom, Func<TokenStream, bool> isFunction)
        {
            if (isFunction == null)
                throw new ArgumentNullException($"{nameof(isFunction)} is null.");
            _atom = atom;
            _isFunc = isFunction;
        }

        Func<TokenStream, bool> _isFunc;

        public object Parent
        {
            private get
            {
                throw new NotImplementedException();
            }

            set
            {

            }
        }

        private Atom _atom;

        public bool Valid(TokenStream codeStream)
        {
            return _isFunc(codeStream);
        }

        public Atom Get(TokenStream codeStream)
        {
            codeStream.CreatedAtom = _atom.Create();
            if (_isFunc(codeStream))
            {
                return codeStream.CreatedAtom;
            }
            return null;
        }

        public TokenStream Is(TokenStream codeStream)
        {
            _isFunc(codeStream);
            return codeStream;
        } 

        public Atom Create()
        {
            return _atom.Create();
        }
    }
}
