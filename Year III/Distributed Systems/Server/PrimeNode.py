import socket
import threading
import time

class PrimeNode:
    def __init__(self):
        self.HOST = socket.gethostbyname(socket.gethostname())              # Get The Host address of the local PC
        self.PORT = 1400
        self.ADDR = (self.HOST, self.PORT)                                  # Create tuple that combines host ant port.
        self.ENTRIES = []                                                   # This will store the address and the id of the nodes 
        self.systemRunning = False
        self.distributionNodes = []
        


    def startServer(self):
       
        with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:        # Create socket object using with statement, so it closes on its own without errors.
            s.bind(self.ADDR)                                               # Bind the socket to the address.
            print(f"Server has started! Listening on {self.HOST}")
            s.listen()                                                      # Listen for incoming connections.
            thread = threading.Thread(target=self.primeNodeHandle, args=())  # Create a thread to handle prime node functions as the bellow code will 'block' the code.
            thread.start()
            while True:
                conn, addr = s.accept()                                     # s.accept returns two variables that I assign to conn and addr.
                thread = threading.Thread(target=self.handleClient, args=(conn, addr))  # Create a thread that will handle connections
                thread.start()
                

                
    def getEntry(self,msg,conn):                    # This function will save the information of a node that connected
        entry = msg.split("|")                      # The entry should split the message to get the name and connection info
        entry.append(conn)                          # Append the connection info to the entry list
        self.ENTRIES.append(entry)                                  
    
    def displayEntries(self):                                               # Print out the list of current entries if the list contains anything
        if len(self.ENTRIES) != 0:
            print("Current entries:")                                   
        index = 0
        for e in self.ENTRIES:
            index+=1
            print(f"{index}: {e[0]} {e[1]} {e[2]}")        # Print out the Name , host and port.

    def primeNodeHandle(self):
        time.sleep(3)                                             # Wait for some time to allow control nodes to connect
        input("Press enter to start system")
        self.startService()

    def startService(self):                                 
        msg = "START AUTHENTICATION SERVICE"                
        finalMsg = msg.encode('utf-8')
        self.ENTRIES[0][3].send(finalMsg)       # Send message to the first control node to start auth service
        msg = "START DISTRIBUTION SERVICE"
        finalMsg = msg.encode('utf-8')
        self.ENTRIES[1][3].send(finalMsg)               # Send message to second node to start distribution service
        finalMsg = msg.encode('utf-8')
        self.ENTRIES[2][3].send(finalMsg)               # Send message to second node to start distribution service
        time.sleep(1)
        print("System running! Waiting for clients")
        self.getDistributionNodes() # This function will store all of the distribution nodes in the system in a list. This will be used for load balancing
        self.systemRunning = True

    def getService(self,name):
        for entry in self.ENTRIES:      # Loop through every entry and compare first value with name. If they match return that entry
            if entry[0] == name:
                return entry


    def handleClient(self, conn, addr):
        while True:
            try:
                if self.systemRunning == False:                 # If system is not running then all messages will be handled as if they are node information
                    message = conn.recv(1024).decode('utf-8') # Receive message and decode it from the utf-8 format.
                    self.getEntry(message, conn)               # Save node to entry list and display entries
                    self.displayEntries()
                else:
                    message = conn.recv(1024).decode('utf-8')               # If the system is already running then the messages are comming from a client.
                    if message == "Connect":                                # If the message is 'Connect', then the information of the authentication node should be sent to the client
                        print("Client connected!")
                        
                        authEntry = self.getService("AUTHENTICATE")             # Find authentication node from entries
                        message = str(authEntry[1]) + "|" + str(authEntry[2])   # Pass host and port of the entry
                        msg = message.encode('utf-8')
                        conn.send(msg)
                    if message == "GetAudioList":
                        loadBalancedNode = self.loadBalance()
                        distEntry = self.getService("DISTRIBUTE")
                        message = str(loadBalancedNode[1]) + "|" + str(loadBalancedNode[2])
                        msg = message.encode('utf-8')
                        conn.send(msg)
                    if "GetService" in message:
                        messageSplit = message.split('|')                   # Split message into 2. The second part will contain the service name
                        serviceEntry = self.getService(messageSplit[1])     # Get the entry containing that service
                        authAddr = "" + serviceEntry[1]+ "|" + serviceEntry[2]
                        conn.send(authAddr.encode('utf-8'))
                        print(serviceEntry)

            except:
                print(f"{addr} has disconnected!")          # When a node disconnects, it is removed from the entries list.
                return


    def getDistributionNodes(self):
        for entry in self.ENTRIES:
            if entry[0] == "DISTRIBUTE":
                load = 0
                entry.append(load)      # Append the load at the end of the entry which is index 4
                self.distributionNodes.append(entry)            

    def loadBalance(self):
        leastLoadIndex = 0   #Index for which node from the list has the least load
        lowestLoad = self.distributionNodes[0][4]       # Lowest load value. By default its set to the load of the very first distribution node in the list
        print(lowestLoad)
        loopIndex = -1  # Current index of loop

        for entry in self.distributionNodes:
            loopIndex+=1 # Start loop index at 0
            if entry[4] < lowestLoad:               #Compare load value of the current distribution node in the loop to the lowest load
                leastLoadIndex = loopIndex          # If the load is smaller that means the index of this node should be saved

        self.distributionNodes[leastLoadIndex][4] += 1 # Increment the load value of the current distribution node
        return self.distributionNodes[leastLoadIndex]
            
        
        





if __name__ == "__main__":
    pN = PrimeNode()
    print("Starting Prime node...")
    pN.startServer()
    