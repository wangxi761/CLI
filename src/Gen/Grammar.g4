grammar Grammar;

tarsDefinitions: tarsDefinition*;

tarsDefinition
    : includeDefinition
    | moduleDefinition
    ;

includeDefinition: '#include' String;

moduleDefinition
    : 'module' moduleName '{' memberDefinition* '}' ';'*
    ;

memberDefinition
    : enumDefinition
    | structDefinition
    | interfaceDefinition
    ;

moduleName
    : ID
    | moduleName '.' ID
    ;

interfaceDefinition
    : 'interface' ID '{' methodDefinition*  '}' ';'*
    ;

methodDefinition
    : typeDeclaration ID '(' methodParameterDefinition*  ')' ';'
    ;

methodParameterDefinition
    : typeDeclaration ID ','*
    | 'out' typeDeclaration ID ','*
    ;

structDefinition
    : 'struct' ID '{' fieldDefinition*  '}' ';'*
    ;

fieldDefinition
    : Int fieldOption typeDeclaration ID fieldValue* ';'
    ;

fieldOption: 'require' | 'optional';

fieldValue
    : '=' Int
    | '=' Float
    | '=' String
    ;

typeDeclaration
    : ID
    | 'short'
    | 'byte'
    | 'int'
    | 'string'
    | 'vector' '<'typeDeclaration '>'
    | 'map' '<'typeDeclaration ',' typeDeclaration '>'
    | 'void'
    ;

enumDefinition: 'enum' ID '{' enumDeclaration*  '}' ';'*;

enumDeclaration: ID fieldValue* ','*;

//------ Identifiers
ID : ID_Letter (ID_Letter | Digit)* ;
fragment ID_Letter : 'a'..'z' | 'A'..'Z' | '_' ;
fragment Digit : '0'..'9';
fragment Number : Digit | '-' Digit;

//------ Numbers
Int   : Number+ ;
Float : Number+ '.' Digit* ;

//------ Strings
String : '"' (ESC | .)*? '"' ;
fragment ESC : '\\' [btnr"\\] ;  // \b, \t, \n, ...
LineComment: '//' .*? '\n' -> skip;
BlockComment : '/*' .*? '*/' -> skip;
WS: [ \t\n\r]+ -> skip;