from linked_list import *
from base_classes import *
import json


suit = Suit(1)
rank = Rank(3)
card = Card(suit,rank)
di = {"suit":card.suit.val,"rank":card.rank.val}
dumped = dict(eval(json.dumps(di)))
print(type(dumped))
print(dumped)

c = Card(Suit(dumped["suit"]),Rank(dumped["rank"]))
print(c.suit,c.rank)