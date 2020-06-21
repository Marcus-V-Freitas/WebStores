using DLL.BLL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinStore.Unit_Of_Work;

namespace WinStore
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new Categorias().Show();
        }

        private void tsmProdutos_Click(object sender, EventArgs e)
        {
            new Produtos().Show();
        }
    }
}
