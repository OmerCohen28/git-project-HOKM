from base_classes import *


class game:

    def __init__(self,players:list[Player],teams:list[Team]):
        self.deck = Deck()
        self.players = players
        self.teams = teams
        self.game_state = GameState(None, self.teams)
        self.round_state = RoundState(None)
        self.winner_tup = (None, None)
        self.game_over = False

    # takes a player and a card, returns (is turn valid, is round over)
    def play_card(self,player:Player,card:Card) -> tuple(bool,bool):
        try:
            player.hand.index(card) #checking wether the player has the cards he wants to play
        except ValueError as e:
            print(e)
            return (False, False)
        if self.round_state.played_suit == None:
            self.round_state.played_suit = card.suit
            self.winner_tup = (card, player)
        elif self.round_state.played_suit != card.suit:
            if self.round_state.played_suit in [c.suit for c in player.hand]:
                print("card doesn't match round suit while player has matching card")
                return (False, False)
        
        self.round_state.played_cards.append(card)
        self.change_winner(card,player)
        if len(self.round_state.played_cards) == 4:
            self.increment_points()
            self.round_state.played_cards = []
            return (True, True)
        else:
            return (True, False)
    
    def increment_points(self):
        for team in self.teams:
            for player in team.players:
                if player.player_id == self.winner_tup[1].player_id:
                    self.game_over = team.add_points()

    def change_winner(self,card:Card,player:Player):
        if card.suit != self.round_state.played_suit and card.suit != self.strong_suit:
            return
        if self.winner_tup[0].suit == self.strong_suit:
            if card.suit == self.strong_suit:
                if card.rank > self.winner_tup[0].rank:
                    self.winner_tup = (card,player)
            else:
                return
        else:
            if card.suit == self.strong_suit:
                self.winner_tup = (card,player)
            else:
                if card.rank > self.winner_tup[0].rank:
                    self.winner_tup = (card,player)
            


    def set_strong_suit(self,suit:Suit):
        self.strong_suit = suit
        self.game_state.s_suit = suit
        

    def set_ruler(self,player:Player):
        self.ruler = player

    def hand_cards_to_player(self,player:Player):
        if len(player.hand)==0:
            for i in range(5):
                player.hand.append(self.deck.draw_card())
        elif len(player.hand) == 5:
            for i in range(8):
                player.hand.append(self.deck.draw_card())
        '''
        if player doesnt have cards, deal 5, if player does have cards, deal another 8
        '''

    def get_round_state(self) -> RoundState:
        return self.round_state
        
    def get_game_state(self) -> GameState:
        return self.game_state

    def get_next_player_turn(self) -> Player:
        pass
        '''
        return the player's instance that has to play next
        
        keeping track of turns will be done via linked list
        '''


