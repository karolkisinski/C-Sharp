﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;

namespace TCPIP
{
    public partial class Form1 : Form
    {
        TcpClient clientSocket = new TcpClient();
        NetworkStream serverStream = default(NetworkStream);
        string readdata = null;


        


        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clientSocket.Connect(ip.Text, Int32.Parse(port.Text));
            Thread ctThread = new Thread(getMessage);
            ctThread.Start();
        }
        private void getMessage()
        {
            string returndata;
            while (true)
            {
                serverStream = clientSocket.GetStream();
                var buffsize = clientSocket.ReceiveBufferSize;
                byte[] inStream = new byte[buffsize];
                serverStream.Read(inStream, 0, buffsize);

                returndata = System.Text.Encoding.ASCII.GetString(inStream);
                readdata = returndata;

                msg();
            }
        }
        private void msg()
        {
            if(this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(msg));
            }
            else
            {
                textBox4.Text = readdata;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] outstream = Encoding.ASCII.GetBytes(textBox3.Text);

            serverStream.Write(outstream, 0, outstream.Length);
            serverStream.Flush();
        }
    }
}
