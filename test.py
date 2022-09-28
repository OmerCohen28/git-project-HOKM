from linked_list import *

llist = LinkedList()
llist.head = Node(1)
node1 = Node(2)
node2 = Node(3)
llist.head.next = node1
node1.next = node2
node2.next = llist.head

hye = llist.head
print(hye.data)
hye = hye.next
hye = hye.next
hye = hye.next
print(hye.data)
