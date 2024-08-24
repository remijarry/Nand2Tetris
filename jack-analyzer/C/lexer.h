#ifndef LEXER_H // prevent multiple inclusions
#define LEXER_H

typedef enum {
  TOKEN_KEYWORD,
  TOKEN_SYMBOL,
  TOKEN_IDENTIFIER,
  TOKEN_INTEGER_CONSTANT,
  TOKEN_STRING_CONSTANT
} TokenType;

typedef struct {
  TokenType type;
  char* value;
} Token;

#endif // LEXER_H