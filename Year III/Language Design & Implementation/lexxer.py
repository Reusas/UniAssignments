from lexeme import *



# Function to create list of tokens from a line.
def lex(line, shouldPrint):
    currentCharacter = 0
    lastCharacter = len(line)
    # List to hold tokens
    lexemes = []
    # Go through every character in the line
    while currentCharacter < lastCharacter:
        # Get character from line
        character = line[currentCharacter]
        if character.isdigit():
            start = currentCharacter
            end = currentCharacter
            # If the character is a number then loop through the next characters if they are another number or '.' for floats
            while end < lastCharacter and (line[end].isdigit() or line[end] == "."):
                end = end + 1        

            # Get full number from start to end.    
            value = line[start:end]
            currentCharacter = end
            lexemes.append(Lexeme(value, Types.NUMBER, 0))
            # Update the character being checked
        elif character.isalpha():
            character = line[currentCharacter]
            start = currentCharacter
            end = currentCharacter

            while end < lastCharacter and (line[end].isalpha()):
                end = end + 1
            value = line[start:end]

            if value == "true" or value == "True":
                lexemes.append(Lexeme("TRUE",Types.TRUE,0))
            elif value == "false" or value == "False":
                lexemes.append(Lexeme("FALSE",Types.FALSE,0))
            elif value== "and":
                lexemes.append(Lexeme("AND",Types.AND,1))
            elif value=="or":
                lexemes.append(Lexeme("OR",Types.OR,1))
            elif value== "print":
                lexemes.append(Lexeme("PRINT",Types.PRINT,0))
            elif value=="if":
                lexemes.append(Lexeme("IF",Types.IF,0))
            elif value=="then":
                lexemes.append(Lexeme("THEN", Types.THEN, 0))
            elif value=="while":
                lexemes.append(Lexeme("WHILE",Types.WHILE,0))
            else:
                lexemes.append(Lexeme(value,Types.VARIABLE,0))
            currentCharacter = end


        elif character == '"':
            # Increment start and end by 1 to ignore the starting '"'
            start = currentCharacter +1
            end = currentCharacter + 1
            
            while end < lastCharacter and (line[end]!='"'):
                end = end +1
            
            value = line[start:end]
            # Increment end by 1 to move past the closing '"'
            currentCharacter = end +1
            lexemes.append(Lexeme(value,Types.STRING,0))



        if currentCharacter < lastCharacter:     
            character = line[currentCharacter]



        
        if character == "+":
            lexemes.append(Lexeme(character,Types.ADD, 1))
        elif character == "-":
            # If - is the first character of a equation or if it is after another operator then it is a unary negation
            if currentCharacter == 0 or lexemes[-1].value in "+-*/(":
                lexemes.append(Lexeme(character, Types.UNEG, 3))
            else:
                lexemes.append(Lexeme(character,Types.SUB, 1))
        elif character == "*":
            lexemes.append(Lexeme(character,Types.MUL, 2))
        elif character == "/":
            lexemes.append(Lexeme(character,Types.DIV, 2))
        elif character == "(":
            lexemes.append(Lexeme(character,Types.LPAREN, 0))
        elif character == ")":
            lexemes.append(Lexeme(character,Types.RPAREN, 0))
        elif character =="=":
            # If there is another = after this = then this is a comparison
            if line[currentCharacter+1] == "=":
                currentCharacter = currentCharacter + 1
                #Otherwise its an assignation
                lexemes.append(Lexeme("==",Types.COMPARE,1))
            else:
                lexemes.append(Lexeme("=",Types.ASSIGN,0))
        elif character == "!":
            if line[currentCharacter +1] =="=":
                lexemes.append(Lexeme("!=",Types.NOTEQUAL,0))
                currentCharacter = currentCharacter + 1
            else:
                lexemes.append(Lexeme("!",Types.UNEG2,5))
        elif character == "<":
            lexemes.append(Lexeme("<",Types.LESSTHEN,1))
        elif character == ">":
            lexemes.append(Lexeme(">",Types.GREATERTHEN,1))
        
        # Move on to the next charachter
        currentCharacter = currentCharacter + 1

    if shouldPrint:
        print(f"Tokens: {lexemes}")
    return lexemes