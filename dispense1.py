import RPi.GPIO as GPIO
from time import sleep

in2 = 27
in1 = 22
ena = 17

GPIO.setmode(GPIO.BCM)
GPIO.setwarnings(False)


GPIO.setup(in1,GPIO.OUT)
GPIO.setup(in2,GPIO.OUT)
GPIO.setup(ena,GPIO.OUT)

GPIO.output(ena, GPIO.HIGH)
GPIO.output(in1,GPIO.HIGH)
GPIO.output(in2, GPIO.LOW)
sleep(0.05)

GPIO.cleanup()
