import logging
import sys
import subprocess
import time
sys.path.append("./") #searchh modules
from signalrcore.hub_connection_builder import HubConnectionBuilder
from getmac import get_mac_address as gma
import connection

def do_instruction(instruction,con): #function to receive the intruction
    msg = instruction[0].split(" ")
    print(msg) 
    if(msg[0]  == "dispensate"):
        cmdDispensate = ['python3', 'dispensateAll.py']
        subprocess.Popen(cmdDispensate).wait()

    if(msg[0] == "addSchedule"):
        feederId = msg[1]
        content = msg[2]
        #if(my_mac == feederId):
        #lets do it
        connection.addSchedule(feederId,content)
            
    if(msg[0] == "getSchedules"):
        feederId = msg[1]
        #if(my_mac == feederId):
        print("sex")
        result = connection.getSchedules(feederId)
        try:
            con.send("SendMessage",[f"SchedulesReceived {feederId} {result}"])
        except Exception as e: print(e)

server_url =  "http://192.168.1.191:5000/instructionHub"
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
hub_connection.on("ReceiveMessage", lambda instruction:do_instruction(instruction,hub_connection))
hub_connection.start()
my_mac = gma()
while(True): #to wait instructions
    exit_bool = input("Digite salir() si quiere terminar ")
    hub_connection.send("SendMessage",["hola"])
    if(exit_bool): break

hub_connection.stop()

sys.exit(0)
