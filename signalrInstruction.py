import logging
import sys
sys.path.append("./") #searchh modules
from signalrcore.hub_connection_builder import HubConnectionBuilder

def do_instruction(instruction): #function to receive the intruction to do
    print(instruction)

server_url =  "http://54.85.141.173/intructionHub"
handler = logging.StreamHandler()
handler.setLevel(logging.DEBUG)
hub_connection = HubConnectionBuilder()\
    .with_url(server_url, options={"verify_ssl": False}) \
    .with_automatic_reconnect({
            "type": "interval",
            "keep_alive_interval": 10,
            "intervals": [1, 3, 5, 6, 7, 87, 3]
        }).build()

hub_connection.on_open(lambda: print("connection opened and handshake received ready to send messages"))
hub_connection.on_close(lambda: print("connection closed"))

#wait for the message and execute the function created
hub_connection.on("ReceiveMessage", lambda instruction:do_instruction(instruction))
hub_connection.start()

while(True): #to wait instructions
    exit_bool = input("Digite salir() si quiere terminar ")
    if(exit_bool): break

hub_connection.stop()

sys.exit(0)
