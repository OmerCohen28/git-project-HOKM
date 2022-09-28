from base_classes import *


class game:

    def __init__(self,players:list[Player],teams:list[Team]):
        self.deck = Deck()
        self.players = players
        self.teams = teams


    def play_card(self,player:Player,card:Card) -> tuple(bool,bool):
        try:
            player.hand.index(card) 
        except ValueError as e:
            print(e)
            return (False,False)
    
    def set_strong_suit(self,suit:Suit):
        self.strong_suit = suit
        self.game_state = GameState(suit, self.teams)

    def set_ruler(self,player:Player):
        self.ruler = player

    def hand_cards_to_player(player:Player):
        pass
        '''
        if player doesnt have cards, deal 5, if player does have cards, deal another 8
        '''

    def get_round_state(self) -> RoundState:
        pass
        #return the round state of the game
        
    def get_game_state(self) -> GameState:
        pass
        # return gamestate

    def get_next_player_turn(self) -> Player:
        pass
        '''
        return the player's instance that has to play next
        
        keeping track of turns will be done via linked list
        '''


