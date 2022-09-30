import socket
from select import select


class Server:
    def __init__(self, ip, port):
        """
        setting up the class of the base server which handles the socket level
        :param ip: str - server ip to bind
        :param port: int - server port to bind
        """
        self.__ip = ip
        self.__port = port

        self.__clients = []
        self.__client_ids = {}
        self.__messages_to_send = []
        self.__setup_socket()

    def __setup_socket(self):
        """
        setting up the server socket object
        :return: None
        """
        self.__server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        self.__server_socket.bind((self.__ip, self.__port))

    def start(self):
        """
        starting the server's socket and mainloop
        :return: None
        """
        self.__server_socket.listen()

        self.__main_loop()

    def close(self):
        """
        closing server socket
        :return: None
        """
        print(f"[SERVER] server closed")
        self.__server_socket.close()

    def __close_client(self, client):
        """
        closing connection to a client
        :param client: socket - client socket object
        :return: None
        """
        print(f"[SERVER] {client.getpeername()} disconnected")
        self.__clients.remove(client)
        client.close()

    def send_message(self, client_id, msg):
        """
        adding message that need to be sent to the message list
        :param client_id: int - id of a player client
        :param msg: str
        :return: None
        """

        client_sock = [sock for sock, _id in self.__client_ids.items() if _id == client_id]
        client_sock = client_sock[0]

        self.__messages_to_send.append((client_sock, msg))

    def _handle_data(self, client_id, msg, msg_type="data"):
        """
        method to be overwritten by handler class
        :return: True or None if server need to be closed
        """
        # example - echo and not closing the server
        if msg_type == "data":
            self.send_message(client_id, msg)
        return False

    def __main_loop(self):
        """
        server main loop that handles socket with select
        :return: None
        """
        print("server started")
        run = True
        # main server loop
        while run:
            rlist, wlist, _ = select(self.__clients + [self.__server_socket], self.__clients, [])

            # handling readable sockets
            for sock in rlist:
                # handling new client
                if sock is self.__server_socket:
                    try:
                        new_client, addr = self.__server_socket.accept()
                    except:
                        self.close()
                        return
                    print(f"[SERVER] new connection from {addr}")
                    self.__clients.append(new_client)
                    self.__client_ids[new_client] = len(self.__clients)

                    # maybe add after handler class is ready
                    # self._handle_data(len(self.__clients), "", msg_type="new_client")

                # handling client request
                else:
                    msg, success = self.__recv_from_socket(sock)

                    if not success:
                        self.__close_client(sock)
                        # maybe add after handler class is ready
                        # self._handle_data(self.__client_ids[sock], "", msg_type="client_disconnected")
                        continue

                    out = self._handle_data(self.__client_ids[sock], msg)
                    if out is True:
                        self.close()
                        return

            self.__send_messages(wlist)

    def __send_messages(self, wlist):
        """
        this function sends the clients messages that are waiting to be sent by the wanted format
        :param wlist: list[socket] - list of sockets that can be send to
        :return: None
        """
        for message in self.__messages_to_send:

            client, data = message

            if client not in self.__clients:
                self.__messages_to_send.remove(message)
                continue
            if client in wlist:
                try:
                    client.send(str(len(data.encode())).zfill(8).encode() + data.encode())
                except:
                    pass

                self.__messages_to_send.remove(message)

    def __recv_from_socket(self, sock):
        """
        function that receive data from socket by the wanted format
        :param sock: socket
        :return: tuple - (msg/error - str, status(True for ok, False for error))
        """
        try:
            msg_size = sock.recv(8)
        except:
            return "recv error", False
        if not msg_size:
            return "msg length error", False
        try:
            msg_size = int(msg_size)
        except:  # not an integer
            return "msg length error", False

        msg = b''
        while len(msg) < msg_size:  # this is a fail - safe -> if the recv not giving the msg in one time
            try:
                msg_fragment = sock.recv(msg_size - len(msg))
            except:
                return "recv error", False
            if not msg_fragment:
                return "msg data is none", False
            msg = msg + msg_fragment

        msg = msg.decode(errors="ignore")

        return msg, True


# for testing purposes
# if __name__ == "__main__":
    # s = Server("0.0.0.0", 55555)
    # s.start()
