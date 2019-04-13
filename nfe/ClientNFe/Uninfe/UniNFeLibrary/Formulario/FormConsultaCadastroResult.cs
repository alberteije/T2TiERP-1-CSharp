using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UniNFeLibrary.Formulario
{
    public partial class FormConsultaCadastroResult : Form
    {
        private UniNFeLibrary.RetConsCad vConsCad = null;

        public FormConsultaCadastroResult(UniNFeLibrary.RetConsCad vConsCad)
        {
            InitializeComponent();
            this.vConsCad = vConsCad;
        }

        private void FormConsultaCadastroResult_Load(object sender, EventArgs e)
        {
            this.tabControl1.TabPages.Clear();
            for (int n = 0; n < vConsCad.infCad.Count; ++n)
            {
                this.tabControl1.TabPages.Add("Consulta " + (n + 1).ToString());

                ///
                /// cria um control do UserControl
                /// 
                retConsCad ctr = new retConsCad(vConsCad.infCad[n]);
                ctr.Dock = DockStyle.Fill;
                ctr.Parent = this.tabControl1.TabPages[n];

                this.tabControl1.TabPages[n].Controls.Add(ctr);
            }
            this.txtdhCons.Text = vConsCad.dhCons.ToString();
            this.txtxMotivo.Text = vConsCad.xMotivo;
            this.txtcStat.Text = vConsCad.cStat.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
