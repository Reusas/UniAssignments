from lexxer import *
from rpn import *

# Shouldprint will print out the lexed tokens and the RPN if set to true
def readFile(fileName, shouldPrint):
    file = open(fileName, "r")
    lines = file.readlines()


    for line in lines:
        # ignore empty lines
        if line.strip() == "":
            pass
        else:
           result = calculateRpn(convertToRpn(lex(line,shouldPrint), shouldPrint))
           if result != None:
               print(result)
               print("\n")

              


    

# Code to allow user to enter name of file to run.
if __name__ == '__main__':

    while True:
        userInput = input("[Running]")
        if "run" in userInput:
            splitInput = userInput.split(" ")
            if ".wrm" in splitInput[-1]:
                readFile(splitInput[-1],False)
        elif "test" in userInput:
            splitInput = userInput.split(" ")
            if ".wrm" in splitInput[-1]:
                readFile(splitInput[-1],True)            

    