using Antlr4.Runtime.Misc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;

namespace Tars.Net.CLI.Grammar
{
    public class TarsGrammarVisitor : GrammarBaseVisitor<CSharpSyntaxNode>
    {
        public override CSharpSyntaxNode VisitTarsDefinitions([NotNull] GrammarParser.TarsDefinitionsContext context)
        {
            var compilationUnit = SyntaxFactory.CompilationUnit();
            var namespaces = context.tarsDefinition()
                .Select(VisitTarsDefinition)
                .ToMemberDeclarationSyntaxArray();

            compilationUnit = compilationUnit.AddMembers(namespaces);
            return compilationUnit;
        }

        public override CSharpSyntaxNode VisitTarsDefinition([NotNull] GrammarParser.TarsDefinitionContext context)
        {
            return new CSharpSyntaxNode[]
            {
                //VisitIncludeDefinition(context.includeDefinition()),
                VisitModuleDefinition(context.moduleDefinition())
            }
            .FirstOrDefault(i => i != null);
        }

        //public override CSharpSyntaxNode VisitIncludeDefinition([NotNull] GrammarParser.IncludeDefinitionContext context)
        //{
        //    return null;
        //}

        public override CSharpSyntaxNode VisitModuleDefinition([NotNull] GrammarParser.ModuleDefinitionContext context)
        {
            if (context == null)
            {
                return null;
            }

            var name = SyntaxFactory.IdentifierName(context.moduleName().GetText())
                     ;
            var namespaceDec = SyntaxFactory.NamespaceDeclaration(name);
            var members = context.memberDefinition()
                .Select(VisitMemberDefinition)
                .ToMemberDeclarationSyntaxArray();
            namespaceDec = namespaceDec.AddMembers(members);
            return namespaceDec;
        }

        public override CSharpSyntaxNode VisitMemberDefinition([NotNull] GrammarParser.MemberDefinitionContext context)
        {
            return new CSharpSyntaxNode[]
            {
                VisitStructDefinition(context.structDefinition()),
                //VisitEnumDefinition(context.enumDefinition()),
                //VisitInterfaceDefinition(context.interfaceDefinition())
            }
            .FirstOrDefault(i => i != null);
        }

        public override CSharpSyntaxNode VisitStructDefinition([NotNull] GrammarParser.StructDefinitionContext context)
        {
            if (context == null)
            {
                return null;
            }
            var name = SyntaxFactory.IdentifierName(context.name().GetText());
            var classDec = SyntaxFactory.ClassDeclaration(name.GetFirstToken())
                .AddAttributeLists(SyntaxFactory.AttributeList(
                    SyntaxFactory.SingletonSeparatedList(SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("TarsStruct")))))
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddMembers(context.fieldDefinition()
                    .Select(VisitFieldDefinition)
                    .ToMemberDeclarationSyntaxArray());
            return classDec;
        }

        public override CSharpSyntaxNode VisitFieldDefinition([NotNull] GrammarParser.FieldDefinitionContext context)
        {
            var typeDec = VisitTypeDeclaration(context.typeDeclaration()) as TypeSyntax;
            if (string.Equals("optional", context.fieldOption().GetText())
                && typeDec.Kind() == SyntaxKind.PredefinedType
                && !string.Equals("string", typeDec.GetFirstToken().Text))
            {
                typeDec = SyntaxFactory.NullableType(typeDec);
            }
            
            var propertyDec = SyntaxFactory.PropertyDeclaration(typeDec, context.name().GetText())
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddAttributeLists(SyntaxFactory.AttributeList(SyntaxFactory.SingletonSeparatedList(
                    SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("TarsStructProperty"), SyntaxFactory.AttributeArgumentList(SyntaxFactory.SingletonSeparatedList(
                        SyntaxFactory.AttributeArgument(SyntaxFactory.ParseExpression(context.fieldOrder().GetText()))
                    ))))))
                .AddAccessorListAccessors(
                                    SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)),
                                    SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken)));
            var value = context.fieldValue();
            if (value != null)
            {
                propertyDec = propertyDec.WithInitializer(SyntaxFactory.EqualsValueClause(SyntaxFactory.ParseExpression(value.GetText())))
                    .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));
            }
            return propertyDec;
        }

        public override CSharpSyntaxNode VisitTypeDeclaration([NotNull] GrammarParser.TypeDeclarationContext context)
        {
            var name = context.GetText()
                .Replace("vector<", "List<", StringComparison.OrdinalIgnoreCase)
                .Replace("map<", "Dictionary<", StringComparison.OrdinalIgnoreCase);
            return SyntaxFactory.ParseTypeName(name);
        }
    }
}