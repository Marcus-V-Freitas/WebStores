using DLL.BLL.Models;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Proc = DLL.DAL.Repository.Database.Procedures.ProceduresSQL;
using DLL.DAL.Repository.Contracts;
using DLL.DAL.Repository.Unit_Of_Work.Interfaces;
using DLL.DAL.Repository.Unit_Of_Work.Contracts;
using DLL.DAL.Repository.Interfaces;
using DLL.DAL.Repository.Database;
using WinStore.Unit_Of_Work;
using System.Linq;
using System;
using DLL.BLL.Services.ExtensionsMethods;
using WinStore.Configuracao;
using Microsoft.AspNetCore.Mvc;

namespace WinStore
{
    public partial class Categorias : Form
    {

        private static IUnitOfWork _unitOfWork;

        public Categorias()
        {
            _unitOfWork = Inicializar.UnitOfWork;
            InitializeComponent();
            _unitOfWork.CategoriaRepository.SetUnitOfWork(_unitOfWork);
        }

        private void Categorias_Load(object sender, System.EventArgs e)
        {
            grdCategorias.DataSource = _unitOfWork.CategoriaRepository.ObterTodos(Proc.Categoria, new { ID = 0 }).ToList();
        }

        private void txtCadastrar_Click(object sender, System.EventArgs e)
        {

            var categoria = new Categoria() { Nome = txtNome.Text, Descricao = txtDescricao.Text };

            if (ModelState.IsValid(categoria))
            {
                _unitOfWork.Begin();

                _unitOfWork.CategoriaRepository.Cadastrar(Proc.Categoria, categoria);
                _unitOfWork.Commit();
                MessageBox.Show("Deu tudo Certo", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Categorias_Load(e, e);
            }
            else
            {
                MessageBox.Show(string.Join("\n", ModelState.ErrorMessages.ToArray()), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void grdCategorias_DoubleClick(object sender, EventArgs e)
        {
            if (grdCategorias.Rows.Count > 0)
            {
                var categoria = _unitOfWork.CategoriaRepository.ObterId(Proc.Categoria, new { ID = grdCategorias[grdCategorias.RowSel, "ID"] });

                txtId.Text = categoria.Id.ToString();
                txtNome.Text = categoria.Nome;
                txtDescricao.Text = categoria.Descricao;

            }
        }

        private void txtAtualizar_Click(object sender, EventArgs e)
        {
            var categoria = new Categoria() { Id = Convert.ToInt32(txtId.Text), Nome = txtNome.Text, Descricao = txtDescricao.Text };

            if (ModelState.IsValid(categoria))
            {
                _unitOfWork.Begin();

                _unitOfWork.CategoriaRepository.Atualizar(Proc.Categoria, categoria);
                _unitOfWork.Commit();
                MessageBox.Show("Deu tudo Certo", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(string.Join("\n", ModelState.ErrorMessages.ToArray()), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Categorias_Load(e, e);
        }

        private void txtRemover_Click(object sender, EventArgs e)
        {
            if (grdCategorias.Rows.Count > 0)
            {
                if (MessageBox.Show("Deseja Apagar", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    _unitOfWork.Begin();

                    _unitOfWork.CategoriaRepository.Excluir(Proc.Categoria, new { ID = txtId.Text });
                    _unitOfWork.Commit();

                    MessageBox.Show("Deu tudo Certo", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Categorias_Load(e, e);

            }
        }
    }
}

