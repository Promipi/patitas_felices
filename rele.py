import RPi.GPIO as GPIO
from time import sleep

pinRele = 26

GPIO.setmode(GPIO.BCM)
GPIO.setwarnings(False)


GPIO.setup(pinRele,GPIO.OUT)
GPIO.output(pinRele, GPIO.HIGH)
sleep(2)
GPIO.output(pinRele, GPIO.LOW)
sleep(0.8) 
GPIO.cleanup()
