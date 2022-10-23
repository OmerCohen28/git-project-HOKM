from models.database import Database
from controller.handler import Handler
from _thread import start_new_thread
import sys
import os


def main():
    # fixing some bugs
    os.chdir(os.path.dirname(__file__))

    gui = True

    database = Database("data/players.db")
    database.create_scores_table()

    start_new_thread(lambda: os.system("view\\windowsFormsApp4.exe"), ())

    a = Handler(database, gui)
    sys.excepthook = a.handle_error
    a.start()


if __name__ == "__main__":
    main()
