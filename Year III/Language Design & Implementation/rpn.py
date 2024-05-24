from lexeme import *
from variable import *


globalVariables = {}
ifContinue = False

# Converts tokens to reverse polish notation using shunting yard algorithm
def convertToRpn(tokens,shouldPrint):
    # output quoue and stack for operands
    output = []
    stack = []

    for token in tokens:
        # If token is a number it gets appended to the output list
        if token.type == Types.NUMBER or token.type == Types.TRUE or token.type == Types.FALSE or token.type == Types.STRING:
            output.append(token)
        elif token.type == Types.VARIABLE:
            output.append(token)
        elif token.type == Types.UNEG:
            # Unary negation should be treated separately
            stack.append(token)
        elif token.type in (Types.ADD, Types.SUB, Types.MUL, Types.DIV, Types.COMPARE, Types.NOTEQUAL, Types.GREATERTHEN, Types.LESSTHEN, Types.UNEG2, Types.AND, Types.OR,Types.ASSIGN, Types.PRINT,Types.IF,Types.THEN,Types.WHILE):
            while stack and stack[-1].precedence >=token.precedence:
                output.append(stack.pop())
            stack.append(token)
        elif token.type == Types.LPAREN:
            stack.append(token)
            # If the token is ) then loop through until the ( and append from stack to output
        elif token.type == Types.RPAREN:
            while stack and stack[-1].type != Types.LPAREN:
                output.append(stack.pop())
            stack.pop()

    while stack:
        output.append(stack.pop())
        
    if shouldPrint:
        print(f"RPN: {output}")
    return output

# Stack based calculator
def calculateRpn(rpn):
    stack = []
    index = 0
    ifContinue = True  # Continue assuming theres no if then statement in this expression
    for token in rpn:
        if token.type == Types.NUMBER:
            stack.append(float(token.value))
        elif token.type == Types.STRING:
            stack.append(token.value)
        elif token.type == Types.VARIABLE:
            # Check if variable name already exists
            if token.value in globalVariables:
                # If variable is first token and there is an assignation operation then the variable is being created/assigned to
                if index == 0 and Lexeme("=",Types.ASSIGN,0) in rpn or rpn[1].type == 20:
                    stack.append(token.value)
                else:
                    # Otherwise append its value
                    stack.append(globalVariables[token.value])
            else:
                # If the variable doesnt exist then create it in the global variable dictionary with the value of 0 for now
                globalVariables[token.value] = 0
                #print(globalVariables)
                stack.append(token.value)


        elif token.type == Types.TRUE:
            stack.append(1)   # 1 for true 0 for false.
        elif token.type == Types.FALSE:
            stack.append(0)
        elif token.type == Types.GREATERTHEN:
            operand2 = stack.pop()
            operand1 = stack.pop()

            if type(operand1) == float and type(operand2) == float:
                result = operand1 > operand2
                stack.append(result)
            else:
                result = "Error: invalid types being compared"
                stack.append(result)
        elif token.type == Types.LESSTHEN:
            operand2 = stack.pop()
            operand1 = stack.pop()
            if type(operand1) == float and type(operand2) == float:
                result = operand1 < operand2
                stack.append(result)
            else:
                result = "Error: invalid types being compared"
                stack.append(result)
            # Unary negation for booleans
        elif token.type == Types.UNEG2:
            operand = stack.pop()
            result = 0;
            if operand == 0:
                result = True
            elif operand == 1:
                result = False
            stack.append(result)
        elif token.type == Types.AND:
            operand2 = stack.pop()
            operand1 = stack.pop()



            if type(operand1) != str and type(operand2) != str:

                result = operand1 & operand2
                if result == 0:
                    stack.append(False)
                elif result == 1:
                    stack.append(True)
            else:
                result = "Error: Invalid types"
                stack.append(result)
        elif token.type == Types.OR:
            operand2 = stack.pop()
            operand1 = stack.pop()
            if type(operand1) != str and type(operand2) != str:
                result = operand1 | operand2
                if result == 0:
                    stack.append(False)
                elif result == 1:
                    stack.append(True)
            else:
                result = "Error: Invalid types"
                stack.append(result)

        elif token.type == Types.IF:
            operand1 = stack.pop()

            if operand1 == False:
                ifContinue = False
            elif operand1 == True:
                ifContinue = True

        elif token.type == Types.THEN:
            if ifContinue == True:
                pass
            elif ifContinue == False:
                break

        elif token.type == Types.WHILE:
            operand1 = stack.pop()
            if operand1 == 1 or operand1 == True:
                print("ShouldLoop")


            
        # Unary negation for numbers
        elif token.type == Types.UNEG:
            operand = stack.pop()
            # Only perform unary negation if value is number
            if type(operand) == int or type(operand) == float:
                result = -operand  
                stack.append(result)
            else:
                result = "Error: Cannot unary negate non numerical value"
                stack.append(result)
        elif token.type == Types.ADD:
            operand2 = stack.pop()
            operand1 = stack.pop()

            if type(operand1) == str and type(operand2) == str or type(operand1) == float and type(operand2) == float:
                result = operand1 + operand2
                stack.append(result)
            else:
                result = "Error: Cannot add different types together"
                stack.append(result)
        elif token.type == Types.SUB:
                operand2 = stack.pop()
                operand1 = stack.pop()
                if type(operand1) == str and type(operand2) == str or type(operand1) == float and type(operand2) == float:
                    result = operand1 - operand2
                    stack.append(result)
                else:
                    result = "Error: Cannot subtract different types together"
                    stack.append(result)
        elif token.type == Types.MUL:
            operand2 = stack.pop()
            operand1 = stack.pop()
            if type(operand1) == float and type(operand2) == float:
                result = operand1 * operand2
                stack.append(result)
            else:
                result = "Error: can only perform multiplication on numbers"
                stack.append(result)
        elif token.type == Types.DIV:
            operand2 = stack.pop()
            operand1 = stack.pop()
            if type(operand1) == float and type(operand2) == float:
                result = operand1 / operand2
                stack.append(result)
            else:
                result = "Error: can only perform division on numbers"
                stack.append(result)
        elif token.type == Types.COMPARE:
            operand2 = stack.pop()
            operand1 = stack.pop()
            #print(f"Op1{operand1} op2: {operand2}")
            result = operand1 == operand2
            stack.append(result)
        elif token.type == Types.NOTEQUAL:
            operand2 = stack.pop()
            operand1 = stack.pop()
            result = operand1 != operand2
            stack.append(result)
        elif token.type == Types.ASSIGN:
            operand2 = stack.pop()
            operand1 = stack.pop()
            # If the variable exists then update its value with the calculated value from the right hand side
            if operand1 in globalVariables:
               # print("Exists")
                globalVariables[operand1] = operand2
                #print("Update variable")
                #print(globalVariables)
        elif token.type == Types.PRINT:
            operand = stack.pop()
            print(globalVariables[operand])
            print("\n")
        index = index + 1
    if ifContinue == True:
        return stack[0] if stack else None
    elif ifContinue == False:
        return 
  

