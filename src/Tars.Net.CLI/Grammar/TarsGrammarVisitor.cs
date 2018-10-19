using Antlr4.Runtime.Misc;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tars.Net.CLI.Grammar
{
    public class TarsGrammarVisitor : GrammarBaseVisitor<CSharpSyntaxNode>
    {
        public override CSharpSyntaxNode VisitTarsDefinitions([NotNull] GrammarParser.TarsDefinitionsContext context)
        {
            var compilationUnit = SyntaxFactory.CompilationUnit();
            //compilationUnit.AddMembers(SyntaxFactory.List(context.tarsDefinition().Select(i => i.moduleDefinition()).Select(VisitTarsDefinition)));
            return compilationUnit;
        }
    }
}
