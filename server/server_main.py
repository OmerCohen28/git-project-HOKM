from models.database import Database
from models.base_classes import Player
from controller.handler import Handler
from view.score_client import ScoreClient
from _thread import start_new_thread
import sys
import os
import subprocess
import time


def main():
    # fixing some bugs
    os.chdir(os.path.dirname(__file__))

    gui = True

    database = Database("data/players.db")
    database.create_scores_table()

    while gui is not None:
        if gui:
            # start_new_thread(lambda: os.system("view\\ServerGameGUI.exe"), ())
            start_new_thread(lambda: subprocess.Popen("view\\ServerGameGUI.exe", shell=True), ())

        a = Handler(database, gui)
        sys.excepthook = a.handle_error
        a.start()

        time.sleep(2)
        Player.player_id = 1

        score_client = ScoreClient(database)
        gui = score_client.start()
        if gui is not None:
            score_client.stop()




if __name__ == "__main__":
    main()
