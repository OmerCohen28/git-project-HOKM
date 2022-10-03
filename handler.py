from base_classes import *
from server import Server
from game import Game
import json
import random
import time


BAD_CARD_MSG = "bad_card"
BAD_PLAY_MSG = "bad_play"

DELAY_BETWEEN_TURNS_IN_SEC = 1.2


def list_to_str(lst):
    return "|".join([str(item) for item in lst])


class Handler(Server):
    def __init__(self, ip="0.0.0.0", port=55555):
        super().__init__(ip, port)

        self.players = []
        self.game = None
        self.current_player = None

    def _handle_data(self, client_id: int, msg: str, msg_type="data"):
        """
        function that handles the requests of the players and manages the game
        :param client_id: int - id of the player who sent the request
        :param msg: str - the request of the player
        :param msg_type: str - the type of request that the player sent
        :return: bool - True if the server need to be shut down and False otherwise
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
        if client_id != self.game.ruler.player_id:
            return

        if self.game.strong_suit is not None:
            return

        if suit not in Suit.__members__:
            self.send_message(client_id, BAD_CARD_MSG)
        else:
            self.game.set_strong_suit(Suit[suit])
            self.send_message(client_id, "ok")

            self.game.hand_cards_for_all()

            # sends remaining cards for all players in format: suit*rank|suit*rank...
            for player in self.game.players:
                self.send_message(player.player_id, list_to_str(player.hand))

            # format like this: "teams:1+3|2+4,strong:DIAMONDS"
            self.send_all(f"teams:{list_to_str(self.game.teams)},strong:{suit}")

            self.start_turn()

    def start_turn(self):
        player = self.game.get_current_player_turn()
        self.current_player = player

        round_status = self.game.get_round_state()

        msg = f"played_suit:{'' if round_status.played_suit is None else round_status.played_suit.name},played_cards:{list_to_str(round_status.played_cards)}"
        self.send_message(player.player_id, msg)

    def handle_play_card(self, client_id, str_card):
        if self.current_player is None or client_id != self.current_player.player_id:
            return

        str_card = str_card.split("*")

        if len(str_card) != 2:
            self.send_message(client_id, BAD_CARD_MSG)
            return

        suit, rank = str_card
        if suit not in Suit.__members__ or rank not in Rank.__members__:
            self.send_message(client_id, BAD_CARD_MSG)

        card = Card(Suit[suit], Rank[rank])

        valid, round_over_team = self.game.play_card(self.current_player, card)

        if not valid:
            self.send_message(client_id, BAD_PLAY_MSG)
            return

        self.send_message(client_id, "ok")

        if round_over_team:
            game_state = self.game.get_game_state()
            scores = game_state.scores

            self.send_all(f"round_winner:{round_over_team},scores:{list_to_str([f'{team}*{score}' for team, score in scores.items()])}")

            if self.game.game_over:
                self.handle_game_over()
                return

        time.sleep(DELAY_BETWEEN_TURNS_IN_SEC)

        # start new turn
        self.start_turn()

    def handle_new_player(self, client_id):
        p = Player()
        self.players.append(p)

        self.send_message(client_id, f"client_id:{client_id}")

        if len(self.players) == 4:
            self.start_game()

    def start_game(self):
        random.shuffle(self.players)
        team1 = Team(self.players[:2])
        team2 = Team(self.players[2:])

        self.game = Game(self.players, [team1, team2])

        ruler = random.choice(self.game.players)
        self.game.set_ruler(ruler)

        self.send_all(f"ruler:{ruler.player_id}")

        self.game.hand_cards_for_all()

        # sends cards for all players in format: suit*rank,suit*rank...
        for player in self.game.players:
            self.send_message(player.player_id, list_to_str(player.hand))

    def handle_player_disconnect(self, client_id):
        pass

    def handle_game_over(self):
        self.run = False



'''
get-game-state() //at start of game and after every round
decide-ruler() // start of game before handing cards
decide-strong-suit() // after handing cards once
take-cards() //2 times
player-played() //get new round state
you-play() //
game-over()
'''

'''
game flow:
    call send_cards() once
    call decide_ruler()
    call decite_strong_suit()
    call send_cards() second time
    call send_game_state()
    call send_round_state()

'''

''''
    *everyone connects*
    call send_cards() once
    call decide_ruler()
    call decite_strong_suit()
    call send_cards() second time
    call send_game_state()
    call send_round_state()


'''