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

namespace Tars.Net.CLI {
using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="GrammarParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6-rc001")]
[System.CLSCompliant(false)]
public interface IGrammarListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="GrammarParser.tarsDefinitions"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTarsDefinitions([NotNull] GrammarParser.TarsDefinitionsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GrammarParser.tarsDefinitions"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTarsDefinitions([NotNull] GrammarParser.TarsDefinitionsContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GrammarParser.tarsDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTarsDefinition([NotNull] GrammarParser.TarsDefinitionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GrammarParser.tarsDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTarsDefinition([NotNull] GrammarParser.TarsDefinitionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GrammarParser.includeDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIncludeDefinition([NotNull] GrammarParser.IncludeDefinitionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GrammarParser.includeDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIncludeDefinition([NotNull] GrammarParser.IncludeDefinitionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GrammarParser.moduleDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterModuleDefinition([NotNull] GrammarParser.ModuleDefinitionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GrammarParser.moduleDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitModuleDefinition([NotNull] GrammarParser.ModuleDefinitionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GrammarParser.memberDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMemberDefinition([NotNull] GrammarParser.MemberDefinitionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GrammarParser.memberDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMemberDefinition([NotNull] GrammarParser.MemberDefinitionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GrammarParser.moduleName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterModuleName([NotNull] GrammarParser.ModuleNameContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GrammarParser.moduleName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitModuleName([NotNull] GrammarParser.ModuleNameContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GrammarParser.interfaceDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterInterfaceDefinition([NotNull] GrammarParser.InterfaceDefinitionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GrammarParser.interfaceDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitInterfaceDefinition([NotNull] GrammarParser.InterfaceDefinitionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GrammarParser.methodDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMethodDefinition([NotNull] GrammarParser.MethodDefinitionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GrammarParser.methodDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMethodDefinition([NotNull] GrammarParser.MethodDefinitionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GrammarParser.methodParameterDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMethodParameterDefinition([NotNull] GrammarParser.MethodParameterDefinitionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GrammarParser.methodParameterDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMethodParameterDefinition([NotNull] GrammarParser.MethodParameterDefinitionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GrammarParser.structDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStructDefinition([NotNull] GrammarParser.StructDefinitionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GrammarParser.structDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStructDefinition([NotNull] GrammarParser.StructDefinitionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GrammarParser.fieldDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFieldDefinition([NotNull] GrammarParser.FieldDefinitionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GrammarParser.fieldDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFieldDefinition([NotNull] GrammarParser.FieldDefinitionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GrammarParser.fieldOption"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFieldOption([NotNull] GrammarParser.FieldOptionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GrammarParser.fieldOption"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFieldOption([NotNull] GrammarParser.FieldOptionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GrammarParser.fieldValue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFieldValue([NotNull] GrammarParser.FieldValueContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GrammarParser.fieldValue"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFieldValue([NotNull] GrammarParser.FieldValueContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GrammarParser.typeDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterTypeDeclaration([NotNull] GrammarParser.TypeDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GrammarParser.typeDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitTypeDeclaration([NotNull] GrammarParser.TypeDeclarationContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GrammarParser.enumDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEnumDefinition([NotNull] GrammarParser.EnumDefinitionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GrammarParser.enumDefinition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEnumDefinition([NotNull] GrammarParser.EnumDefinitionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="GrammarParser.enumDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEnumDeclaration([NotNull] GrammarParser.EnumDeclarationContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="GrammarParser.enumDeclaration"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEnumDeclaration([NotNull] GrammarParser.EnumDeclarationContext context);
}
} // namespace Tars.Net.CLI
