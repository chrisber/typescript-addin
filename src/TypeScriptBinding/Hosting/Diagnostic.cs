// 
// Diagnostic.cs
// 
// Author:
//   Matt Ward <ward.matt@gmail.com>
// 
// Copyright (C) 2014 Matthew Ward
// 
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ICSharpCode.TypeScriptBinding.Hosting
{

    public enum DiagnosticCategory {
        Warning,
        Error,
        Message,
    }


/// <summary>
/// TODO Find out if implementing this classes has benifits if not remove them
/// </summary>
 
//
//    public enum SyntaxKind {
//        Unknown,
//        EndOfFileToken,
//        SingleLineCommentTrivia,
//        MultiLineCommentTrivia,
//        NewLineTrivia,
//        WhitespaceTrivia,
//        // Literals
//        NumericLiteral,
//        StringLiteral,
//        RegularExpressionLiteral,
//        // Punctuation
//        OpenBraceToken,
//        CloseBraceToken,
//        OpenParenToken,
//        CloseParenToken,
//        OpenBracketToken,
//        CloseBracketToken,
//        DotToken,
//        DotDotDotToken,
//        SemicolonToken,
//        CommaToken,
//        LessThanToken,
//        GreaterThanToken,
//        LessThanEqualsToken,
//        GreaterThanEqualsToken,
//        EqualsEqualsToken,
//        ExclamationEqualsToken,
//        EqualsEqualsEqualsToken,
//        ExclamationEqualsEqualsToken,
//        EqualsGreaterThanToken,
//        PlusToken,
//        MinusToken,
//        AsteriskToken,
//        SlashToken,
//        PercentToken,
//        PlusPlusToken,
//        MinusMinusToken,
//        LessThanLessThanToken,
//        GreaterThanGreaterThanToken,
//        GreaterThanGreaterThanGreaterThanToken,
//        AmpersandToken,
//        BarToken,
//        CaretToken,
//        ExclamationToken,
//        TildeToken,
//        AmpersandAmpersandToken,
//        BarBarToken,
//        QuestionToken,
//        ColonToken,
//        // Assignments
//        EqualsToken,
//        PlusEqualsToken,
//        MinusEqualsToken,
//        AsteriskEqualsToken,
//        SlashEqualsToken,
//        PercentEqualsToken,
//        LessThanLessThanEqualsToken,
//        GreaterThanGreaterThanEqualsToken,
//        GreaterThanGreaterThanGreaterThanEqualsToken,
//        AmpersandEqualsToken,
//        BarEqualsToken,
//        CaretEqualsToken,
//        // Identifiers
//        Identifier,
//        // Reserved words
//        BreakKeyword,
//        CaseKeyword,
//        CatchKeyword,
//        ClassKeyword,
//        ConstKeyword,
//        ContinueKeyword,
//        DebuggerKeyword,
//        DefaultKeyword,
//        DeleteKeyword,
//        DoKeyword,
//        ElseKeyword,
//        EnumKeyword,
//        ExportKeyword,
//        ExtendsKeyword,
//        FalseKeyword,
//        FinallyKeyword,
//        ForKeyword,
//        FunctionKeyword,
//        IfKeyword,
//        ImportKeyword,
//        InKeyword,
//        InstanceOfKeyword,
//        NewKeyword,
//        NullKeyword,
//        ReturnKeyword,
//        SuperKeyword,
//        SwitchKeyword,
//        ThisKeyword,
//        ThrowKeyword,
//        TrueKeyword,
//        TryKeyword,
//        TypeOfKeyword,
//        VarKeyword,
//        VoidKeyword,
//        WhileKeyword,
//        WithKeyword,
//        // Strict mode reserved words
//        ImplementsKeyword,
//        InterfaceKeyword,
//        LetKeyword,
//        PackageKeyword,
//        PrivateKeyword,
//        ProtectedKeyword,
//        PublicKeyword,
//        StaticKeyword,
//        YieldKeyword,
//        // TypeScript keywords
//        AnyKeyword,
//        BooleanKeyword,
//        ConstructorKeyword,
//        DeclareKeyword,
//        GetKeyword,
//        ModuleKeyword,
//        RequireKeyword,
//        NumberKeyword,
//        SetKeyword,
//        StringKeyword,
//        TypeKeyword,
//        // Parse tree nodes
//        Missing,
//        // Names
//        QualifiedName,
//        // Signature elements
//        TypeParameter,
//        Parameter,
//        // TypeMember
//        Property,
//        Method,
//        Constructor,
//        GetAccessor,
//        SetAccessor,
//        CallSignature,
//        ConstructSignature,
//        IndexSignature,
//        // Type
//        TypeReference,
//        TypeQuery,
//        TypeLiteral,
//        ArrayType,
//        TupleType,
//        UnionType,
//        ParenType,
//        // Expression
//        ArrayLiteral,
//        ObjectLiteral,
//        PropertyAssignment,
//        PropertyAccess,
//        IndexedAccess,
//        CallExpression,
//        NewExpression,
//        TypeAssertion,
//        ParenExpression,
//        FunctionExpression,
//        ArrowFunction,
//        PrefixOperator,
//        PostfixOperator,
//        BinaryExpression,
//        ConditionalExpression,
//        OmittedExpression,
//        // Element
//        Block,
//        VariableStatement,
//        EmptyStatement,
//        ExpressionStatement,
//        IfStatement,
//        DoStatement,
//        WhileStatement,
//        ForStatement,
//        ForInStatement,
//        ContinueStatement,
//        BreakStatement,
//        ReturnStatement,
//        WithStatement,
//        SwitchStatement,
//        CaseClause,
//        DefaultClause,
//        LabeledStatement,
//        ThrowStatement,
//        TryStatement,
//        TryBlock,
//        CatchBlock,
//        FinallyBlock,
//        DebuggerStatement,
//        VariableDeclaration,
//        FunctionDeclaration,
//        FunctionBlock,
//        ClassDeclaration,
//        InterfaceDeclaration,
//        TypeAliasDeclaration,
//        EnumDeclaration,
//        ModuleDeclaration,
//        ModuleBlock,
//        ImportDeclaration,
//        ExportAssignment,
//        // Enum
//        EnumMember,
//        // Top-level nodes
//        SourceFile,
//        Program,
//        // Synthesized list
//        SyntaxList,
//        // Enum value count
//        Count,
//        // Markers
//        FirstAssignment = SyntaxKind.EqualsToken,
//        LastAssignment = CaretEqualsToken,
//        FirstReservedWord = BreakKeyword,
//        LastReservedWord = WithKeyword,
//        FirstKeyword = BreakKeyword,
//        LastKeyword = TypeKeyword,
//        FirstFutureReservedWord = ImplementsKeyword,
//        LastFutureReservedWord = YieldKeyword,
//        FirstTypeNode = TypeReference,
//        LastTypeNode = ParenType,
//        FirstPunctuation = OpenBraceToken,
//        LastPunctuation = CaretEqualsToken,
//        FirstToken = EndOfFileToken,
//        LastToken = TypeKeyword,
//        FirstTriviaToken = SingleLineCommentTrivia,
//        LastTriviaToken = WhitespaceTrivia
//    }
//
//    public enum SymbolFlags {
//        FunctionScopedVariable = 0x00000001,  // Variable (var) or parameter
//        BlockScopedVariable    = 0x00000002,  // A block-scoped variable (let or const)
//        Property               = 0x00000004,  // Property or enum member
//        EnumMember             = 0x00000008,  // Enum member
//        Function               = 0x00000010,  // Function
//        Class                  = 0x00000020,  // Class
//        Interface              = 0x00000040,  // Interface
//        Enum                   = 0x00000080,  // Enum
//        ValueModule            = 0x00000100,  // Instantiated module
//        NamespaceModule        = 0x00000200,  // Uninstantiated module
//        TypeLiteral            = 0x00000400,  // Type Literal
//        ObjectLiteral          = 0x00000800,  // Object Literal
//        Method                 = 0x00001000,  // Method
//        Constructor            = 0x00002000,  // Constructor
//        GetAccessor            = 0x00004000,  // Get accessor
//        SetAccessor            = 0x00008000,  // Set accessor
//        CallSignature          = 0x00010000,  // Call signature
//        ConstructSignature     = 0x00020000,  // Construct signature
//        IndexSignature         = 0x00040000,  // Index signature
//        TypeParameter          = 0x00080000,  // Type parameter
//        TypeAlias              = 0x00100000,  // Type alias
//
//        // Export markers (see comment in declareModuleMember in binder)
//        ExportValue            = 0x00200000,  // Exported value marker
//        ExportType             = 0x00400000,  // Exported type marker
//        ExportNamespace        = 0x00800000,  // Exported namespace marker
//
//        Import                 = 0x01000000,  // Import
//        Instantiated           = 0x02000000,  // Instantiated symbol
//        Merged                 = 0x04000000,  // Merged symbol (created during program binding)
//        Transient              = 0x08000000,  // Transient symbol (created during type check)
//        Prototype              = 0x10000000,  // Prototype property (no source representation)
//        UnionProperty          = 0x20000000,  // Property in union type
//
//        Variable  = FunctionScopedVariable | BlockScopedVariable,
//        Value     = Variable | Property | EnumMember | Function | Class | Enum | ValueModule | Method | GetAccessor | SetAccessor,
//        Type      = Class | Interface | Enum | TypeLiteral | ObjectLiteral | TypeParameter | TypeAlias,
//        Namespace = ValueModule | NamespaceModule,
//        Module    = ValueModule | NamespaceModule,
//        Accessor  = GetAccessor | SetAccessor,
//        Signature = CallSignature | ConstructSignature | IndexSignature,
//
//        // Variables can be redeclared, but can not redeclare a block-scoped declaration with the 
//        // same name, or any other value that is not a variable, e.g. ValueModule or Class
//        FunctionScopedVariableExcludes = Value & ~FunctionScopedVariable,   
//
//        // Block-scoped declarations are not allowed to be re-declared
//        // they can not merge with anything in the value space
//        BlockScopedVariableExcludes = Value,
//
//        ParameterExcludes       = Value,
//        PropertyExcludes        = Value,
//        EnumMemberExcludes      = Value,
//        FunctionExcludes        = Value & ~(Function | ValueModule),
//        ClassExcludes           = (Value | Type) & ~ValueModule,
//        InterfaceExcludes       = Type & ~Interface,
//        EnumExcludes            = (Value | Type) & ~(Enum | ValueModule),
//        ValueModuleExcludes     = Value & ~(Function | Class | Enum | ValueModule),
//        NamespaceModuleExcludes = 0,
//        MethodExcludes          = Value & ~Method,
//        GetAccessorExcludes     = Value & ~SetAccessor,
//        SetAccessorExcludes     = Value & ~GetAccessor,
//        TypeParameterExcludes   = Type & ~TypeParameter,
//        TypeAliasExcludes       = Type,
//        ImportExcludes          = Import,  // Imports collide with all other imports with the same name
//
//        ModuleMember = Variable | Function | Class | Interface | Enum | Module | TypeAlias | Import,
//
//        ExportHasLocal = Function | Class | Enum | ValueModule,
//
//        HasLocals  = Function | Module | Method | Constructor | Accessor | Signature,
//        HasExports = Class | Enum | Module,
//        HasMembers = Class | Interface | TypeLiteral | ObjectLiteral,
//
//        IsContainer        = HasLocals | HasExports | HasMembers,
//        PropertyOrAccessor = Property | Accessor,
//        Export             = ExportNamespace | ExportType | ExportValue,
//    }
//
//    public enum NodeFlags {
//        Export           = 0x00000001,  // Declarations
//        Ambient          = 0x00000002,  // Declarations
//        QuestionMark     = 0x00000004,  // Parameter/Property/Method
//        Rest             = 0x00000008,  // Parameter
//        Public           = 0x00000010,  // Property/Method
//        Private          = 0x00000020,  // Property/Method
//        Protected        = 0x00000040,  // Property/Method
//        Static           = 0x00000080,  // Property/Method
//        MultiLine        = 0x00000100,  // Multi-line array or object literal
//        Synthetic        = 0x00000200,  // Synthetic node (for full fidelity)
//        DeclarationFile  = 0x00000400,  // Node is a .d.ts file
//        Let              = 0x00000800,  // Variable declaration
//        Const            = 0x00001000,  // Variable declaration
//
//        Modifier = Export | Ambient | Public | Private | Protected | Static,
//        AccessibilityModifier = Public | Private | Protected,
//        BlockScoped = Let | Const
//    }
//
//    public interface  TextRange {
//        int pos { get; set; }
//        int end { get; set; }
//    }
//
//
//    public class SymbolTable {
//        List<KeyValuePair<string,Symbol>> index { get; set; }
//    }
//
//    public interface Identifier : Node {
//        string text { get; set; }                // Text of identifier (with escapes converted to characters)
//    }
//
//    public interface Declaration : Node {
//        Identifier name { get; set; }
//    }
//
//    public class  Symbol {
//        SymbolFlags  flag{ get; set; }           // Symbol flags
//        string  name{ get; set; }                  // Name of symbol
//        int id { get; set; }                 // Unique id (used to look up SymbolLinks)
//        int mergeId{ get; set; }             // Merge id (used to look up merged symbol)
//        Declaration[] declarations{ get; set; }  // Declarations associated with this symbol
//        Symbol parent{ get; set; }               // Parent symbol
//        SymbolTable members{ get; set; }      // Class, interface or literal instance members
//        SymbolTable exports{ get; set; }       // Module exports
//        Symbol exportSymbol{ get; set; }        // Exported symbol associated with this symbol
//        Declaration valueDeclaration{ get; set; } // First value declaration of the symbol
//    }
//
//    public interface Node : TextRange {
//        SyntaxKind kind{ get; set; }
//        NodeFlags flags{ get; set; }
//        int id{ get; set; }                  // Unique id (used to look up NodeLinks)
//        Node parent{ get; set; }                // Parent node (initialized by binding)
//        Symbol symbol{ get; set; }              // Symbol declared by node (initialized by binding)
//        SymbolTable locals{ get; set; }         // Locals associated with node (initialized by binding)
//        Node nextContainer{ get; set; }         // Next container in declaration order (initialized by binding)
//        Symbol localSymbol{ get; set; }         // Local symbol declared by node (initialized by binding only for exported nodes)
//        int pos { get; set; }
//        int end { get; set; }
//    }
//
//   public class NodeImpl : Node
//    {
//        public SyntaxKind kind{ get; set; }
//        public NodeFlags flags{ get; set; }
//        public int id{ get; set; }                  // Unique id (used to look up NodeLinks)
//        public Node parent{ get; set; }                // Parent node (initialized by binding)
//        public Symbol symbol{ get; set; }              // Symbol declared by node (initialized by binding)
//        public SymbolTable locals{ get; set; }         // Locals associated with node (initialized by binding)
//        public Node nextContainer{ get; set; }         // Next container in declaration order (initialized by binding)
//        public Symbol localSymbol{ get; set; } 
//
//        public int pos { get; set; }
//        public int end { get; set; } 
//    }
//
//    public class  Statement : Node {
//
//        public SyntaxKind kind{ get; set; }
//        public NodeFlags flags{ get; set; }
//        public int id{ get; set; }                  // Unique id (used to look up NodeLinks)
//        public Node parent{ get; set; }                // Parent node (initialized by binding)
//        public Symbol symbol{ get; set; }              // Symbol declared by node (initialized by binding)
//        public SymbolTable locals{ get; set; }         // Locals associated with node (initialized by binding)
//        public Node nextContainer{ get; set; }         // Next container in declaration order (initialized by binding)
//        public Symbol localSymbol{ get; set; } 
//
//        public int pos { get; set; }
//        public int end { get; set; } 
//    }
//
//    public class  Block  : Statement {
//        NodeArray<Statement> statements { get; set; }
//    }
//
//    public class  NodeArray<T> : IEnumerable<T>, IEnumerable, TextRange {
//        List<T> list = new List<T> ();
//
//        public Boolean hasTrailingComma  { get; set; }
//
//        public int pos { get; set; }
//        public int end { get; set; } 
//
//        public T this[int index]  
//        {  
//            get { return list[index]; }  
//            set { list.Insert(index, value); }  
//        } 
//
//        public IEnumerator<T> GetEnumerator()
//        {
//            return list.GetEnumerator();
//        }
//
//        System.Collections.IEnumerator IEnumerable.GetEnumerator()
//        {
//            // uses the strongly typed IEnumerable<T> implementation
//            return this.GetEnumerator();
//        }
//
//   }
//
//    public  class FileReference : TextRange {
//        #region Block implementation
//        public int pos { get; set; }
//        public int end { get; set; } 
//        #endregion
//
//        string filename { get; set; }
//    }
//
//    public interface ModuleElement : Node {
//        object _moduleElementBrand { get; set; }
//    }
//    public class ModuleElementImpl :Node
//    {
//        public SyntaxKind kind{ get; set; }
//        public NodeFlags flags{ get; set; }
//        public int id{ get; set; }                  // Unique id (used to look up NodeLinks)
//        public Node parent{ get; set; }                // Parent node (initialized by binding)
//        public Symbol symbol{ get; set; }              // Symbol declared by node (initialized by binding)
//        public SymbolTable locals{ get; set; }         // Locals associated with node (initialized by binding)
//        public Node nextContainer{ get; set; }         // Next container in declaration order (initialized by binding)
//        public Symbol localSymbol{ get; set; } 
//
//        public int pos { get; set; }
//        public int end { get; set; } 
//
//        public object _moduleElementBrand { get; set; }
//
//    }
//
//    public class SourceFile : Block {
//
//
//        #region Block implementation
//
//        public  NodeArray<ModuleElement> statements { get; set; }
//
//        #endregion
//
//        public NodeImpl endOfFileToken { get; set; }
//
//
//        public  string filename { get; set; }
//        public  string text { get; set; }
//
////        void  getLineAndCharacterFromPosition(int position);
////        int getPositionFromLineAndCharacter(int line, int character);
////        int getLineStarts();
//
//
//        public  List<string> amdDependencies { get; set; }
//        public  string amdModuleName { get; set; }
//        public  List<FileReference> referencedFiles { get; set; }
//
//        public List<Diagnostic> referenceDiagnostics { get; set; }
//
//        public List<Diagnostic> parseDiagnostics { get; set; }
//
//        public  List<Diagnostic> semanticErrors { get; set; }
//
//        public Boolean hasNoDefaultLib { get; set; }
//        public Node externalModuleIndicator { get; set; } // The first node that causes this file to be an external module
//        public int nodeCount { get; set; }
//        public  int identifierCount { get; set; }
//        public  int  symbolCount { get; set; }
//
//        public  ScriptTarget languageVersion { get; set; }
//        public  Dictionary<string,string> identifiers { get; set; }
//
//        #region Node implementation
//
//        public SyntaxKind kind{ get; set; }
//        public NodeFlags flags{ get; set; }
//        public int id{ get; set; }                  // Unique id (used to look up NodeLinks)
//        public Node parent{ get; set; }                // Parent node (initialized by binding)
//        public Symbol symbol{ get; set; }              // Symbol declared by node (initialized by binding)
//        public SymbolTable locals{ get; set; }         // Locals associated with node (initialized by binding)
//        public Node nextContainer{ get; set; }         // Next container in declaration order (initialized by binding)
//        public Symbol localSymbol{ get; set; } 
//
//        #endregion
//
//        #region TextRange implementation
//
//        public int pos { get; set; }
//        public int end { get; set; } 
//
//        #endregion
//    }

    public class Diagnostic  
	{
        public object  file { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public string  messageText{ get; set; }
        public  DiagnosticCategory category{ get; set; }
        public  int code { get; set; }
        public  Boolean isEarly{ get; set; }
       
		
		public string GetDiagnosticMessage()
		{
            if (file == null){
            return String.Format("{0},{1}",messageText,code);
            }
            return String.Format("{0},{1},",messageText,code);
		}
		
		public override string ToString()
		{
			return GetDiagnosticMessage();
		}
	}
}
