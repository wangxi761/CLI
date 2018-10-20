using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace Tars.Net.CLI.Grammar
{
    public static class SyntaxFactoryExtensions
    {
        public static MemberDeclarationSyntax[] ToMemberDeclarationSyntaxArray(this IEnumerable<SyntaxNode> syntaxNodes)
        {
            return syntaxNodes
                .Select(i => i as MemberDeclarationSyntax)
                .Where(i => i != null)
                .ToArray();
        }
    }
}