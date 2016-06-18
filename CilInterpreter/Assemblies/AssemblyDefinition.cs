namespace CilInterpreter.Assemblies
{
    abstract class AssemblyDefinition
    {
        Assembly _instance;
        public Assembly Instance
        {
            get
            {
                if (_instance == null)
                    _instance = Create();
                return _instance;
            }
        }

        protected abstract Assembly Create();
       
    }
}
