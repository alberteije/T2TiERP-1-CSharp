using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace UniNFeLibrary.Formulario
{
    public partial class FormCNPJ : Form
    {
        public FormCNPJ(string nomeEmpresa)
        {
            InitializeComponent();
            this.edtEmpresa.Text = nomeEmpresa;
        }

        private void FormCNPJ_Load(object sender, EventArgs e)
        {
            this.edtCNPJ.Text = "";
        }

        private void FormCNPJ_Shown(object sender, EventArgs e)
        {
            this.edtCNPJ.Focus();
            this.button1.Enabled = false;
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.button1.Enabled = this.Cnpj != "";
        }

        public string Cnpj { get { return (string)Auxiliar.OnlyNumbers(this.edtCNPJ.Text, ".,-/"); } }

        private void button1_Click(object sender, EventArgs e)
        {
            this.edtCNPJ.Focus();

            if (CNPJ.Validate(this.Cnpj))
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            else
                MessageBox.Show("CNPJ inválido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
