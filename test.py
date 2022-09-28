from platform import node
from linked_list import *

llist = LinkedList()
llist.head = Node(1)
node1 = Node(2)
node2 = Node(3)
llist.head.next = node1
node1.next = node2
node2.next = llist.head


tmp = llist.head

while True:
    print(tmp.data)
    tmp = tmp.next