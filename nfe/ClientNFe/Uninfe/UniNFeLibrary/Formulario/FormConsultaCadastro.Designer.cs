namespace UniNFeLibrary.Formulario
{
    partial class FormConsultaCadastro
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConsultaCadastro));
            this.textConteudo = new System.Windows.Forms.MaskedTextBox();
            this.rbIE = new System.Windows.Forms.RadioButton();
            this.rbCPF = new System.Windows.Forms.RadioButton();
            this.rbCNPJ = new System.Windows.Forms.RadioButton();
            this.buttonPesquisa = new System.Windows.Forms.Button();
            this.comboUf = new System.Windows.Forms.ComboBox();
            this.lblUF = new System.Windows.Forms.Label();
            this.buttonStatusServidor = new System.Windows.Forms.Button();
            this.textResultado = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbEmissao = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblEmpresa = new System.Windows.Forms.Label();
            this.cbEmpresa = new System.Windows.Forms.ComboBox();
            this.lblAmbiente = new System.Windows.Forms.Label();
            this.cbAmbiente = new System.Windows.Forms.ComboBox();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textConteudo
            // 
            this.textConteudo.Location = new System.Drawing.Point(4, 118);
            this.textConteudo.Mask = "00,000,000/0000-00";
            this.textConteudo.Name = "textConteudo";
            this.textConteudo.Size = new System.Drawing.Size(256, 20);
            this.textConteudo.TabIndex = 4;
            this.textConteudo.TextChanged += new System.EventHandler(this.maskedTextBox1_TextChanged);
            // 
            // rbIE
            // 
            this.rbIE.AutoSize = true;
            this.rbIE.Location = new System.Drawing.Point(134, 101);
            this.rbIE.Name = "rbIE";
            this.rbIE.Size = new System.Drawing.Size(41, 17);
            this.rbIE.TabIndex = 2;
            this.rbIE.Text = "I.E.";
            this.rbIE.UseVisualStyleBackColor = true;
            this.rbIE.CheckedChanged += new System.EventHandler(this.rbIE_CheckedChanged);
            // 
            // rbCPF
            // 
            this.rbCPF.AutoSize = true;
            this.rbCPF.Location = new System.Drawing.Point(73, 101);
            this.rbCPF.Name = "rbCPF";
            this.rbCPF.Size = new System.Drawing.Size(45, 17);
            this.rbCPF.TabIndex = 1;
            this.rbCPF.Text = "CPF";
            this.rbCPF.UseVisualStyleBackColor = true;
            this.rbCPF.CheckedChanged += new System.EventHandler(this.rbCPF_CheckedChanged);
            // 
            // rbCNPJ
            // 
            this.rbCNPJ.AutoSize = true;
            this.rbCNPJ.Checked = true;
            this.rbCNPJ.Location = new System.Drawing.Point(4, 101);
            this.rbCNPJ.Name = "rbCNPJ";
            this.rbCNPJ.Size = new System.Drawing.Size(52, 17);
            this.rbCNPJ.TabIndex = 0;
            this.rbCNPJ.TabStop = true;
            this.rbCNPJ.Text = "CNPJ";
            this.rbCNPJ.UseVisualStyleBackColor = true;
            this.rbCNPJ.CheckedChanged += new System.EventHandler(this.rbCNPJ_CheckedChanged);
            // 
            // buttonPesquisa
            // 
            this.buttonPesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPesquisa.Location = new System.Drawing.Point(145, 145);
            this.buttonPesquisa.Name = "buttonPesquisa";
            this.buttonPesquisa.Size = new System.Drawing.Size(75, 23);
            this.buttonPesquisa.TabIndex = 5;
            this.buttonPesquisa.Text = "Pesquisar";
            this.buttonPesquisa.UseVisualStyleBackColor = true;
            this.buttonPesquisa.Click += new System.EventHandler(this.buttonPesquisaCNPJ_Click);
            // 
            // comboUf
            // 
            this.comboUf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboUf.FormattingEnabled = true;
            this.comboUf.Location = new System.Drawing.Point(8, 104);
            this.comboUf.Name = "comboUf";
            this.comboUf.Size = new System.Drawing.Size(258, 21);
            this.comboUf.TabIndex = 0;
            // 
            // lblUF
            // 
            this.lblUF.AutoSize = true;
            this.lblUF.Location = new System.Drawing.Point(8, 88);
            this.lblUF.Name = "lblUF";
            this.lblUF.Size = new System.Drawing.Size(246, 13);
            this.lblUF.TabIndex = 954;
            this.lblUF.Text = "Unidade Federativa (UF) para o envio da consulta:";
            // 
            // buttonStatusServidor
            // 
            this.buttonStatusServidor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStatusServidor.Location = new System.Drawing.Point(145, 145);
            this.buttonStatusServidor.Name = "buttonStatusServidor";
            this.buttonStatusServidor.Size = new System.Drawing.Size(75, 23);
            this.buttonStatusServidor.TabIndex = 2;
            this.buttonStatusServidor.Text = "Consultar";
            this.buttonStatusServidor.UseVisualStyleBackColor = true;
            this.buttonStatusServidor.Click += new System.EventHandler(this.buttonStatusServidor_Click);
            // 
            // textResultado
            // 
            this.textResultado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textResultado.Location = new System.Drawing.Point(8, 233);
            this.textResultado.Multiline = true;
            this.textResultado.Name = "textResultado";
            this.textResultado.ReadOnly = true;
            this.textResultado.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textResultado.Size = new System.Drawing.Size(365, 147);
            this.textResultado.TabIndex = 9;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 383);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(384, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(369, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "sssssssssssssssssssss";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(6, 215);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 13);
            this.lblStatus.TabIndex = 12;
            this.lblStatus.Text = "Status:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Tipo de Emissão:";
            // 
            // cbEmissao
            // 
            this.cbEmissao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbEmissao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEmissao.FormattingEnabled = true;
            this.cbEmissao.Location = new System.Drawing.Point(4, 118);
            this.cbEmissao.Name = "cbEmissao";
            this.cbEmissao.Size = new System.Drawing.Size(288, 21);
            this.cbEmissao.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 8);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(384, 206);
            this.tabControl1.TabIndex = 955;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.cbEmissao);
            this.tabPage1.Controls.Add(this.buttonStatusServidor);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(376, 177);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Consulta status do serviço";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textConteudo);
            this.tabPage2.Controls.Add(this.buttonPesquisa);
            this.tabPage2.Controls.Add(this.rbIE);
            this.tabPage2.Controls.Add(this.rbCPF);
            this.tabPage2.Controls.Add(this.rbCNPJ);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(376, 177);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Consulta cadastro do contribuinte";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.AutoSize = true;
            this.lblEmpresa.Location = new System.Drawing.Point(8, 39);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Size = new System.Drawing.Size(227, 13);
            this.lblEmpresa.TabIndex = 956;
            this.lblEmpresa.Text = "Utilizar para consulta o certificado da empresa:";
            // 
            // cbEmpresa
            // 
            this.cbEmpresa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEmpresa.FormattingEnabled = true;
            this.cbEmpresa.Location = new System.Drawing.Point(8, 57);
            this.cbEmpresa.Name = "cbEmpresa";
            this.cbEmpresa.Size = new System.Drawing.Size(365, 21);
            this.cbEmpresa.TabIndex = 957;
            this.cbEmpresa.SelectedIndexChanged += new System.EventHandler(this.cbEmpresa_SelectedIndexChanged);
            // 
            // lblAmbiente
            // 
            this.lblAmbiente.AutoSize = true;
            this.lblAmbiente.Location = new System.Drawing.Point(273, 88);
            this.lblAmbiente.Name = "lblAmbiente";
            this.lblAmbiente.Size = new System.Drawing.Size(54, 13);
            this.lblAmbiente.TabIndex = 3;
            this.lblAmbiente.Text = "Ambiente:";
            // 
            // cbAmbiente
            // 
            this.cbAmbiente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAmbiente.FormattingEnabled = true;
            this.cbAmbiente.Location = new System.Drawing.Point(273, 104);
            this.cbAmbiente.Name = "cbAmbiente";
            this.cbAmbiente.Size = new System.Drawing.Size(100, 21);
            this.cbAmbiente.TabIndex = 4;
            // 
            // FormConsultaCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 405);
            this.Controls.Add(this.lblEmpresa);
            this.Controls.Add(this.cbEmpresa);
            this.Controls.Add(this.lblUF);
            this.Controls.Add(this.comboUf);
            this.Controls.Add(this.cbAmbiente);
            this.Controls.Add(this.lblAmbiente);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.textResultado);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 412);
            this.Name = "FormConsultaCadastro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consulta ao Servidor";
            this.Load += new System.EventHandler(this.FormConsultaCadastro_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormConsultaCadastro_FormClosed);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPesquisa;
        private System.Windows.Forms.Button buttonStatusServidor;
        private System.Windows.Forms.TextBox textResultado;
        private System.Windows.Forms.ComboBox comboUf;
        private System.Windows.Forms.Label lblUF;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.RadioButton rbIE;
        private System.Windows.Forms.RadioButton rbCPF;
        private System.Windows.Forms.RadioButton rbCNPJ;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbEmissao;
        private System.Windows.Forms.MaskedTextBox textConteudo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblEmpresa;
        private System.Windows.Forms.ComboBox cbEmpresa;
        private System.Windows.Forms.ComboBox cbAmbiente;
        private System.Windows.Forms.Label lblAmbiente;

    }
}