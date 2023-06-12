import RPi.GPIO as GPIO
import time
import subprocess

# declare the sensor and led pin
sensor_pin = 23

# GPIO setup
GPIO.setwarnings(False)
GPIO.setmode(GPIO.BCM)
GPIO.setup(sensor_pin, GPIO.IN)

print("Sensor iniciado!")
try:
    while True:
        if GPIO.input(sensor_pin):
            # If no object is near
            while GPIO.input(sensor_pin):
                time.sleep(0.2)
        else:
            # If an object is detected
            cmd = ['python', 'photo_uploader.py']
            subprocess.Popen(cmd).wait()
            time.sleep(1)
            
except KeyboardInterrupt:
    GPIO.cleanup()
