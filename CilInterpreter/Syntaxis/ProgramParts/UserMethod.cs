using CilInterpreter.Executing;

namespace CilInterpreter.Syntaxis.ProgramParts
{
    class UserMethod : Method
    {
        public override void PreAction()
        {
            var parent = Parent as Assembly;
            if (parent != null)
            {
                parent.Methods.Add(this);
                parent.Directives.Remove(this);
            }
            else
            {
                var classParent = Parent as Class;
                if (classParent == null)
                    throw new CiliPreparingException("method must be declared on Assembly or Class.");
                classParent.Methods.Add(this);
            }
            base.PreAction();
        }

        public override void Action(Context context)
        {
            context.MethodCall(StackSize); 
            LocalAddress = context.Memory.LocalPointer;
        }


        public override Atom Create() { return new UserMethod(); }
    }
}
