import sqlite3
import time
from datetime import datetime
import subprocess
# Nombre de la base de datos SQLite
db_file = "bd1.db"

def check_schedule():
    try:
        # Conectar a la base de datos
        conn = sqlite3.connect(db_file)
        cursor = conn.cursor()

        # Obtener la hora actual
        current_time = datetime.now().strftime("%H:%M")

        # Consultar la base de datos para ver si la hora actual coincide con alguna hora en "Schedules"
        cursor.execute("SELECT id FROM Schedules WHERE Content = ?", (current_time,))
        result = cursor.fetchone()

        if result:
            cmdDispensate = ['python3', 'dispensateAll.py']
            subprocess.Popen(cmdDispensate).wait()
        
        # Cerrar la conexión a la base de datos
        conn.close()

    except Exception as e:
        print(f"Error: {e}")

if __name__ == "__main__":
    while True:
        check_schedule()
        # Esperar 1 segundo antes de verificar nuevamente
        time.sleep(1)