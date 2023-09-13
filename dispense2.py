import RPi.GPIO as GPIO
from time import sleep

in4 = 27
in3 = 22
ena = 17


GPIO.setmode(GPIO.BCM)
GPIO.setwarnings(False)


GPIO.setup(in4,GPIO.OUT)
GPIO.setup(in3,GPIO.OUT)
GPIO.setup(ena,GPIO.OUT)

GPIO.output(ena, GPIO.HIGH)
GPIO.output(in3,GPIO.LOW)
GPIO.output(in4, GPIO.HIGH)

sleep(0.2)

GPIO.cleanup()
