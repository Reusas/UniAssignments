[How to run the program]
To use this program ensure that all of the source files are in one folder including the text files of the code that the 
program will run.
Once this is done the main.py file can be opened. It should open a window that reads "[Running]".
In order to run a source file type in "run fileName.wrm" or "test fileName.wrm".
The "run" command will only run the results of the code while "test" will also print out lexed tokens and rpn for every 
line of a source file.
There can be as many spaces as you want inbetween the "run" or "test" and "fileName.wrm" however the commands needs to be typed in lowercase
and the file name of the source file should not contain any spaces and end with the .wrm extension. 'run' and 'test' should also
not be included in the same line.
An example to run the included source files would be:

run Stage1.wrm
test    Stage5.wrm

[How to write source files]
Source files should be in .wrm extension. The language supports arithmetic operations, boolean comparisons, text values, variables and
if statments.
Each of the stage .wrm files show how to use these functions. The program ignores spaces so you can write expresions how you wish. Both of
these are valid:
5 + 5
5     +   5

'If' statements should be writen in one line along side 'then' with the expression to execute after.