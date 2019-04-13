using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniNFeLibrary.Formulario
{
    public partial class FormWait : Form
    {
        public FormWait()
        {
            InitializeComponent();

            this.label1.Text = "Espere...";
        }

        public void DisplayMessage(string msg)
        {
            this.label1.Text = msg;
            this.label1.Update();
            Application.DoEvents();
        }
    }
}
