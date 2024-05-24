# When the PC starts, the processor is essentially emulating an 8086 processor, i.e. 
# a 16-bit processor.  So our initial boot loader code is 16-bit code that will 
# eventually switch the processor into 32-bit mode.

# This code is linked to assume a starting address of 0x7C00 which is where the BIOS
# will load a boot segment.

.code16                         # Assemble for 16-bit mode
.globl start
start:
    jmp     real_start

# Write to the console using BIOS.
# 
# Input: SI contains the address of the null-terminated string to be displayed

cons_write:
    movb    $0x0e, %ah           # 0x0e is the INT 10h BIOS call to output the value contained in AL to screen

cons_write_rpt:
    movb    (%si), %al          # Load the byte at the location contained in the SI register into AL   
    inc     %si                 # Add 1 to the value in SI
    cmp     $0, %al             # Compare the value in AL with 0
    jz      cons_write_done     # If it is zero, then we are done
    int     $0x10               # Output the character in AL to the screen
    jmp     cons_write_rpt      # and continue

cons_write_done:                # Something that is called will never return 
    ret                         # until a 'ret' instruction is encountered.  Labels do 
                                # not give a program any structure.  They just give a 
                                # memory location a name that we can use in our code. 

cons_write_crlf:
    movb    $0x0e, %ah          # Output CR
    movb    $0x0d, %al
    int     $0x10
    movb    $0x0a, %al          # Output LF
    int     $0x10
    ret

cons_writeline:
    call    cons_write
    call    cons_write_crlf
    ret    

real_start:
    movb    %dl, (boot_device) # Boot device number is passed in DL from BIOS. Save it hear since DL might get trashed

    movw    $boot_message, %si  # Display our boot message
    call    cons_writeline

    movw    $booted_from_msg, %si
    call    cons_write

    movb    (boot_device), %dl
    cmp     $0x80, %dl
    jge     harddrive

    movw    $fd, %si
    jmp     finish

harddrive:
    movw    $hd, %si


finish:
    call    cons_writeline

    movw $enterString2, %si
    call cons_write
    
    movw $sectorEnteredBuffer , %si          
    mov $0, %cx
    jmp GetInput

SectorsToRead:
    movw $askSectorsToRead, %si                 
    call cons_write
    movw $sectorsToReadBuffer, %si
    
GetInput:
    mov $0x00, %ah              # Set up for int 0x16
    int $0x16
    cmp $-1, %al                 # If Ascii value is less then -1 then stay in the loop.
    jle GetInput                
    call checkInput
    jmp GetInput

checkInput:
    # If the value is 13 (Enter) then jump out of this loop to "submit"
    cmp $13, %al
    je submit
    jmp printNumber     #Otherwise jump to printNumber and print out the character

printNumber:
    movb %al, (%si)         # Move the entered key into the buffer
    inc %si                 # Increment SI to go to the next byte in the buffer
    movb    $0x0e, %ah      # Setup for printing a character  
    int     $0x10           # Print out the character
    ret                     # Return to the getInput loop
    
submit:
    call cons_write_crlf                    # Print a break line
    inc %cx                                 # Check if CX is 2. If it is then all of the input has been received
    cmp $2, %cx
    je askForBytesToSearch
    jg printSectorEntered
    jmp SectorsToRead


askForBytesToSearch:
    movw $byteText, %si
    call cons_write
    movw $byteSearchBuffer, %si
    jmp GetInput


printSectorEntered:
    movw $enteredSector, %si                # Print out text saying which sector is being read aswell as the number from the buffer
    call cons_write
    movw $sectorEnteredBuffer, %si
    call cons_writeline

    call cons_write_crlf                    # Print a break line

    mov $0, %bx         # BX will hold the integer value of the entered sector. For now its 0

convertToInt:
    movw $sectorEnteredBuffer, %si          #Move the starting sector buffer into SI again. So '1'
    mov $0, %cx                             # Reset CX
    
startConversionLoop:
 xor %bx, %bx
conversionLoop:
    
    mov (%si), %ax
    sub $48, %ax                            # Substract 48 to get number version of ascii number. '1' + 48 would become 1
    add %ax, %bx                            # Add that number to bx. So 0 + 1 = 1
    inc %si                                 # Increment SI to move on to the next digit
    cmpb $0x20, (%si)                       # Compare 0 with that next digit
    je convertDone                          # If it is 0 that means the loop should finish
    jmp continueLoop

continueLoop:
    imul $10, %bx                           # Otherwise the loop should continue
    jmp conversionLoop



convertDone:
    push %bx        #Save current BX
    inc %cx
    cmp $2, %cx                         # If CX is 2 that means both strings have been converted to numbers
    je convertsDone                     # If its not then the second string should be moved to SI and that should be converted too
    movw $sectorsToReadBuffer, %si
    jmp startConversionLoop


convertsDone:
    pop %bx                                 # Pop of the first value from the stack. Its actually the same as the current BX but it still needs to be popped off to access the next value
    movw    $disk_address_packet, %si 
    mov    %bl, 2(%si)                      # How many sectors to read?
    movw    $0xD000, 4(%si)                 # Address to store data in
    pop %bx                                 # Pop of the next value which will be the starting sector number
    mov     %bl, 8(%si)                     # Starting sector
    movb    $0x42, %ah                   	
    int     $0x13
    jc read_failed  

    
    mov $32, %cx                # Move 32 into CX as one sector will contain 32 lines so it will need to loop 32 times

    imul 2(%si), %cx            # Multiple with the ammount of sectors that are read

    mov %cx, %di                # Move CX into DI and substrat 16 from it to get the number where the loop will need to pause
    sub $16, %di

    

    push %cx        # CX is pushed onto the stack as it will be modified in the cons_write_hex function

    mov $0, %bx     # Starting value of offset. so 0 - 0000
    push %bx
    mov $0, %dx    # Starting offset of the address

    push %dx        # Save the starting offset of 0xD000




beginSector:

    call cons_write_offset
    dec %dx                 # Decrement DX by one because for some reason the first byte is located at -1 not 0



sectorLoop:
    mov %dx, %si            # Move DX into SI to use as an index
    mov 0xD000(%si), %bx    # Move the address + offset into BX


    call cons_write_hex     # Print the address as a 2 digit hex character
    movb    $0x0e, %ah      # Setup for printing a character  
    movb    $32, %al        # 32 is the ascii code for space.
    int     $0x10           # Print out the space to leave a space between the values


    
    pop %ax             # Move saved DX into ax
    push %ax            # Move DX back
    add $15, %ax        #Add 15 to AX 

    
    inc %dx                 # Increment the offset if its less then 15. This will loop 16 times
    cmp %ax, %dx


    jl sectorLoop

    mov $0, %dx             # Starting offset of the ascii address
    pop %dx                 # Pop DX from stack back into DX
    push %dx                # Push it back to still keep it on the stack

asciiLoop:
    movb    $0x0e, %ah      # Setup for printing a character  
    mov %dx, %si
    mov 0xD000(%si), %al    
    cmp $31, %al             # If the value is less or equal to 31 jump to lessThenSpace
    jbe lessThenSpace
    jmp nextPart


lessThenSpace:
mov $95, %al            # Move $95 into AL, which is '_'
nextPart:

    int     $0x10           # Print out the value in AL

    pop %ax             # Move saved DX into ax
    push %ax            # Move DX back to still keep it in the stack
    add $16, %ax        # Add 15 to AX 


    inc %dx                 # Increment the offset if its less then 15. This will loop 16 times
    cmp %ax, %dx

    jl asciiLoop
    
    call cons_write_crlf


    pop %dx                 # Pop out DX and increment it by 16 to get the starting address for the next loop
    add $16, %dx

    pop %bx                 # Increment the offset value by 1
    inc %bx

    pop %cx                 # Pop the CX value, decrement it and push it back onto the stack to save the decreased loop index.
    dec %cx

    push %cx                # Push everything back on the stack
    push %bx
    push %dx

    
    cmp %di, %cx            # If loop iteration is equal to DI then jump to the wait for enter function
    je WaitForEnter

returnFromEnter:
    
    cmp $0, %cx             # Stop printing when CX is 0
    jg beginSector
    jmp endless_loop


read_failed:	                      # Display error messages
	movw 	$read_failed_msg, %si
    call    cons_writeline
	


WaitForEnter:
    movw $enterString, %si          # Display message asking to press enter
    call cons_write
EnterLoop:
    mov $0x00, %ah              # Set up for int 0x16
    int $0x16
    cmp $13, %al                # 13 is the code for Enter.
    jne EnterLoop               # If the key pressed is not enter loop the function.
    call cons_write_crlf        # Go to the next line and return to the loop to continue printing the sector
    sub $16, %di                # Sub 16 from DI to get next index to pause at
    jmp returnFromEnter

endless_loop:                   # Loop forever more
    jmp     endless_loop    


cons_write_hex:
mov $2, %cx     # Set loop number. In this case 2 since im printing 2 hex numbers
push %di        # Save DI in stack, Here it will be used as an index to decide the color to print.
mov $0, %di     # Move 0 to DI. This will mean the character will print in black.
mov $byteSearchBuffer, %si

byteCheck:
cmpb %bh, (%si)
je setGreenColor                        # If the value of BH matches the value in SI that means the byte should be highlighted
jmp dontHighlight

dontHighlight:
inc %si                                 # Move to the next value in the buffer
cmpb $0x20, (%si)                       # Check if that next value is a space. If it is stop searching bytes and move onto the hexloop
je hexloop
jmp byteCheck                           # Otherwise loop byteCheck


setGreenColor:
mov $1, %di


hexloop:                    # <<<NOTES>>>
push %cx

rol $4, %bx                 # Rotate bits to the left by 4. This means the first number will now become the last. 1234 - 2341
push %bx
mov %bx, %si                # Move this into SI. SI needs to be used when accesing array index
and $0x000F, %si            # Perform an AND to clear all other bytes except the last one. So 1111 1111 1111 0010 would become 0000 0000 0000 0010

movb $0x09, %ah
movb HexChars(%si), %al

mov $1, %cx
xor %bx, %bx    # Clear BX register. Otherwise changing the color in BL doesnt work
cmp $0, %di     # Compare 0 to DI. If its 0 print in black. If its anything else print in green.
je blackColor
jmp greenColor

blackColor: 
mov $0x07, %bl          # 0x07 is the code for black
jmp printColoredChar

greenColor:
mov $0x0A, %bl              # 0x0a is the code for green

printColoredChar:
int $0x10


movb $0x0e, %ah             # Setup for printing character later
movb HexChars(%si), %al     # Use that bit as an index. if the last number was something like 0100 then the number would be 4
int $0x10                   # Call func to print number

pop %bx                     # Restore registers
pop %cx

loop hexloop                # Loop this as many times as number in CX
pop %di
ret

HexChars:	.ascii "0123456789ABCDEF"




# Function to print offset of address. Its the same as printing hex but loops 5 times to print the last digit.
cons_write_offset:
mov $5, %cx     
movb $0x0e, %ah 

hexloop2:                    
rol $4, %bx                 
mov %bx, %si               
and $0x000F, %si            
cmp $5, %cx             # Check if this is the first loop iteration. If it is then jump to next to skip printing this character
je next
movb HexChars(%si), %al     
int $0x10                   

next:
loop hexloop2           
                        # Print another space after the offset is done to leave a space between the offset and the sectors values
movb    $32, %al        
int     $0x10           
ret




# Program data

boot_device:
    .byte   0

boot_message:     
    .string "Boot Loader Stage 2 loaded"             
booted_from_msg:
    .string "Booting from "
fd: .string "floppy disk"
hd: .string "hard drive"


sectorEnteredBuffer:
    .string "    "

sectorsToReadBuffer:
    .string "    "

askSectorsToRead:
    .string "How many sectors to read: "

read_failed_msg:
    .string "Failed to read disk"

enterString:
    .string "Press enter to continue:"

enterString2:
    .string "Enter number of starting sector to read:"

enteredSector:
    .string "Reading sector: "


byteText:
    .string "Enter bytes to search for in ASCII: "

byteSearchBuffer:
    .string "           "

test:
    .string "EB"


disk_address_packet:
    .byte   0x10
    .byte   0
    .word   0
    .long   0
    .long   0
    .long   0

