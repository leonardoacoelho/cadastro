﻿using AprendendoEF.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AprendendoEF.UI
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void menuClientes_Click(object sender, EventArgs e)
        {
            var form = new ListaClientesForm(this);
            form.Show();
        }

        private void menuSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuProdutos_Click(object sender, EventArgs e)
        {
            var form = new ListaProdutosForm(this);
            form.Show();
        }

        private void gruposDeProdutosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ListaGruposProdutos(this);
            form.Show();
        }

        private void menuVenda_Click(object sender, EventArgs e)
        {
            var form = new ListaVendasForm(this);
            form.Show();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ListaUsuariosForm(this);
            form.Show();
        }
    }
}
