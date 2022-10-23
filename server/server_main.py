from models.database import Database
from controller.handler import Handler
import sys
import os

# fixing some bugs
os.chdir(os.path.dirname(__file__))

if __name__ == "__main__":
    database = Database("data/players.db")
    database.create_scores_table()
    a = Handler(database)
    sys.excepthook = a.handle_error
    a.start()