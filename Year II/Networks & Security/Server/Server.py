# Importing the modules i will use. Sockets for networking and threading for threads.
import socket
import threading
# This is my own encryptor class that will use the Diffie-Hellman algorithm
from Encryptor import *
import time


# Create server class.
class Server:
    def __init__(self):
        # Get The Host address of the local PC
        self.HOST = socket.gethostbyname(socket.gethostname())
        self.PORT = 1487
        # Create tuple that combines host ant port.
        self.ADDR = (self.HOST, self.PORT)
        # List that will keep track of all connected clients so that messages can be sent to all of them
        self.connList = []
        # Define the public and private keys used for encryption. The second public key will be of the client.
        self.publicKey = 197
        self.publicKey2 = None
        self.privateKey = 199
        self.partialKey = None
        # This is the partial key that the client will send over
        self.partialKey2 = None
        # The full key that will be used for encryption
        self.fullKey = None
        # Variable to check if the full key has been calculated to ensure that the client sent all of its keys.
        self.fullKeyGotten = False

        # Create partial key using encryptor class
    def generatePartialKey(self):
        # Generate partial key from encryptor class and set it to self.partialKey
        e = Encryptor(self.publicKey, self.publicKey2, self.privateKey)
        partialKey = e.createPartialKey()
        self.partialKey = partialKey

    def startServer(self):
        # Create socket object using with statement, so it closes on its own without errors.
        with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
            # Bind the socket to the address.
            s.bind(self.ADDR)
            # Listen for incoming connections.
            print(f"Server has started! Listening on {self.HOST}")
            s.listen()
            while True:
                # S.accept returns two variables that I assign to conn and addr.
                conn, addr = s.accept()
                # Add the new connection to the connection list.
                self.connList.append(conn)
                # Check if partialKey2 is not set to none.
                if self.partialKey2 is not None:
                    # If its not None that means that a client already sent it over. In this case i want to clear all
                    # of my key variables so that another client can send them over.
                 del self.partialKey2
                 del self.fullKey
                 del self.publicKey2
                 del self.partialKey
                 self.fullKeyGotten = False
                # Create a thread. The target is the handleClient function with two arguments being conn and addr.
                thread = threading.Thread(target=self.handleClient, args=(conn, addr))
                thread.start()
                # Print how many total threads are running. -1 to not include the main server thread, just the clients.
                print(f"Active connections: {threading.activeCount() - 1}")

    # This function will handle the messages from clients.
    def handleClient(self, conn, addr):
        print(f"{addr} has connected!")
        # This value will be responsible for checking if the keys from the client have been received. 0 will mean
        # no keys have been received, 1 will be public key and 2 will be partial key received.
        hasReceivedKey = 0
        while True:
            try:
                # Receive message and decode it from the utf-8 format.
                message = conn.recv(1024).decode('utf-8')
                # When the client connects it will automatically send the public key over to the server. This if
                # statement checks if the key value is 0 which means this will be the public key sent over first
                if hasReceivedKey == 0 and not self.fullKeyGotten:
                    # Assigning the public key. I also convert the message to int as the key will be used in
                    # calculations
                    self.publicKey2 = int(message)
                    # Now that the second public key is assigned i can generate the partial key.
                    self.generatePartialKey()

                    # These lines send the servers public key  to the client.
                    msg = self.publicKey.__str__()
                    conn.send(msg.encode('utf-8'))
                    # Delay so that the messages dont get sent to fast which ends up merging them into one.
                    time.sleep(1)
                    # These send the servers partial key to the client
                    msg = self.partialKey.__str__()
                    conn.send(msg.encode('utf-8'))

                # This if statement checks if the hasReceivedKey value is 1 meaning that the partial key is now being
                # sent over
                if hasReceivedKey == 1 and not self.fullKeyGotten:
                    self.partialKey2 = int(message)
                    # These lines generate the full key now that the partial key and public key are both known.
                    e = Encryptor(self.publicKey,self.publicKey2,self.privateKey)
                    self.fullKey = e.createFullKey(self.partialKey2)
                    self.fullKeyGotten = True

                # This will only be executed if the hasReceivedKey value is more then 2 meaning that both keys have
                # been received
                if message and hasReceivedKey >= 2:
                    # For every client in the client list send them the message that was received.
                    for c in self.connList:
                        # Only send the message to a client if it is not the client that currently sent it. It is not
                        # neccesary to resend the message that a client typed back to them as they already see it.
                        if conn != c:
                            e = Encryptor(self.publicKey, self.publicKey2, self.privateKey)
                            e.fullKey = self.fullKey
                            msg = e.encrypt(message)
                            c.sendall(msg.encode('utf-8'))

                    # Change hasReceivedKey values here bellow. This means that the lines above will not be executed
                    # one after the other and will ensure that the message order is followed correctly.
                if hasReceivedKey == 0:
                    hasReceivedKey = 1
                elif hasReceivedKey == 1:
                    hasReceivedKey = 2
            # Exception here to prevent error being thrown if the client closes their terminal.
            except:
                print(f"{addr} has disconnected!")
                # Remove connection from connection list so that messages dont get sent to it.
                self.connList.remove(conn)
                # Return statement to quit the loop.
                return


if __name__ == "__main__":
    server = Server()
    server.startServer()
