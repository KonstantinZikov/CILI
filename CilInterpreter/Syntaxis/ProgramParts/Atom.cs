using CilInterpreter.Executing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CilInterpreter.Syntaxis.ProgramParts
{
    internal abstract class Atom
    {
        public abstract Atom Create();
        public Atom Parent { get; set; }
        public int Address { get; set; }
        public Dictionary<string, Token> Tokens { get; } = new Dictionary<string, Token>();
        public Dictionary<string, Atom> Atoms { get; } = new Dictionary<string, Atom>();
        public Dictionary<string, object> Info { get; } = new Dictionary<string, object>();
        public IAtomType Type { get; set; }
        public virtual void Action(Context context) { }
        public virtual void PreAction()
        {
            foreach (var atom in Atoms)
            {
                atom.Value.Parent = this;
                atom.Value.PreAction();
            }
        } 
    }
}
