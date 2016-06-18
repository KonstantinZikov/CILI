using System.Collections.Generic;
using CilInterpreter.Executing;

namespace CilInterpreter.Syntaxis.ProgramParts.Atoms
{
    class CallAtom : Atom
    {
        public Method TargetMethod { get; set; }
        public bool InstanceCall { get; set; }

        public override void PreAction()
        {
            Assembly assembly = Helper.GetParentAssembly(Parent);
            if (assembly.Done == false)
            {               
                foreach (var atom in Atoms)
                {
                    atom.Value.Parent = this;
                    atom.Value.PreAction();
                }
                assembly.Continuation += PreAction;
            }
            else
            {
                object o;
                if (Info.TryGetValue("instance", out o))
                    InstanceCall = true;

                Assembly targetAssembly;
                Token assemblyName;
                if (Tokens.TryGetValue("assembly", out assemblyName) && assembly.Name != assemblyName.Value)
                {
                    targetAssembly = assembly.ReferencedAssemblies.Find((a) => a.Name == assemblyName.Value);
                }
                else
                    targetAssembly = assembly;
                if (targetAssembly == null)
                    throw new CiliPreparingException($"Unknown assembly {assemblyName.Value}");

                CilType resultType = (Atoms["type"] as TypeAtom).TargetType;
                int paramsCount = (int)Info["paramsCount"];
                List<CilType> paramTypes = new List<CilType>();
                var cili = Helper.GetCili(assembly);
                for (int i = 0; i < paramsCount; i++)
                {
                    var type = Atoms[$"param{i}"] as TypeAtom;
                    if ((bool)type.Info["predefined"] == true)
                    {
                        var targetClass = cili.Classes.Find(c => c?.LinkedType?.ShortName == type.Tokens["name"].Value);
                        paramTypes.Add(targetClass.LinkedType);
                    }
                    else
                    {
                        Token assemblyToken = null;
                        Assembly typeAssembly = assembly;
                        if (type.Tokens.TryGetValue("assembly", out assemblyToken))
                        {
                            typeAssembly = assembly.ReferencedAssemblies.Find(a => a.Name == assemblyToken.Value);
                            if (typeAssembly == null)
                                throw new CiliPreparingException($"Unknown assembly {assemblyToken.Value}");
                        }

                        string typeName = Tokens["name"].Value;
                        CilType targetType = typeAssembly.Classes.Find(c => c.Name == typeName)?.LinkedType;
                        if (targetType == null)
                            throw new CiliPreparingException($"Unknown type {typeName}");

                        paramTypes.Add(targetType);
                    }
                }
                List<Method> targetMethods = null;
                Token className;
                if (Tokens.TryGetValue("class", out className))
                {
                    Class targetClass = targetAssembly.Classes.Find((c) => c.Name == className.Value);
                    if (targetClass == null)
                        throw new CiliPreparingException($"Unknown class {className.Value}");
                    targetMethods = targetClass.Methods
                        .FindAll((m) => Equals(m.ResultType, resultType) &&
                        m.Name == Tokens["method"].Value);
                }
                else
                {
                    targetMethods = targetAssembly
                        .Methods.FindAll(m => Equals(m.ResultType, resultType) &&
                        m.Name == Tokens["method"].Value);
                }

                Method targetMethod = null;
                foreach (var method in targetMethods)
                {
                    if (method.ParamTypes.Count != paramTypes.Count) continue;
                    if (method.ParamTypes.Count == 0)
                    {
                        targetMethod = method;
                        break;
                    }
                    for (int i = 0; i < paramTypes.Count; i++)
                    {
                        if (!method.ParamTypes[i].Equals(paramTypes[i])) continue;
                        targetMethod = method;
                    }
                    if (targetMethod != null) break;
                }
                if (targetMethod == null || (targetMethod.ThisParam != null) != InstanceCall)
                    throw new CiliPreparingException("Unknown method ???");
                TargetMethod = targetMethod;
            }        
        }

        public override void Action(Context context)
        {
            context.ReturnsStack.Push(context.CurrentInstruction);
            context.CurrentInstruction = TargetMethod.Address - 4;
        }

        public override Atom Create() { return new CallAtom(); }
    }
}
