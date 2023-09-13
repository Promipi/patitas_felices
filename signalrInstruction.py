import logging
import sys
import subprocess
sys.path.append("./") #searchh modules
from signalrcore.hub_connection_builder import HubConnectionBuilder
from getmac import get_mac_address as gma


def dispensate():
    cmd = ['python3','dispense2.py']
    subprocess.Popen(cmd).wait()
    cmd2 = ['python3','dispense1.py']
    subprocess.Popen(cmd2).wait()
    cmd3 = ['python3', 'rele.py']
    subprocess.Popen(cmd3).wait()
    time.sleep(1000)
    

def do_instruction(instruction,con): #function to receive the intruction
    msg = instruction[0].split(" ")
    print(msg) 
    if(msg[0]  == "dispensate"):
        dispensate()
    if(msg[0] == "addSchedule"):
        feederId = msg[1]
        content = msg[2]
        if(my_mac == feederId):
            #lets do it
            connection.addSchedule(feederId,content)
    if(msg[0] == "getSchedules"):
        feederId = msg[1]
        if(my_mac == feederId):
            print("sex")
            result = connection.getSchedules(feederId)
            try:
                con.send("SendMessage", ["HOLA"])
                print(con)
                con.send("SendMessage",[f"SchedulesReceived {feederId} {result}"])
            except Exception as e: print(e)

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
hub_connection.on("ReceiveMessage", lambda instruction:do_instruction(instruction,hub_connection))
hub_connection.start()
my_mac = gma()
while(True): #to wait instructions
    exit_bool = input("Digite salir() si quiere terminar ")
    hub_connection.send("SendMessage",["hola"])
    if(exit_bool): break

hub_connection.stop()

sys.exit(0)
