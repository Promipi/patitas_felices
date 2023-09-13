import subprocess
import time 

cmd = ['python3','dispense2.py']
subprocess.Popen(cmd).wait()
cmd2 = ['python3','dispense1.py']
subprocess.Popen(cmd2).wait()
cmd3 = ['python3', 'rele.py']
subprocess.Popen(cmd3).wait()
time.sleep(1000)
    

