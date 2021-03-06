﻿using AprendendoEF.BLL;
using AprendendoEF.UI.Base;
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
    public partial class CadastroProdutoForm : BaseForm
    {
        Produto _produto;

        ListaProdutosForm _lista;

        ProdutoBO bo;


        public CadastroProdutoForm(ListaProdutosForm lista) : base(lista)
        {
            InitializeComponent();
            bo = new ProdutoBO();
            _lista = lista;
        }

        public CadastroProdutoForm(ListaProdutosForm lista, Produto produto) : this(lista)
        {
            _produto = produto;
        }

        private void CadastroProdutoForm_Load(object sender, EventArgs e)
        {
            var grupoBO = new GrupoProdutoBO();
            var grupos = grupoBO.Listar();
            cbxGrupo.DataSource = grupos;

            if (_produto != null)
            {
                txtId.Text = _produto.Id.ToString();
                txtDescricao.Text = _produto.Descricao;
                cbxGrupo.SelectedIndex = grupos.IndexOf(grupos.FirstOrDefault(x => x.Id == _produto.GrupoProduto_Id));
                txtValor.Text = _produto.Valor.ToString();

                menuRemover.Visible = true;
            }
            else
                menuRemover.Visible = false;
        }

        private void menuGravar_Click(object sender, EventArgs e)
        {
            try
            {
                double valor = 0;
                double.TryParse(txtValor.Text, out valor);

                if (valor <= 0)
                    throw new ArgumentOutOfRangeException();

                _produto = new Produto
                {
                    Id = ObterId(),
                    Descricao = txtDescricao.Text,
                    GrupoProduto_Id = ((GrupoProduto)cbxGrupo.SelectedItem).Id,
                    Valor = Convert.ToDouble(txtValor.Text)
                };

                bo.Salvar(_produto);

                MessageBox.Show("Salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (string.IsNullOrEmpty(txtId.Text))
                {
                    LimparCampos();
                    txtDescricao.Focus();
                    _lista.AtualizaGrid();
                }
                else
                {
                    Hide();
                    _lista.AtualizaGrid();
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("O campo Descrição é obrigatório!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("O campo Valor deve ser maior que zero '0' ou de letras!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LimparCampos()
        {
            txtId.Text = "";
            txtDescricao.Text = "";
            cbxGrupo.SelectedIndex = -1;
            txtValor.Text = "";
        }

        private int ObterId()
        {
            return !string.IsNullOrEmpty(txtId.Text) ? Convert.ToInt32(txtId.Text) : 0;
        }

        private void menuCancelar_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void menuRemover_Click(object sender, EventArgs e)
        {
            try
            {
                var id = ObterId();

                if (id > 0)
                {
                    var result = MessageBox.Show($"Você tem certeza que deseja remover {txtDescricao.Text}?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        bo.Remover(id);

                        MessageBox.Show("Removido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LimparCampos();
                        _lista.AtualizaGrid();
                        Hide();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}

