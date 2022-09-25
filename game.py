from base_classes import *
class RoundState:
    def __init__(self,suit:Suit):
        self.played_suit = suit
        self.played_cards = []
    

class GameState:
    def __init__(self, s_suit:Suit, teams:list[Team]):
        self.s_suit = s_suit
        self.teams = teams
        self.scores = {team: 0 for team in teams}







def play_card(player:Player,card:Card) -> tuple(bool,bool):
    try:
        player.hand.index(card) 
    except ValueError as e:
        print(e)
        return (False,False)

