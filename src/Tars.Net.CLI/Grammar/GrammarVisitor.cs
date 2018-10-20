﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6-rc001
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from D:\code\Tars\CLI\src\Gen\Grammar.g4 by ANTLR 4.6.6-rc001

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Tars.Net.CLI.Grammar {
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="GrammarParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6-rc001")]
public interface IGrammarVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.tarsDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTarsDefinition([NotNull] GrammarParser.TarsDefinitionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.includeDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIncludeDefinition([NotNull] GrammarParser.IncludeDefinitionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.moduleDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitModuleDefinition([NotNull] GrammarParser.ModuleDefinitionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.memberDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMemberDefinition([NotNull] GrammarParser.MemberDefinitionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.moduleName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitModuleName([NotNull] GrammarParser.ModuleNameContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.interfaceDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitInterfaceDefinition([NotNull] GrammarParser.InterfaceDefinitionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.methodDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMethodDefinition([NotNull] GrammarParser.MethodDefinitionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.methodParameterDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMethodParameterDefinition([NotNull] GrammarParser.MethodParameterDefinitionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.structDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStructDefinition([NotNull] GrammarParser.StructDefinitionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.fieldDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFieldDefinition([NotNull] GrammarParser.FieldDefinitionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.fieldOrder"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFieldOrder([NotNull] GrammarParser.FieldOrderContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.fieldOption"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFieldOption([NotNull] GrammarParser.FieldOptionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.fieldValue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFieldValue([NotNull] GrammarParser.FieldValueContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.typeDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeDeclaration([NotNull] GrammarParser.TypeDeclarationContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.enumDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEnumDefinition([NotNull] GrammarParser.EnumDefinitionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.enumDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEnumDeclaration([NotNull] GrammarParser.EnumDeclarationContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="GrammarParser.name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitName([NotNull] GrammarParser.NameContext context);
}
} // namespace Tars.Net.CLI.Grammar
