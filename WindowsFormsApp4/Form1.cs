using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp4.Instances;
using System.Net.Sockets;
using System.Net;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AfterInitializeComponent();
        }

        private void InitializeConnection() {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 55555);

            Socket s = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {

                // Connect Socket to the remote
                // endpoint using method Connect()
                s.Connect(localEndPoint);

                // We print EndPoint information
                // that we are connected
                Console.WriteLine("Socket connected to -> {0} ",
                              s.RemoteEndPoint.ToString());

                while (true)
                {

                    // Data buffer
                    byte[] l = new byte[8];
                    int length = s.Receive(l);
                    length = int.Parse(Encoding.ASCII.GetString(l, 0, length));
                    l = new byte[length];
                    int bMsg = s.Receive(l);
                    string cards = Encoding.ASCII.GetString(l, 0, bMsg);

                    AnalyzeCards(cards);

                }

            }

            // Manage of Socket's Exceptions
            catch (ArgumentNullException ane)
            {

                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }

            catch (SocketException se)
            {

                Console.WriteLine("SocketException : {0}", se.ToString());
            }

            catch (Exception e0)
            {
                Console.WriteLine("Unexpected exception : {0}", e0.ToString());
            }
        }

        private void AnalyzeCards(string cards)
        {
            try
            {
                string[] playerCards = cards.Split('|');
                Hand[] playerHands = new Hand[4];
                Card[] playerPlaced = new Card[4];
                for (int i = 0; i < playerCards.Length; i++)
                {
                    string[] options = playerCards[i].Split('-');
                    playerPlaced[i] = Card.GetCardFromStr(options[1]);
                    string[] handStr = options[0].Split('+');
                    Card[] handCards = new Card[handStr.Length];
                    for (int j = 0; j < handStr.Length; j++)
                    {
                        handCards[j] = Card.GetCardFromStr(handStr[j]);
                    }
                    playerHands[i] = new Hand(handCards);
                }

                this.UpdateCards(playerHands, playerPlaced);
            }
            catch (Exception e0) {
                Console.WriteLine("String Parsing exception: {0}", e0.ToString());
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeConnection();
        }

        public static void button1_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            Card card = ((Card)sender);
            card.Rank = rand.Next(1, 13);
        }
    }
}
