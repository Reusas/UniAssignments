import socket
import threading
from Encryptor import *

# Import enum module for states.
from enum import Enum


# Enum class that holds the different states.
class State(Enum):
    Messaging = 1
    Help = 2
    Name = 3
    Save = 4


class Client:
    def __init__(self):
        # The host is the local host again and the port matches the server port.
        self.HOST = None
        self.PORT = 1487
        self.ADDR = None
        self.username = ""
        # Keys for encryption
        self.publicKey = 151
        self.privateKey = 157
        # This clients partial key
        self.partialKey = None
        # Servers public key that will be sent over
        self.publicKey2 = None

        # This will be the partial key that the server sends over
        self.partialKey2 = None
        # fullKey will be the full key used for encryption and fullKeyGotten will be used to check if the key is calcu-
        # -lated.
        self.fullKey = None
        self.fullKeyGotten = False
        # Variables to keep track which state client is currently in.
        self.currentState = State.Messaging
        self.previousState = None

        # Command that will transfer user to help state
        self.helpCommand = "/help"
        # Command that will transfer user to change name state
        self.nameCommand = "/name"
        # Command that will trasnfer user to save name state
        self.saveCommand = "/save"
        # Text that will be shown for the user when they are in the Help state.
        self.helpText = "\n" \
                        "/help - displays help text\n " \
                        "/name - allows to change username\n" \
                        "/save - allows to save username "

    def generatePartialKey(self):
        # Generate partial key using my encryptor class. Here i pass in the servers public key (publicKey2) first.
        e = Encryptor(self.publicKey2, self.publicKey, self.privateKey)
        partialKey = e.createPartialKey()
        self.partialKey = partialKey

    def connect(self):
        # Create socket object again. This time I use "connect" to connect to the address rather than binding it like
        # in the server.
        with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
            try:
                s.connect(self.ADDR)
                # Send the public key to the server right away.
                self.sendPublicKey(s)
                self.messageLoop(s)
            except socket.error:
                print("Failed to connect to server!")
                input()

    # This function will send the public key to the server on connection.
    def sendPublicKey(self, socket):
        # Converting int to str to be able to encode it.
        msg = self.publicKey.__str__()
        message = msg.encode('utf-8')
        socket.sendall(message)

    def changeName(self):
        # Set username variable from keyboard input.
        self.username = input("Please enter your username: ")

    def messageLoop(self, socket):
        print(f"Welcome {self.username}!")
        print(f"Type {self.helpCommand} for help!")
        # Start a new thread that will handle receiving messages. This is needed since input() blocks and will
        # prevent received messages from being displayed.
        thread = threading.Thread(target=self.receiveMessages, args=(socket,))
        thread.start()
        while True:

            # Get message from input
            msg = input()
            # If the current state is the messaging state then messages will be sent out to server.
            if self.currentState == State.Messaging:
                # If the user types "/help" set the current state to the help state.
                if msg == self.helpCommand:
                    self.previousState = State.Messaging
                    self.currentState = State.Help
                    # Set state to Name state
                elif msg == self.nameCommand:
                    self.previousState = State.Messaging
                    self.currentState = State.Name
                    # Set state to Save state
                elif msg == self.saveCommand:
                    self.previousState = State.Messaging
                    self.currentState = State.Save
                # If none of the commands are entered then send messages to server
                else:
                    msg = self.username + ": " + msg
                    # Encode message with utf-8 format and send it to the server
                    message = msg.encode('utf-8')
                    socket.sendall(message)
            # If The state is help than instead of sending messages ask the user to press Enter to continue
            if self.currentState == State.Help:
                # Display commands, ask user to press enter to continue
                print(self.helpText)
                input("Press Enter to return to messaging.")
                # Return to messaging state and make help the previous state
                self.previousState = State.Help
                self.currentState = State.Messaging

                # Executed if the current state is name.
            if self.currentState == State.Name:
                # Call the changeName method from the start.
                self.changeName()
                # Return to messaging state.
                self.previousState = State.Name
                self.currentState = State.Messaging
                # Executed if current state is Save
            if self.currentState == State.Save:
                print("Would you like to save your username for future logins? y/n")
                # Loop to get input incase user enters wrong input
                while True:
                    i = input()
                    if i == "y":
                        # Call saveUserName function and set state back to messaging.
                        self.saveUsername()
                        self.previousState = State.Save
                        self.currentState = State.Messaging
                        break
                    elif i == "n":
                        # Return to messaging state and out of the loop.
                        self.previousState = State.Save
                        self.currentState = State.Messaging
                        break
                    else:
                        print("Wrong input! y or n accepted!")

    def receiveMessages(self, socket):
        # Receive messages and print them if they exist.

        # Value to keep track of how many keys have been received from the server
        hasReceivedKey = 0
        while True:
            try:
                msg = socket.recv(1024).decode('utf-8')
            except ConnectionResetError:
                print("The server has been shutdown and you have been disconnected!")
                msg = 0
                return

            # if the key has not been received than this first message gets assigned to the public key variable.
            if hasReceivedKey == 0 and not self.fullKeyGotten:
                self.publicKey2 = int(msg)
                # Generate the partial key now that the publicKey2 is gotten.
                self.generatePartialKey()
                # Send over the partialKey to the server
                msg = self.partialKey.__str__()
                socket.sendall(msg.encode('utf-8'))

            if hasReceivedKey == 1 and not self.fullKeyGotten:
                # get partialKey from server and calculate full key from it and the servers public key using Encryptor
                # class
                self.partialKey2 = int(msg)
                e = Encryptor(self.publicKey2, self.publicKey, self.privateKey)
                self.fullKey = e.createFullKey(self.partialKey2)
                self.fullKeyGotten = True

            if msg and hasReceivedKey >= 2:
                # Decrypt and print message received from server
                e = Encryptor(self.publicKey2, self.publicKey, self.privateKey)
                e.fullKey = self.fullKey
                message = e.decrypt(msg)
                print(message)

                # Update received key count values here
            if hasReceivedKey == 0:
                hasReceivedKey = 1
            elif hasReceivedKey == 1:
                hasReceivedKey = 2

    def saveUsername(self,):
        # Open or create a text file (if it doesnt exist) and write the username in it.
        f = open("savefile.txt", "w")
        f.write(self.username)
        # Close the file
        f.close()

    def loadUsername(self):
        # Try to load username file.
        try:
            f = open("savefile.txt", "r")
            self.username = f.readline()
        except FileNotFoundError:
            # If the file does not exist call the function to change name.
            self.changeName()

    def connectToServer(self):
        # Get input for the host address. If the length is 0 that means that user didint enter anything and default
        # host should be used.
        host = input("Enter the host address(Leave empty for default): ")
        if len(host) == 0:
            self.HOST = socket.gethostbyname(socket.gethostname())
        # If len is not = then the host should be assigned to the users input
        else:
            self.HOST = host
    # Combine host and port to get address
        self.ADDR = (self.HOST, self.PORT)


if __name__ == "__main__":
    client = Client()
    client.connectToServer()
    client.loadUsername()
    client.connect()
