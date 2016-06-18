using CilInterpreter.Executing;
using CilInterpreter.Syntaxis;
using CilInterpreter.Syntaxis.ProgramParts.Atoms;
using System;
using System.Collections.Generic;

namespace CilInterpreter
{
    internal static class ExecuteEngine
    {
        public static void Execute(Assembly executed, IEnumerable<Assembly> referenced, Context context, int maxOperations)
        {          
            executed.PotentialReferencedAssemblies = new List<Assembly>(referenced);
            executed.PreAction();            
            if (executed.EntryPoint == null)
                throw new CiliRuntimeException("Executing assembly doesn't have the entry point.");
            AddressInstalation(executed, context);
            context.CurrentInstruction = executed.EntryPoint.Address;

            context.ReturnsStack.Push(-4);
            try
            {
                int i = 0;
                while (context.CurrentInstruction != 0 && !context.Stopped && i < maxOperations)
                {
                    i++;
                    context.Instructions[context.CurrentInstruction].Action(context);
                    context.CurrentInstruction += 4;
                }
            }
            catch (Exception ex)
            {
                if (!context.Stopped)
                    throw ex;
            }
            context.Stopped = false;
        }

        private static void AddressInstalation(Assembly assembly, Context context)
        {
            List<Assembly> assemblies = new List<Assembly>(assembly.ReferencedAssemblies);
            assemblies.Add(assembly);
            int i = 4;           
            foreach (var a in assemblies)
            {
                i = MethodAddressInstalation(a.Methods, context, i);
                foreach (var c in a.Classes)
                    i = MethodAddressInstalation(c.Methods, context, i);
            }
            
        }

        private static int MethodAddressInstalation(IEnumerable<Method> methods, Context context, int i)
        {
            foreach (var m in methods)
            {
                m.Address = i;
                context.Instructions[i] = m;
                i += 4;
                List<BranchAtom> branches = new List<BranchAtom>();
                List<LabelAtom> labels = new List<LabelAtom>();
                foreach (var act in m.Actions)
                {
                    act.Address = i;
                    context.Instructions[i] = act;
                    i += 4;
                    var branch = act as BranchAtom;
                    if (branch != null)
                        branches.Add(branch);
                    else
                    {
                        var label = act as LabelAtom;
                        if (label != null)
                            labels.Add(label);
                    }
                }
                foreach (var br in branches)
                {
                    var label = labels.Find((l) => l.Name == br.TargetName);
                    if (label == null)
                        throw new CiliPreparingException($"Unknown label \"{br.TargetName}\".");
                    br.JumpAddress = label.Address;
                }
            }
            return i;
        }

    }
}
