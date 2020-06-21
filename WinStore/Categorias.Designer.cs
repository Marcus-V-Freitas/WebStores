namespace WinStore
{
    partial class Categorias
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Categorias));
            this.grdCategorias = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.txtId = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.txtNome = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.txtDescricao = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.lblCategoriaTitulo = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtCadastrar = new C1.Win.C1Input.C1Button();
            this.txtAtualizar = new C1.Win.C1Input.C1Button();
            this.txtConsultar = new C1.Win.C1Input.C1Button();
            this.txtRemover = new C1.Win.C1Input.C1Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdCategorias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNome)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCadastrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAtualizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsultar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemover)).BeginInit();
            this.SuspendLayout();
            // 
            // grdCategorias
            // 
            this.grdCategorias.ColumnInfo = resources.GetString("grdCategorias.ColumnInfo");
            this.grdCategorias.Location = new System.Drawing.Point(30, 205);
            this.grdCategorias.Name = "grdCategorias";
            this.grdCategorias.Rows.Count = 1;
            this.grdCategorias.Rows.DefaultSize = 19;
            this.grdCategorias.Size = new System.Drawing.Size(407, 221);
            this.grdCategorias.TabIndex = 0;
            this.grdCategorias.DoubleClick += new System.EventHandler(this.grdCategorias_DoubleClick);
            // 
            // txtId
            // 
            this.txtId.Enabled = false;
            this.txtId.Location = new System.Drawing.Point(107, 78);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(100, 20);
            this.txtId.TabIndex = 1;
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(107, 114);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(100, 20);
            this.txtNome.TabIndex = 2;
            // 
            // txtDescricao
            // 
            this.txtDescricao.Location = new System.Drawing.Point(107, 155);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(100, 20);
            this.txtDescricao.TabIndex = 3;
            // 
            // lblCategoriaTitulo
            // 
            this.lblCategoriaTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategoriaTitulo.Location = new System.Drawing.Point(12, 13);
            this.lblCategoriaTitulo.Name = "lblCategoriaTitulo";
            this.lblCategoriaTitulo.Size = new System.Drawing.Size(146, 31);
            this.lblCategoriaTitulo.TabIndex = 4;
            this.lblCategoriaTitulo.Text = "Categorias";
            // 
            // autoLabel1
            // 
            this.autoLabel1.Location = new System.Drawing.Point(30, 78);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(43, 13);
            this.autoLabel1.TabIndex = 5;
            this.autoLabel1.Text = "Código:";
            // 
            // autoLabel2
            // 
            this.autoLabel2.Location = new System.Drawing.Point(30, 121);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(38, 13);
            this.autoLabel2.TabIndex = 6;
            this.autoLabel2.Text = "Nome:";
            // 
            // autoLabel3
            // 
            this.autoLabel3.Location = new System.Drawing.Point(30, 162);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(58, 13);
            this.autoLabel3.TabIndex = 7;
            this.autoLabel3.Text = "Descrição:";
            // 
            // txtCadastrar
            // 
            this.txtCadastrar.Location = new System.Drawing.Point(12, 478);
            this.txtCadastrar.Name = "txtCadastrar";
            this.txtCadastrar.Size = new System.Drawing.Size(75, 23);
            this.txtCadastrar.TabIndex = 8;
            this.txtCadastrar.Text = "Cadastrar";
            this.txtCadastrar.UseVisualStyleBackColor = true;
            this.txtCadastrar.Click += new System.EventHandler(this.txtCadastrar_Click);
            // 
            // txtAtualizar
            // 
            this.txtAtualizar.Location = new System.Drawing.Point(107, 478);
            this.txtAtualizar.Name = "txtAtualizar";
            this.txtAtualizar.Size = new System.Drawing.Size(75, 23);
            this.txtAtualizar.TabIndex = 9;
            this.txtAtualizar.Text = "Atualizar";
            this.txtAtualizar.UseVisualStyleBackColor = true;
            this.txtAtualizar.Click += new System.EventHandler(this.txtAtualizar_Click);
            // 
            // txtConsultar
            // 
            this.txtConsultar.Location = new System.Drawing.Point(221, 478);
            this.txtConsultar.Name = "txtConsultar";
            this.txtConsultar.Size = new System.Drawing.Size(75, 23);
            this.txtConsultar.TabIndex = 10;
            this.txtConsultar.Text = "Consultar";
            this.txtConsultar.UseVisualStyleBackColor = true;
            // 
            // txtRemover
            // 
            this.txtRemover.Location = new System.Drawing.Point(362, 478);
            this.txtRemover.Name = "txtRemover";
            this.txtRemover.Size = new System.Drawing.Size(75, 23);
            this.txtRemover.TabIndex = 11;
            this.txtRemover.Text = "Remover";
            this.txtRemover.UseVisualStyleBackColor = true;
            this.txtRemover.Click += new System.EventHandler(this.txtRemover_Click);
            // 
            // Categorias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 525);
            this.Controls.Add(this.txtRemover);
            this.Controls.Add(this.txtConsultar);
            this.Controls.Add(this.txtAtualizar);
            this.Controls.Add(this.txtCadastrar);
            this.Controls.Add(this.autoLabel3);
            this.Controls.Add(this.autoLabel2);
            this.Controls.Add(this.autoLabel1);
            this.Controls.Add(this.lblCategoriaTitulo);
            this.Controls.Add(this.txtDescricao);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.grdCategorias);
            this.Name = "Categorias";
            this.Text = "Categorias";
            this.Load += new System.EventHandler(this.Categorias_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdCategorias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNome)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCadastrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAtualizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsultar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemover)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid grdCategorias;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtId;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtNome;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtDescricao;
        private Syncfusion.Windows.Forms.Tools.AutoLabel lblCategoriaTitulo;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private C1.Win.C1Input.C1Button txtCadastrar;
        private C1.Win.C1Input.C1Button txtAtualizar;
        private C1.Win.C1Input.C1Button txtConsultar;
        private C1.Win.C1Input.C1Button txtRemover;
    }
}