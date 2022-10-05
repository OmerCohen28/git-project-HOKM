from socket import *
from base_classes import *
from select import select
from game import game
import json
import random
import time


'''
TODO:
    change handler to class to work with server class
    integrate it into server
'''

class handler:
    def __init__(self, players:list[Player]) -> None:
        self.players = players
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
        


''''
game flow:
    *everyone connects*
    call send_cards() once
    call decide_ruler()
    call decite_strong_suit()
    call send_cards() second time
    call send_game_state()
    call send_round_state()
    (loop till game is done):
        call is_game_over()
        (loop till round is over):
            call play_card() //it is called for different player each time
            call send_round_state()
        call send_game_state()
'''