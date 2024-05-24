import socket
import threading
import random
class ControlNode:
    def __init__(self):
        self.HOST = socket.gethostbyname(socket.gethostname())    
        self.PORT = 1400
        self.ADDR = (self.HOST, self.PORT)  
        self.SOCKET = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        

    def connect(self):
        self.SOCKET.connect(self.ADDR)                          # Connect to prime node
        msg = "CONTROL|" + self.HOST + "|" + str(self.PORT)     # Send name, host and port divided by '|' symbol so it can be split into 3 variables in the prime node
        message = msg.encode('utf-8')
        self.SOCKET.sendall(message)
        print("Connected to prime node.")
        while True:
            msg = self.SOCKET.recv(1024).decode('utf-8')
            if msg == "START AUTHENTICATION SERVICE":                               # Start authentication or distribution node depending on the message received from prime node
                thread = threading.Thread(target = self.authenticationNode, args=())
                thread.start()
            elif msg == "START DISTRIBUTION SERVICE":
                thread = threading.Thread(target = self.distributionNode, args=())
                thread.start()

            

    def authenticationNode(self):
        print("Launching authentication node")
        with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:        # Launch authentication node socket
            self.HOST = socket.gethostbyname(socket.gethostname())    
            self.PORT = 1401
            self.ADDR = (self.HOST, self.PORT)  
            s.bind(self.ADDR)                                               
            print("Auth Server has started!")
            msg = "AUTHENTICATE|" + self.HOST + "|" + str(self.PORT)        # Send over the information of this node to the prime node to register. The message is divided by '|' to seperate name host and port
            message = msg.encode('utf-8')
            self.SOCKET.sendall(message)
            s.listen()                                                      
            while True:
                conn, addr = s.accept()                                     
                print(f"{addr} has connected ")
                thread = threading.Thread(target=self.handleClient, args=(conn, addr))  # Launch thread to handle connected clients
                thread.start()

    def handleClient(self, conn, addr):
        print("Sending token to client- TOKEN123")
        while True:
            try:
                message = "TOKEN123"                    # Send over the token to the connected client
                msg = message.encode('utf-8')
                conn.send(msg)
            except:
                print(f"{addr} has disconnected!")          # When a node disconnects, it is removed from the entries list.
                return
            
    def handleClientDistribute(self, conn, addr, audioList):
        
        while True:
            try:
                token = conn.recv(1024).decode('utf-8') # Receive message and decode it from the utf-8 format. This will be the token sent from the client
                print(f"Token from client received: {token}")
                
                test = "GetService|AUTHENTICATE".encode('utf-8')            # Connect to prime node and request the information of the authentication node
                HOST = socket.gethostbyname(socket.gethostname())    
                PORT = 1400
                ADDR = (HOST, PORT)  
                SOCKET = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
                SOCKET.connect(ADDR)
                SOCKET.sendall(test)
                message = SOCKET.recv(1024).decode('utf-8')
                splitMessage = message.split("|")
                authAddr = (splitMessage[0], int(splitMessage[1]))          # Split the message to get host and port and make it a tuple
                SOCKET.close()

                print(authAddr)
                SOCKET = socket.socket(socket.AF_INET, socket.SOCK_STREAM)      # Connect to the authorization node
                SOCKET.connect(authAddr)
                tokenFromAuth = SOCKET.recv(1024).decode('utf-8')               # Auth node will send a token.
                if tokenFromAuth == token:                                      # if the tokens match, that means the clients token is correct and authorization is granted
                    print("Client authorized. Sending audio list")
                    conn.send(str(audioList).encode('utf-8'))                   # Send over the audio list to the client
                else:
                    print("Client token invalid. Authorization rejected.")
                
                audioFileIndex = conn.recv(1024).decode('utf-8')      
                print(audioList[int(audioFileIndex)])  

                with open(audioList[int(audioFileIndex)], 'rb') as file:        # Open audio file
                    while True:
                        chunk = file.read(1024)                         # Read first chunk
                        if not chunk:
                            print("Done")                               # If the chunk is empty send byte b'1' to the client to let it know that the download is finished and break the loop
                            conn.sendall(b'1')
                            break
                        conn.sendall(chunk)                             # Otherwise send the current chunk



                print("Done sending.")
                

               # message = "AudioList"
               # msg = message.encode('utf-8')
                #conn.send(msg)
            except:
                print(f"{addr} has disconnected!")          # When a node disconnects, it is removed from the entries list.
                return


    def distributionNode(self):
        audioList = ["Test.mp3", "TestShort.mp3"]                           # List of available audio files to send
        print("Launching distribution node")
        
        with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:        # Launch distribution node socket
            self.HOST = socket.gethostbyname(socket.gethostname())    
            self.PORT = random.randint(1402,1502)
            self.ADDR = (self.HOST, self.PORT)  
            s.bind(self.ADDR)                                               
            print(f"Distribution Server has started!")
            msg = "DISTRIBUTE|" + self.HOST + "|" + str(self.PORT)
            message = msg.encode('utf-8')
            self.SOCKET.sendall(message)
            s.listen()                                                      
            while True:
                conn, addr = s.accept()                                     
                print(f"{addr} has connected ")
                thread = threading.Thread(target=self.handleClientDistribute, args=(conn, addr, audioList))  # Launch thread to handle connected clients
                thread.start()



c = ControlNode()
print("Waiting for prime node to start...")
while True:
    try:                # Loop here trying to connect to a prime node. It will loop until it connects
        c.connect()
        break
    except:
        pass





