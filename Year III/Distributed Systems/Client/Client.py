import socket

class Client:
    def __init__(self):
        self.HOST = socket.gethostbyname(socket.gethostname())    
        self.PORT = 1400
        self.ADDR = (self.HOST, self.PORT)  
        self.SOCKET = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        

    def connect(self):
        self.SOCKET.connect(self.ADDR)         
        print("Connected to server.")
        msg = "Connect"
        message = msg.encode('utf-8')
        self.SOCKET.sendall(message)
        connected = True            
        while connected:
            msg = self.SOCKET.recv(1024).decode('utf-8')        
            info = msg.split("|")                               
            authAddr = (info[0], int(info[1]))
            print(authAddr)
            authSocket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)          
            authSocket.connect(authAddr)
            token = authSocket.recv(1024).decode('utf-8')                        
            authSocket.close()                                                      
            print(token)                                                          
            input("Press enter to get list of audio files")
            msg = "GetAudioList"
            message = msg.encode('utf-8')                                           
            self.SOCKET.sendall(message)
            msg = self.SOCKET.recv(1024).decode('utf-8')        
            info = msg.split("|")   
            distAddr = (info[0], int(info[1]))
            print(distAddr)
            distSocket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)          
            distSocket.connect(distAddr)

            message = token
            msg = message.encode('utf-8')
            distSocket.send(msg)
            message = distSocket.recv(1024).decode('utf-8')     
            print(message)
            audioFileToDownload = input("Enter index of audio file to download it:")
            distSocket.sendall(audioFileToDownload.encode('utf-8'))

            with open("Download.mp3", 'wb') as file:            # Open/create file called download.mp3 that will store the audio file
                while True:
                    chunk = distSocket.recv(1024)               # Receive the first chunk
                    if chunk == b'1':                           # If the received chunk is b'1' that means the file has finished downloading and the loop can end
                        break
                    file.write(chunk)                           # Otherwise write the chunk data into the created file.
                print("File downloaded")
                connected = False
                input("Press enter to exit program") 
                exit()                                          # Exit program
                

c = Client()
c.connect()
input()