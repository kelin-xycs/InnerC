using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InnerCTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            InnerC.CallFromDll("Hello World !");
        }

        private void WriteMsg(string msg)
        {
            txtMsg.AppendText(DateTime.Now.ToString("HH:mm:ss.fff") + "  " + msg + "\r\n");
        }
    }
}
