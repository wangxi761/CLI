using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tars.Net.CLI.Grammar
{
    public class TarsGrammarVisitor : GrammarBaseVisitor<SyntaxNode>
    {
        private readonly string[] usings = new string[]
        {
            "System",
            "System.Collections.Generic",
            "System.Linq",
            "System.Threading.Tasks",
            "Tars.Net.Attributes"
        };
        private readonly string file;

        public TarsGrammarVisitor(string file)
        {
            this.file = file;
        }

        public override SyntaxNode VisitTarsDefinition([NotNull] GrammarParser.TarsDefinitionContext context)
        {
            var compilationUnit = SyntaxFactory.CompilationUnit();
            var namespaces = context.moduleDefinition()
                .Select(VisitModuleDefinition)
                .ToMemberDeclarationSyntaxArray();

            var usingDecs = usings.Select(i => SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(i)))
                .Union(context.includeDefinition()
                    .SelectMany(GetUsingDirective))
                .Where(i => i != null)
                .ToArray();

            compilationUnit = compilationUnit.AddMembers(namespaces)
                .AddUsings(usingDecs);
            return compilationUnit;
        }

        private IEnumerable<UsingDirectiveSyntax> GetUsingDirective(GrammarParser.IncludeDefinitionContext context)
        {
            var newfile = context.String().GetText();
            var newPath = file.Replace(Path.GetFileName(file), newfile.Replace("\"", ""));
            if (File.Exists(newPath))
            {
                using (var stream = File.OpenRead(newPath))
                {
                    var lexer = new GrammarLexer(new AntlrInputStream(stream));
                    var parser = new GrammarParser(new CommonTokenStream(lexer));
                    var syntax = Visit(parser.tarsDefinition()) as CompilationUnitSyntax;
                    return syntax.Members.Select(i => i as NamespaceDeclarationSyntax)
                        .Where(i => i != null)
                        .Select(i => SyntaxFactory.UsingDirective(i.Name));
                }
            }
            else
            {
                return new UsingDirectiveSyntax[0];
            }
        }

        public override SyntaxNode VisitModuleDefinition([NotNull] GrammarParser.ModuleDefinitionContext context)
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

        public override SyntaxNode VisitMemberDefinition([NotNull] GrammarParser.MemberDefinitionContext context)
        {
            return new SyntaxNode[]
            {
                VisitEnumDefinition(context.enumDefinition()),
                VisitStructDefinition(context.structDefinition()),
                VisitInterfaceDefinition(context.interfaceDefinition())
            }
            .FirstOrDefault(i => i != null);
        }

        public override SyntaxNode VisitStructDefinition([NotNull] GrammarParser.StructDefinitionContext context)
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

        public override SyntaxNode VisitFieldDefinition([NotNull] GrammarParser.FieldDefinitionContext context)
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

        public override SyntaxNode VisitTypeDeclaration([NotNull] GrammarParser.TypeDeclarationContext context)
        {
            var name = context.GetText()
                .Replace("vector<byte>", "byte[]", StringComparison.OrdinalIgnoreCase)
                .Replace("vector<", "List<", StringComparison.OrdinalIgnoreCase)
                .Replace("map<", "Dictionary<", StringComparison.OrdinalIgnoreCase);
            return SyntaxFactory.ParseTypeName(name);
        }

        public override SyntaxNode VisitEnumDefinition([NotNull] GrammarParser.EnumDefinitionContext context)
        {
            if (context == null)
            {
                return null;
            }
            var name = SyntaxFactory.IdentifierName(context.name().GetText());
            var enumDec = SyntaxFactory.EnumDeclaration(name.GetFirstToken())
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddMembers(context.enumDeclaration()
                    .Select(VisitEnumDeclaration)
                    .Select(i => i as EnumMemberDeclarationSyntax)
                    .Where(i => i != null)
                    .ToArray());

            return enumDec;
        }

        public override SyntaxNode VisitEnumDeclaration([NotNull] GrammarParser.EnumDeclarationContext context)
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

        public override SyntaxNode VisitInterfaceDefinition([NotNull] GrammarParser.InterfaceDefinitionContext context)
        {
            if (context == null)
            {
                return null;
            }
            var name = SyntaxFactory.IdentifierName(context.name().GetText());
            var interfaceDec = SyntaxFactory.InterfaceDeclaration(name.GetFirstToken())
                .AddAttributeLists(SyntaxFactory.AttributeList(
                    SyntaxFactory.SingletonSeparatedList(SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("Rpc")))))
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddMembers(context.methodDefinition()
                    .Select(VisitMethodDefinition)
                    .ToMemberDeclarationSyntaxArray());

            return interfaceDec;
        }

        public override SyntaxNode VisitMethodDefinition([NotNull] GrammarParser.MethodDefinitionContext context)
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
            return methodDec.WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));
        }

        public override SyntaxNode VisitMethodParameterDefinition([NotNull] GrammarParser.MethodParameterDefinitionContext context)
        {
            var name = SyntaxFactory.IdentifierName(context.name().GetText());
            var returnTypeDec = VisitTypeDeclaration(context.typeDeclaration()) as TypeSyntax;
            var parameter = SyntaxFactory.Parameter(name.GetFirstToken())
                .WithType(returnTypeDec);
            if (context.GetText().StartsWith("out "))
            {
                parameter = parameter.AddModifiers(SyntaxFactory.Token(SyntaxKind.OutKeyword));
            }
            var value = context.fieldValue();
            if (value != null)
            {
                parameter = parameter.WithDefault(SyntaxFactory.EqualsValueClause(SyntaxFactory.ParseExpression(value.GetText())));
            }
            return parameter;
        }
    }
}