using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

//Il faut croiser les adresses ip :
//Joueur 1 : 50 en haut et 51 en bas
//Joueur 2 : 51 en haut et 50 en bas

namespace snake
{
    public partial class NetworkConnection : Form
    {
        public Socket sock;
        public Thread receiver;
        IPEndPoint multiep = new IPEndPoint(IPAddress.Any, 9050);
        public String xml;
        String AddressReceiver, AddressSender;

        private EventWaitHandle EventStopThreadPlease;

        delegate void packetReceiveDelegate(String TextToDisplay);

        private void packetReceive(String TextToDisplay)
        {
            xml = TextToDisplay;
        }

        private void ThreadCounterEntryPoint()
        {
            EndPoint ep = (EndPoint)multiep;
            byte[] data = new byte[4096];
            int recv;

            while (!this.EventStopThreadPlease.WaitOne(0))
            {
                data = new byte[4096];
                recv = sock.ReceiveFrom(data, ref ep);

                packetReceive(Encoding.UTF8.GetString(data, 0, recv));
                //Thread.Sleep(500);
            }
        }

        public NetworkConnection(String AddressIPSender, String AddressIPReceiver)
        {
            this.AddressReceiver = AddressIPReceiver;
            this.AddressSender = AddressIPSender;

            if (sock==null || !sock.IsBound)
            {
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                IPEndPoint iep = new IPEndPoint(IPAddress.Any, 9050);
                sock.Bind(iep);
            }
            sock.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(IPAddress.Parse(this.AddressReceiver)));

            xml = "";

            this.EventStopThreadPlease = new EventWaitHandle(false, EventResetMode.AutoReset);
            //receiver = new Thread(new ThreadStart(() => { packetReceive(label); }));
            receiver = new Thread(new ThreadStart(ThreadCounterEntryPoint));

            receiver.IsBackground = true;
            receiver.Start();
        }

        public void sendInfo(string info)
        {
            this.sock.SendTo(Encoding.UTF8.GetBytes(info), info.Length, SocketFlags.None, new IPEndPoint(IPAddress.Parse(this.AddressSender), 9050));
        }

        
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.EventStopThreadPlease.Set();
            this.receiver.Join();
        }


    }
}
