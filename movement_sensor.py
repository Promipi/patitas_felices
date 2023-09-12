import RPi.GPIO as GPIO
import time
import subprocess

pir_sensor= 23

GPIO.setwarnings(False)
GPIO.setmode(GPIO.BCM)

GPIO.setup(pir_sensor, GPIO.IN)

current_state = 0

try:
    while True:
        time.sleep(0.1)
        current_state = GPIO.input(pir_sensor)
        if current_state == 1:
            print("GPIO pin %s is %s" % (pir_sensor, current_state))
            print("hello")
            time.sleep(10)
            cmd = ['python3','photo_uploader.py']
            subprocess.Popen(cmd).wait()
            current_state = 0

except KeyboardInterrupt:
    pass
finally:
    GPIO.cleanup()
