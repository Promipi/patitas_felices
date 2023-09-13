import sqlite3
import uuid
def create_connection():
    connection = sqlite3.connect("bd1.db")
    return connection

def getSchedules(feederId):
    cur = create_connection().cursor()
    res = cur.execute(f"SELECT * from Schedules WHERE FeederId='{feederId}' ")
    return res.fetchall()

def addSchedule(feederId,Content):
    connection = create_connection()
    cur = connection.cursor()
    res = cur.execute(f"Insert INTO Schedules VALUES ( '{uuid.uuid4()}', '{feederId}', '{Content}' ) ")
    connection.commit()
