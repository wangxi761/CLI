grammar Grammar;

tarsDefinition: includeDefinition* moduleDefinition*;

includeDefinition: '#include' String;

moduleDefinition
    : 'module' moduleName '{' memberDefinition* '}' ';'?
    ;

memberDefinition
    : enumDefinition
    | structDefinition
    | interfaceDefinition
    | constDefinition
    | keyDefinition
    ;

constDefinition
    : 'const' typeDeclaration name '='? fieldValue? ';'
    ;
keyDefinition
    : 'key' '[' name (',' name)* ']' ';'
    | 'key' '['  ']' ';'
    ;

moduleName
    : ID
    | moduleName '.' ID
    ;

interfaceDefinition
    : 'interface' name '{' methodDefinition*  '}' ';'?
    ;

methodDefinition
    : typeDeclaration name '(' methodParameterDefinition*  ')' ';'
    ;

methodParameterDefinition
    : 'out'? typeDeclaration name '='? fieldValue? ','?
    ;

structDefinition
    : 'struct' name '{' fieldDefinition*  '}' ';'?
    ;

fieldDefinition
    : fieldOrder fieldOption typeDeclaration name '='? fieldValue? ';'
    ;

fieldOrder: Int;

fieldOption: 'require' | 'optional';

fieldValue
    : Int
    | Float
    | String
    ;

typeDeclaration
    : ID
    | ID '.' ID
    | 'vector' '<'typeDeclaration '>'
    | 'map' '<'typeDeclaration ',' typeDeclaration '>'
    ;

enumDefinition: 'enum' name '{' enumDeclaration*  '}' ';'?;

enumDeclaration: name '='? fieldValue? ','?;

name: ID;

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