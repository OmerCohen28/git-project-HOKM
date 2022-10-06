from base_classes import *
from server import Server
from game import Game
import random
import time


BAD_CARD_MSG = "bad_card"
BAD_PLAY_MSG = "bad_play"

DELAY_BETWEEN_TURNS_IN_SEC = 0.3


def list_to_str(lst, sep="|"):
    """
    joins an array to a string with a sep
    :param lst: list
    :param sep: str
    :return: str
    """
    return sep.join([str(item) for item in lst])


class Handler(Server):
    def __init__(self, ip="0.0.0.0", port=55555):
        """
        setting up the handler server
        :param ip: str
        :param port: int
        """

        super().__init__(ip, port)

        self.players = []
        self.game = None
        self.current_player = None
        self.played_cards_dict = {1: "", 2: "", 3: "", 4: ""}

    def _handle_data(self, client_id: int, msg: str, msg_type="data"):
        """
        function that handles the requests of the players and manages the game
        :param client_id: int - id of the player who sent the request
        :param msg: str - the request of the player
        :param msg_type: str - the type of request that the player sent
        :return: None
        """

        if msg_type == "new_client":
            self.handle_new_player(client_id)
        elif msg_type == "client_disconnected":
            self.handle_player_disconnect(client_id)
        else:
            if msg.startswith("set_strong:"):

                suit = msg[len("set_strong:"):]
                self.handle_set_strong_suit(client_id, suit)
            elif msg.startswith("play_card:"):

                card = msg[len("play_card:"):]
                self.handle_play_card(client_id, card)

    def handle_set_strong_suit(self, client_id, suit):
        """
        sets strong suit to get logic with error checking
        :param client_id: int
        :param suit: str
        :return: None

        """
        if client_id != self.game.ruler.player_id:
            return

        if self.game.strong_suit is not None:
            return

        if suit not in Suit.__members__:
            self.send_message(client_id, "bad")
        else:
            self.game.set_strong_suit(Suit[suit])
            self.send_message(client_id, "ok")

            self.game.hand_cards_for_all()

            # sends remaining cards for all players in format: suit*rank|suit*rank...
            for player in self.game.players:
                self.send_message(player.player_id, f"{list_to_str(player.hand)},teams:{list_to_str(self.game.teams)},strong:{suit}")

            # format like this: "teams:1+3|2+4,strong:DIAMONDS"
            # self.send_all(f"teams:{list_to_str(self.game.teams)},strong:{suit}")
            time.sleep(DELAY_BETWEEN_TURNS_IN_SEC)
            self.start_turn()

    def start_turn(self):
        """
        starts the turn by sending to the player the status on the board
        :return: None
        """

        player = self.game.get_current_player_turn()
        self.current_player = player

        round_status = self.game.get_round_state()

        played_cards_by_id = [self.played_cards_dict[1], self.played_cards_dict[2], self.played_cards_dict[3], self.played_cards_dict[4]]

        msg = f"played_suit:{'' if round_status.played_suit is None else round_status.played_suit.name},played_cards:{list_to_str(played_cards_by_id)}"
        self.send_message(player.player_id, msg)

        #self.update_server_gui()

        #time.sleep(1)

    def handle_play_card(self, client_id, str_card):
        """
        handles the player playing a card with game logic and error checking and calling the update server gui
        :param client_id: int
        :param str_card: str - card obj in string format
        :return: None
        """

        if self.current_player is None or client_id != self.current_player.player_id:
            return

        lst_card = str_card.split("*")

        if len(lst_card) != 2:
            self.send_message(client_id, BAD_CARD_MSG)
            return
        print("_____________", client_id, "_________")
        suit, rank = lst_card
        if suit not in Suit.__members__ or rank not in Rank.__members__:
            self.send_message(client_id, BAD_CARD_MSG)

        card = Card(Suit[suit], Rank[rank])
        valid, round_over_team = self.game.play_card(self.current_player, card)

        if not valid:
            self.send_message(client_id, BAD_PLAY_MSG)
            return

        self.send_message(client_id, "ok")

        self.played_cards_dict[client_id] = str_card

        if round_over_team:
            game_state = self.game.get_game_state()
            scores = game_state.scores

            self.send_all(f"round_winner:{round_over_team},scores:{list_to_str([f'{team}*{score}' for team, score in scores.items()])}")

            self.update_server_gui()
            self.played_cards_dict = {1: "", 2: "", 3: "", 4: ""}

            if self.game.game_over:
                self.handle_game_over()
                return
        else:
            self.update_server_gui()
            pass

        time.sleep(DELAY_BETWEEN_TURNS_IN_SEC*2)

        # start new turn
        self.start_turn()

    def handle_new_player(self, client_id):
        """
        handling new player joining the game
        :param client_id: int
        :return: None
        """

        p = Player()
        self.players.append(p)

        self.send_message(client_id, f"client_id:{client_id}")

        if len(self.players) == 4:
            self.start_game()

    def start_game(self):
        """
        starts the game when 4 clients are connected, sending cards and deciding ruler
        :return: None
        """

        team1 = Team(self.players[:2])
        team2 = Team(self.players[2:])

        self.game = Game(self.players, [team1, team2])

        ruler = random.choice(self.game.players)
        self.game.set_ruler(ruler)

        self.send_all(f"ruler:{ruler.player_id}")

        self.game.hand_cards_for_all()

        # sends cards for all players in format: suit*rank|suit*rank...
        for player in self.game.players:
            self.send_message(player.player_id, list_to_str(player.hand))

    def handle_player_disconnect(self, client_id):
        """
        closing the game server when a player disconnect because the game cant continue and sending the players the reason
        :param client_id: int
        :return: None
        """

        self.send_all(f"PLAYER_DISCONNECTED")
        self.run = False

    def handle_game_over(self):
        """
        sending the players the game is over and closing the server
        :return:
        """

        self.send_all(f"GAME_OVER")
        self.run = False

    def update_server_gui(self):
        """
        sending the the server gui the status of the game so it can show the status
        :return: None
        player format: card|card|card...(all cards)-[played card on board]
        format: player1|player2|player3|player4,score of player 1+3|score of player 2+4 -- numbers are not id nums
        """
        score_str = f"{self.game.teams[0].points}|{self.game.teams[1].points}"

        player1_id1_str = f"{list_to_str(self.game.players[0].hand, '+')}-{self.played_cards_dict[1]}"
        player2_id3_str = f"{list_to_str(self.game.players[2].hand, '+')}-{self.played_cards_dict[3]}"
        player3_id2_str = f"{list_to_str(self.game.players[1].hand, '+')}-{self.played_cards_dict[2]}"
        player4_id4_str = f"{list_to_str(self.game.players[3].hand, '+')}-{self.played_cards_dict[4]}"

        players_str = f"{player1_id1_str}|{player2_id3_str}|{player3_id2_str}|{player4_id4_str}"

        self.send_to_server_gui(f"{players_str}")


a = Handler()
a.start()