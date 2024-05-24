from dataclasses import dataclass

# Class containing different types of characters
class Types:
    NUMBER = 0
    ADD = 1
    SUB = 2
    MUL = 3
    DIV = 4
    LPAREN = 5
    RPAREN = 6
    UNEG = 7 # This is for - symbol
    TRUE = 8
    FALSE = 9
    COMPARE = 10
    NOTEQUAL = 11
    GREATERTHEN = 12
    LESSTHEN = 13
    UNEG2 = 14 # This will be for the ! symbol
    AND = 15
    OR = 16
    STRING = 17
    ASSIGN = 18
    VARIABLE = 19
    PRINT = 20
    IF = 21
    THEN = 22
    WHILE = 23


# Class to hold Lexeme type. It has a value and a type associatied with it.
@dataclass
class Lexeme:
    value: str
    type: Types
    precedence: int

    # This will make it so then when this class is printed it will only display the value.
    def __repr__(self):
        return self.value