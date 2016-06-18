using CilInterpreter.Syntaxis.ProgramParts.Atoms;
using System.Collections.Generic;
using static CilInterpreter.TokenTypePool;

namespace CilInterpreter.Syntaxis.ProgramParts
{
    internal class AtomPool
    {
        static AtomPool()
        {
            MetaAtoms = new List<IAtomType>()
            {
                AssemblyDefenition,
                Type,
                UserMethod,
                ExternAssembly
            };

            MethodAtoms = new List<IAtomType>()
            {
                Add,
                MaxStack,
                EntryPoint,
                Print,
                Call,
                LoadString,
                Pop,
                Ret,
                Label,
                LoadInt4,
                InitLocals,
                LoadFromStackToLocal,
                LoadLocalAddressToStack,
                Branch,
                BranchEqual,
                BranchGreaterOrEqual,
                BranchGreater,
                BranchLessOrEqual,
                BranchLess,
                BranchFalse,
                BranchTrue,
                BranchNotEqual
            };
        }

        public static List<IAtomType> MetaAtoms { get; set; }
        public static List<IAtomType> MethodAtoms { get; set; }

        public static IAtomType AssemblyDefenition { get; private set; }
            = new SimpleAtomType(new AssemblyDefenitionAtom(),t => t
                .Is(".assembly")
                .Is(Identifier).As("name")
                .Is("{")
                .Is("}")
                .End
            );

        public static IAtomType MaxStack { get; private set; }
            = new SimpleAtomType(new MaxStackAtom(), t => t
                .Is(".maxstack")
                .Is(Number).As("size")
                .End
            );

        public static IAtomType EntryPoint { get; private set; }
            = new SimpleAtomType(new EntryPointAtom(),t => t
                .Is(".entrypoint")
                .End
            );

        public static IAtomType Call { get; private set; }
            = new SimpleAtomType(new CallAtom(), t => t
                .Is("call")
                .ZeroOrOne(() => t
                    .Is("instance").Info("instance", null)
                )
                .Atom(Type).As("type")
                .ZeroOrOne(()=>t
                    .ZeroOrOne(() => t
                        .Is("[").Is(Identifier).As("assembly").Is("]")
                    )
                    .Is(Identifier).As("class")
                    .Is("::")
                )              
                .Is(Identifier).As("method")
                .Is("(")
                .ZeroOrOne(()=>t
                    .Atom(Type).As($"param{t.C}")
                    .CInc()
                    .ZeroOrMore(()=>t
                        .Is(",")
                        .Atom(Type).As($"param{t.C}")
                        .CInc()
                    )
                 ).Info("paramsCount", t.C).DropC()
                .Is(")").End
            );

        public static IAtomType Type { get; private set; }
            = new SimpleAtomType(new TypeAtom(), t => t
                .OneOf(
                    () => t.Is(TokenTypePool.Type).As("name").Info("predefined", true),
                    () => t.Bounds("[", Identifier, "]").As("assembly")
                            .Is(Identifier).As("name").Info("predefined",false)
                 ).End
            );

        public static IAtomType MethodHeader { get; private set; }
        = new SimpleAtomType(new MethodHeaderAtom(), t => t
            .Is(".method")
            .ZeroOrMore(() => t
                .OneOf(
                    () => t.OneOf("public", "private").As($"accessModifier{t.V[0]}").Do(() => t.V[0] = t.V[0] + 1),
                    () => t.Is("static").As($"modifier{t.V[1]}").Do(() => t.V[1] = t.V[1] + 1)
                )
            ).Info("accessModifiersCount", t.V[0]).Info("modifiersCount", t.V[1]).DropV()
            .Atom(Type).As("type")
            .Is(Identifier).As("name")
            .Is("(").Is(")").End           
        );

        public static IAtomType MethodBody { get; private set; }
            = new SimpleAtomType(new MethodBodyAtom(),  t => t
                .Bounds
                ("{",() => t
                    .ZeroOrMore(()=>t
                        .OneOf(MethodAtoms).As($"action{t.C}")
                        .CInc()
                    ),
                 "}")
                .Info("actionsCount",t.C).DropC()
                .End
        );

        public static IAtomType UserMethod { get; private set; }
            = new SimpleAtomType(new UserMethod(),t => t
                .Atom(MethodHeader).As("header")
                .Atom(MethodBody).As("body")
                .End
        );

        public static IAtomType LoadString { get; private set; }
            = new SimpleAtomType(new LoadStringAtom(), t => t
                .Is("ldstr")
                .Is(String).As("string")
                .End
        );

        public static IAtomType LoadInt4 { get; private set; }
            = new SimpleAtomType(new LoadInt4Atom(), t => t
                 .Is("ldc.i4")
                 .Is(Number).As("number")
                 .End
            );

        public static IAtomType ExternAssembly { get; private set; }
            = new SimpleAtomType(new ExternAssemblyAtom(), t => t
                .Is(".assembly")
                .Is("extern")
                .Is(Identifier).As("name")
                .Is("{").Is("}")
                .End
        );

        public static IAtomType Pop { get; private set; }
            = new SimpleAtomType(new PopAtom(), t => t
                .Is("pop")   
                .End
        );

        public static IAtomType Ret { get; private set; }
            = new SimpleAtomType(new RetAtom(), t => t
                .Is("ret")
                .End
        );

        public static IAtomType Label { get; private set; }
            = new SimpleAtomType(new LabelAtom(), t => t
                .Is(Identifier).As("name")
                .Is(":")
                .End
        );

        public static IAtomType Print { get; private set; }
            = new SimpleAtomType(new PrintAtom(), t => t
                .Is("print")
                .Is(String).As("string")
                .End
        );

        public static IAtomType LoadFromStackToLocal { get; private set; }
            = new SimpleAtomType(new LoadFromStackToLocalAtom(), t => t
                .Is("stloc")
                .OneOf(Identifier,Number).As("identifier")
                .End
        );

        public static IAtomType LoadLocalAddressToStack { get; private set; }
            = new SimpleAtomType(new LoadLocalAddressToStackAtom(), t => t
                .Is("ldloca")
                .OneOf(Identifier, Number).As("identifier")
                .End
        );

        public static IAtomType InitLocals { get; private set; }
            = new SimpleAtomType(new InitLocalsAtom(), t => t
                .Is(".locals")
                .Is("init")
                .Is("(")
                .ZeroOrOne(()=>t
                    .Atom(Type).As($"type{t.C}")
                    .ZeroOrOne(() => t
                        .Is(Identifier).As($"name{t.C}")
                    )
                    .CInc()
                    .ZeroOrMore(()=>t
                        .Is(",")
                        .Atom(Type).As($"type{t.C}")
                        .ZeroOrOne(()=>t
                            .Is(Identifier).As($"name{t.C}")
                        )
                        .CInc()
                    )
                ).Info("localCount",t.C).DropC()
                .Is(")")
                .End
            //.locals init (string, int32 V_0, [mscorlib]System.Activator V_1)
        );

        public static IAtomType Add { get; private set; }
           = new SimpleAtomType(new AddAtom(), t => t
               .Is("add")
               .End
        );

        public static IAtomType Branch { get; private set; }
           = new SimpleAtomType(new BranchAtom(), t => t
               .Is("br")
               .Is(Identifier).As("target")
               .End
        );

        public static IAtomType BranchEqual { get; private set; }
           = new SimpleAtomType(new BranchEqualAtom(), t => t
               .Is("beq")
               .Is(Identifier).As("target")
               .End
        );

        public static IAtomType BranchGreaterOrEqual { get; private set; }
           = new SimpleAtomType(new BranchGreaterOrEqualAtom(), t => t
               .Is("bge")
               .Is(Identifier).As("target")
               .End
        );

        public static IAtomType BranchGreater { get; private set; }
           = new SimpleAtomType(new BranchGreaterAtom(), t => t
               .Is("bgt")
               .Is(Identifier).As("target")
               .End
        );

        public static IAtomType BranchLessOrEqual { get; private set; }
           = new SimpleAtomType(new BranchLessOrEqualAtom(), t => t
               .Is("ble")
               .Is(Identifier).As("target")
               .End
        );

        public static IAtomType BranchLess { get; private set; }
           = new SimpleAtomType(new BranchLessAtom(), t => t
               .Is("blt")
               .Is(Identifier).As("target")
               .End
        );

        public static IAtomType BranchFalse { get; private set; }
           = new SimpleAtomType(new BranchFalseAtom(), t => t
               .OneOf("brfalse","brnull","brzero")
               .Is(Identifier).As("target")
               .End
        );

        public static IAtomType BranchTrue { get; private set; }
           = new SimpleAtomType(new BranchTrueAtom(), t => t
               .OneOf("brtrue", "brinst")
               .Is(Identifier).As("target")
               .End
        );

        public static IAtomType BranchNotEqual { get; private set; }
           = new SimpleAtomType(new BranchNotEqualAtom(), t => t
               .Is("bne")
               .Is(Identifier).As("target")
               .End
        );

    }
}
