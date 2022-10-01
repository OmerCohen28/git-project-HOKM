from socket import *
from base_classes import *
from select import select
from game import game
import json
import random
import time


'''
TODO:
    finish game functions + documantation
    finish game flow comment
    update ALL json to work as specified in test.py
    test all functions together in a demo game
'''

class handler:
    def __init__(self, player_socket_dict:dict[Player:socket]) -> None:
        self.player_socket_dict = player_socket_dict
        self.conn_sock = socket(AF_INET,SOCK_STREAM)
        self.conn_sock.bind((gethostbyname(gethostname()),50500))
        self.conn_sock.listen(2)
        players = list(player_socket_dict.keys())
        self.game = game(players,[Team([players.pop(1-i) for i in range(2)]),Team([players.pop(1-i) for i in range(2)])])
 
    '''
    function that goes through each player handing them their cards and sending their new player object back to them
    '''
    def send_cards(self):
        for player in self.game.players:
            self.game.hand_cards_to_player(player)
            self.player_socket_dict[player].send(json.dumps("getting cards"))
            self.player_socket_dict[player].send(json.dumps(player))
    
    '''
    picks the ruler of the game randomly
    '''
    def decide_ruler(self):
        ruler = random.choice(self.game.players)
        self.game.set_ruler(ruler)

    '''
    asks the ruler what is the game's strong suit and updates it, call this function only after calling 'send_cards()' once
    expectes to get an int representing the suit in return:
            SPADES = 1
            CLUBS = 2
            DIAMONDS = 3
            HEARTS = 4
    '''
    def decide_strong_suit(self):
        self.player_socket_dict[self.game.ruler].send(json.dumps("what is the strong suit?"))
        strong_suit = json.loads(self.player_socket_dict[self.game.ruler].recv(1054))
        self.game.set_strong_suit(Suit(int(strong_suit)))
    
    '''
    sends game and round state to all players, call after every action taken when game started
    slight pause between each send to give the player time to clear the buffer for the object
    '''
    def send_game_state(self):
        for player in self.game.players:
            self.player_socket_dict[player].send(json.dumps("game state"))
            time.sleep(1)
            self.player_socket_dict[player].send(json.dumps(self.game.get_game_state()))

    def send_round_state(self):
        for player in self.game.players:
            self.player_socket_dict[player].send(json.dumps("round state"))
            time.sleep(1)
            self.player_socket_dict[player].send(json.dumps(self.game.get_round_state()))

    def play_card(self):
        player_turn = self.game.get_current_player_turn()
        player_sock = self.player_socket_dict[player_turn]
        player_sock.send(json.dumps("play card"))
        

ser = server({Player():1,Player():2,Player():3,Player():4})

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