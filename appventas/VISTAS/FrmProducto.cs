using appventas.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace appventas.VISTAS
{
    public partial class FrmProducto : Form
    {
        public FrmProducto()
        {
            InitializeComponent();
        }

        private void FrmProducto_Load(object sender, EventArgs e)
        {

        }
        void cargardatos()
        {
          var  ClsProducto = new ClsProducto();
            dataGridView1.Rows.Clear();

            foreach (var listarDatos in ClsProducto.cargarDatosProductoFiltro(txtUsuario.Text)) {
                dataGridView1.Rows.Add(listarDatos.idProducto, listarDatos.nombreProducto, listarDatos.precioProducto);

            }

        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            cargardatos();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            String id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            String Nombre = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            String Precio = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            //FrmVenta frmVenta = new FrmVenta();
            //frmVenta.txtID.Text = id;
            //frmVenta.txtNombreProducto.Text = Nombre;
            //frmVenta.txtPrecio.Text = Precio;
            //frmVenta.Show();

            FrmMenuVenta.frmVenta.txtID.Text = id;
            FrmMenuVenta.frmVenta.txtNombreProducto.Text = Nombre;
            FrmMenuVenta.frmVenta.txtPrecio.Text = Precio;
            FrmMenuVenta.frmVenta.txtCantidad.Focus();
            this.Close();
        }
    }
}
