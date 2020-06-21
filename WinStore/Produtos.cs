using C1.Win.C1Input;
using Dapper;
using DLL.BLL.Models;
using DLL.BLL.Services.ExtensionsMethods;
using DLL.DAL.Repository.Unit_Of_Work.Interfaces;
using Newtonsoft.Json;
using Syncfusion.DataSource.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinStore.Configuracao;
using WinStore.Unit_Of_Work;
using Proc = DLL.DAL.Repository.Database.Procedures.ProceduresSQL;
using CRUD = DLL.BLL.Models.Enums.CRUD;

namespace WinStore
{
    public partial class Produtos : Form
    {
        private static IUnitOfWork _unitOfWork;
        private Produto produto;
        private CRUD acao;

        public Produtos()
        {
            _unitOfWork = Inicializar.UnitOfWork;
            _unitOfWork.Use(x =>
            {
                x.ProdutoRepository.SetUnitOfWork(x);
                x.CategoriaRepository.SetUnitOfWork(x);
            });
            InitializeComponent();
        }

        private void Produtos_Load(object sender, EventArgs e)
        {
            txtCancelar_Click(e, e);
            LimparCampos();
            acao = CRUD.Visualizar;
            _unitOfWork.Use(x =>
            {
                var produtos = x.ProdutoRepository.ObterTodos(Proc.Produto, new { ID = 0 });
                produtos.ForEach(y => y.Categoria = x.CategoriaRepository.ObterId(Proc.Categoria, new { ID = y.CategoriaId }));
                grdCategorias.DataSource = produtos;
            });


            cboCategorias.Use(x =>
            {
                PreencherCombo();
                x.SelectedIndex = -1;
                x.Text = "Selecione";
            });
        }

        private void txtCadastrar_Click(object sender, EventArgs e)
        {
            if (!acao.Equals(CRUD.Gravar))
            {
                txtAtualizar.Enabled = false;
                txtRemover.Enabled = false;
                acao = CRUD.Cadastrar;
                LimparCampos();
                return;
            }

            try
            {
                CriarProduto();

                if (ModelState.IsValid(produto))
                {
                    _unitOfWork.Use(x =>
                    {
                        x.Begin();
                        x.ProdutoRepository.Cadastrar(Proc.Produto, produto);
                        x.Commit();
                    });
                    MessageBox.Show("Tudo Deu Certo", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(string.Join("\n", ModelState.ErrorMessages.ToArray()), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Produtos_Load(e, e);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grdCategorias_DoubleClick(object sender, EventArgs e)
        {
            if (grdCategorias.Rows.Count > 0)
            {
                _unitOfWork.Use(x =>
                {
                    produto = x.ProdutoRepository.ObterId(Proc.Produto, new { ID = grdCategorias[grdCategorias.RowSel, "ID"] });
                    produto.Categoria = x.CategoriaRepository.ObterId(Proc.Categoria, new { ID = produto.CategoriaId });

                    cboCategorias.SelectedIndex = cboCategorias.FindStringExact(produto.Categoria.Nome);
                    PreencherCombo();
                });
                PreecherCampos();
            }
        }

        private void txtAtualizar_Click(object sender, EventArgs e)
        {
            if (!acao.Equals(CRUD.Gravar))
            {
                txtRemover.Enabled = false;
                txtCadastrar.Enabled = false;
                acao = CRUD.Atualizar;
                return;
            }
            try
            {
                CriarProduto();

                if (ModelState.IsValid(produto))
                {
                    _unitOfWork.Use(x =>
                    {
                        x.Begin();
                        x.ProdutoRepository.Atualizar(Proc.Produto, produto);
                        x.Commit();

                        MessageBox.Show("Tudo Deu Certo", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    });

                }
                else
                {
                    MessageBox.Show(string.Join("\n", ModelState.ErrorMessages.ToArray()), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Produtos_Load(e, e);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtRemover_Click(object sender, EventArgs e)
        {
            if (!acao.Equals(CRUD.Gravar))
            {
                txtCadastrar.Enabled = false;
                txtAtualizar.Enabled = false;
                acao = CRUD.Excluir;
                return;
            }

            if (grdCategorias.Rows.Count > 0)
            {
                if (MessageBox.Show("Deseja Apagar", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    _unitOfWork.Use(x =>
                    {
                        x.Begin();
                        x.ProdutoRepository.Excluir(Proc.Produto, new { ID = txtId.Text });
                        x.Commit();
                    });

                    MessageBox.Show("Deu tudo Certo", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Produtos_Load(e, e);

            }
        }

        private void LimparCampos()
        {
            txtId.Text = "";
            txtNome.Text = "";
            txtDescricao.Text = "";
            txtImageUrl.Text = "";
            txtImageThubnailsUrl.Text = "";
            txtPreco.Text = "";
            chkAVenda.Checked = false;
            chkEstoque.Checked = false;
        }

        private void PreencherCombo()
        {
            cboCategorias.Use(x =>
            {
                x.DataSource = _unitOfWork.CategoriaRepository.ObterTodos(Proc.Categoria, new { ID = 0 });
                x.DisplayMember = "Nome";
                x.ValueMember = "Id";

            });
        }

        private void CriarProduto()
        {
            produto.Use(x =>
            {

                x.ID = string.IsNullOrEmpty(txtId.Text) ? 0 : Convert.ToInt32(txtId.Text);
                x.Nome = txtNome.Text;
                x.Descricao = txtDescricao.Text;
                x.Preço = Convert.ToDecimal(txtPreco.Text);
                x.ImageUrl = txtImageUrl.Text;
                x.ImageThumbnailUrl = txtImageThubnailsUrl.Text;
                x.EmEstoque = chkEstoque.Checked;
                x.AVenda = chkAVenda.Checked;
                x.CategoriaId = Convert.ToInt32(cboCategorias.SelectedValue);
                x.Categoria = null;
            });
        }

        private void PreecherCampos()
        {
            txtId.Text = produto.ID.ToString();
            txtNome.Text = produto.Nome;
            txtDescricao.Text = produto.Descricao;
            txtImageUrl.Text = produto.ImageUrl;
            txtImageThubnailsUrl.Text = produto.ImageThumbnailUrl;
            txtPreco.Text = produto.Preço.ToString("c");
            chkAVenda.Checked = produto.AVenda;
            chkEstoque.Checked = produto.EmEstoque;
        }
        private void btnGravar_Click(object sender, EventArgs e)
        {

            switch (acao)
            {
                case CRUD.Cadastrar:
                    acao = CRUD.Gravar;
                    txtCadastrar_Click(e, e);
                    break;
                case CRUD.Atualizar:
                    acao = CRUD.Gravar;
                    txtAtualizar_Click(e, e);
                    break;
                case CRUD.Excluir:
                    acao = CRUD.Gravar;
                    txtRemover_Click(e, e);
                    break;
                default:
                    acao = CRUD.Visualizar;
                    MessageBox.Show("Escolha uma Opção", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }

        private void txtCancelar_Click(object sender, EventArgs e)
        {
            txtCadastrar.Enabled = true;
            txtAtualizar.Enabled = true;
            txtRemover.Enabled = true;
            acao = CRUD.Visualizar;
        }
    }
}
