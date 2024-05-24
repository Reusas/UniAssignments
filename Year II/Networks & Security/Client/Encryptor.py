# This encryptor class uses the Diffie-Hellman algorithm

class Encryptor:
    # Initialisation class takes in two public keys and a private key.
    def __init__(self, publicKey1, publicKey2, privateKey):
        self.publicKey1 = publicKey1
        self.publicKey2 = publicKey2
        self.privateKey = privateKey
        self.fullKey = None

    def createPartialKey(self):
        # The formula for creating a partial key is public key power of private key modulus second public key
        partialKey = (self.publicKey1 ** self.privateKey) % self.publicKey2
        return partialKey

    def createFullKey(self, partialKey):
        # The formula for the full key is the partial key power of private key modulus second public key
        fullKey = (partialKey ** self.privateKey) % self.publicKey2
        return fullKey

    def encrypt(self, msg):
        # Create empty string where encrypted characters will be added
        encryptedMsg = ""
        key = self.fullKey
        for c in msg:
            # For every character in the message replace it with its unicode + the key
            encryptedMsg += chr(ord(c)+key)
        return encryptedMsg

    def decrypt(self, msg):
        decryptedMsg = ""
        key = self.fullKey
        for c in msg:
            # To decrypt I have to take away the key from every unicode letter.
            decryptedMsg += chr(ord(c)-key)
        return decryptedMsg

