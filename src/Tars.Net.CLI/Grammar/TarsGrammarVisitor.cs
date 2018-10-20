using Antlr4.Runtime.Misc;
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
                VisitEnumDefinition(context.enumDefinition()),
                VisitStructDefinition(context.structDefinition()),
                VisitInterfaceDefinition(context.interfaceDefinition())
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

        public override CSharpSyntaxNode VisitEnumDefinition([NotNull] GrammarParser.EnumDefinitionContext context)
        {
            if (context == null)
            {
                return null;
            }
            var name = SyntaxFactory.IdentifierName(context.name().GetText());
            var enumDec = SyntaxFactory.EnumDeclaration(name.GetFirstToken())
                .AddMembers(context.enumDeclaration()
                    .Select(VisitEnumDeclaration)
                    .Select(i => i as EnumMemberDeclarationSyntax)
                    .Where(i => i != null)
                    .ToArray());
            
            return enumDec;
        }

        public override CSharpSyntaxNode VisitEnumDeclaration([NotNull] GrammarParser.EnumDeclarationContext context)
        {
            if (context == null)
            {
                return null;
            }
            var name = SyntaxFactory.IdentifierName(context.name().GetText());
            var enumMemberDec = SyntaxFactory.EnumMemberDeclaration(name.GetFirstToken());
            var value = context.fieldValue();
            if (value != null)
            {
                enumMemberDec = enumMemberDec.WithEqualsValue(SyntaxFactory.EqualsValueClause(SyntaxFactory.ParseExpression(value.GetText())));
            }
            return enumMemberDec;
        }

        public override CSharpSyntaxNode VisitInterfaceDefinition([NotNull] GrammarParser.InterfaceDefinitionContext context)
        {
            if (context == null)
            {
                return null;
            }
            var name = SyntaxFactory.IdentifierName(context.name().GetText());
            var interfaceDec = SyntaxFactory.InterfaceDeclaration(name.GetFirstToken())
                .AddMembers(context.methodDefinition()
                    .Select(VisitMethodDefinition)
                    .ToMemberDeclarationSyntaxArray());

            return interfaceDec;
        }

        public override CSharpSyntaxNode VisitMethodDefinition([NotNull] GrammarParser.MethodDefinitionContext context)
        {
            var name = SyntaxFactory.IdentifierName(context.name().GetText());
            var returnTypeDec = VisitTypeDeclaration(context.typeDeclaration()) as TypeSyntax;
            var info = returnTypeDec.GetFirstToken().Text;
            returnTypeDec = SyntaxFactory.ParseName(string.Equals("void", info, StringComparison.OrdinalIgnoreCase)
                    ? "Task"
                    : $"Task<{info}>");

            var methodDec = SyntaxFactory.MethodDeclaration(returnTypeDec, name.GetFirstToken())
                .AddParameterListParameters(context.methodParameterDefinition()
                    .Select(VisitMethodParameterDefinition)
                    .Select(i => i as ParameterSyntax)
                    .Where(i => i != null)
                    .ToArray());
            return methodDec;
        }

        public override CSharpSyntaxNode VisitMethodParameterDefinition([NotNull] GrammarParser.MethodParameterDefinitionContext context)
        {
            var name = SyntaxFactory.IdentifierName(context.name().GetText());
            var parameter = SyntaxFactory.Parameter(name.GetFirstToken());
            return parameter;
        }
    }
}