from picamera import PiCamera
from time import sleep
from datetime import datetime
import cloudinary.uploader

def capture(path):
    camera.start_preview()
    sleep(5)
    camera.capture(path)
    camera.stop_preview() #take the photo and then save it with the actual  daclote and time
    

date_now = str(datetime.now()).replace(" ","-").replace(":","-").replace(".","-")
path = '/home/promipi/Documents/Projects/patitas_felices/photos/' + date_now + '.jpg'
#camera configuration
camera = PiCamera()
camera.resolution = (2592, 1944)
#endof

#cloud service configuration
cloudinary.config( 
  cloud_name = "dclopakaq", 
  api_key = "466274169171825", 
  api_secret = "UgRQqzUWAONIZqvzazVF1yKS8K4" 
)
#endof

#capture and upload the photo
capture(path)
cloudinary.uploader.upload(path,public_id = path,folder="photos_project")
print("Foto subida! ")
#endof

