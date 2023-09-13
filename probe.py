import logging
import sys
sys.path.append("./")
from signalrcore.hub_connection_builder import HubConnectionBuilder

def getsall(x):
    print(x)

server_url = "http://192.168.1.191:5000/instructionHub"
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
hub_connection.on("ReceiveMessage", lambda x : print(x) )



hub_connection.start()
message = None

# Do login

while message != "exit()":
    message = input("digite msg:  ")
    if message is not None and message != "" and message != "exit()":
        hub_connection.send("SendMessage", [message])

hub_connection.stop()

sys.exit(0)
