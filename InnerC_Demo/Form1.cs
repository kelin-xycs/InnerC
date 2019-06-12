using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using InnerC;

namespace InnerC_Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnTest_Click(object sender, EventArgs e)
        {
            try
            {
                Compiler compiler = new Compiler();

                compiler.Compile(txtSrcFile.Text);

                WriteMessage("编译成功，并将编译生成的语法成员逆向还原为源代码 。");
            }
            catch(Exception ex)
            {
                WriteMessage(ex.ToString());
            }
            
        }

        private void WriteMessage(string msg)
        {
            txtMsg.AppendText(msg + "\r\n\r\n");
        }
    }
}
