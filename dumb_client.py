from socket import *
import re
import random

class client:
    def __init__(self) -> None:
        self.sock = socket(AF_INET,SOCK_STREAM)
        self.sock.connect(("0.0.0.0", 55555))
        self.get_id_pattern = re.compile(r":([1-9])$")
        self.player_id = int(re.findall(string = self.sock.recv(1054).decode(), pattern= self.get_id_pattern)[0])
        self.hand = []
    
    def game_loop(self):
        self.ruler = int(re.findall(string = self.sock.recv(1054).decode(),pattern=self.get_id_pattern)[0])
        self.add_cards_to_hand()
        if self.ruler == self.player_id:
            self.set_strong_suit()
        self.add_cards_to_hand()
        self.get_strong_suit_and_teams()
        
        while True:
            self.play_turn()
            if self.handle_end_of_round():
                break


    def add_cards_to_hand(self):
        cards_string = self.sock.recv(1054).decode()
        cards = cards_string.split("|")    
        for card in cards:
            self.hand.append(card.split("*"))

    def set_strong_suit(self):
        suit = random.choice(self.hand)[0]
        self.sock.send(f"set_strong:{suit}".encode())
        if self.sock.recv(1054).decode() == "bad":
            self.set_strong_suit()
    
    def get_strong_suit_and_teams(self):
        data = self.sock.recv(1054).decode()
        self.strong_suit = re.findall(string = data,pattern=r"strong:(.+?)$")[0]
    
    def handle_end_of_round(self):
        for team_point in re.findall(string = self.sock.recv(1054).decode(), pattern= r"scores:(.+?)$")[0].split("|"):
            team,points = team_point.split("*")
            if points == 7:
                print("GAME OVER!!!")
                won = False
                for played_id in team.split("+"):
                    if played_id ==self.player_id:
                        won = True
                if won:
                    print("YOU WON!!!!")
                else:
                    print("you lost :(")
                return True
        return False

    def print_hand(self):
        print("my hand is:")
        for card in self.hand:
            print(f"suit: {card[0]}, rank: {card[1]}")
    
    def play_turn(self):
        self.print_hand()
        status = self.sock.recv(1054).decode()
        played_suit = re.findall(string = status, pattern= r"^played_suit:(.+?)")[0]
        cards_played = []
        while True:
            played = False
            if played_suit == "empty":
                cards_played.append(self.hand.pop(0))
                self.sock.send(f"play_card:{cards_played[-1][0]}*{cards_played[-1][1]}".encode())
                played= True
            if not played:
                for card in self.hand:
                    if card[0] == played_suit:
                        cards_played.append(self.hand.pop(self.hand.index(card)))
                        self.sock.send(f"play_card:{card[0]}*{card[1]}".encode())
                        played = True
            if not played:
                for card in self.hand:
                    if card[0] == self.strong_suit:
                        cards_played.append(self.hand.pop(self.hand.index(card)))
                        self.sock.send(f"play_card:{card[0]}*{card[1]}".encode())
                        played = True
            if not played:
                cards_played.append(self.hand.pop(0))
                self.sock.send(f"play_card:{cards_played[-1][0]}*{cards_played[-1][1]}".encode())
            
            server_respone = self.sock.recv(1054).decode()
            if server_respone == "ok":
                for i in range(len(cards_played)-1):
                    self.hand.append(cards_played[i])
                return

