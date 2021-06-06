using appventas.DAO;
using appventas.MODEL;
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
    public partial class FrmVenta : Form
    {
        public FrmVenta()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        void ultimocorrelativodeventa()
        {
            var consultarultimaventa = new ClsDVenta();
            int lista = 0;

            foreach (var list in consultarultimaventa.UltimaVenta())
            {
                lista = list.iDVenta;

            }
            lista = lista + 1;
            txtNomDocumento.Text = lista.ToString();
        }

        private void FrmVenta_Load(object sender, EventArgs e)
        {
     
                

            ClsCliente clsCliente = new ClsCliente();


                comboBox2.DataSource = clsCliente.cargarDatosCliente();
                comboBox2.DisplayMember = "nombreCliente";
                comboBox2.ValueMember = "iDCliente";

            //var consultadocumento = (from documento in bd.tb_documento
            //                         select new
            //                         {
            //                             documento.iDDocumento,
            //                             documento.nombreDocumento
            //                         }).ToList();

            ClsDocumento clsDocumento = new ClsDocumento();

                comboBox1.DataSource = clsDocumento.cargarDatosDocumento();
                comboBox1.DisplayMember = "nombreDocumento";
                comboBox1.ValueMember = "iDDocumento";

              




        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmProducto buscar = new FrmProducto();
            buscar.Show();
            btnagregar.PerformClick();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            calcular();
        }
        void calcular()
        {
            try
            {

                Double precio, cantidad, total;

                cantidad = Convert.ToDouble(txtCantidad.Text);
                precio = Convert.ToDouble(txtPrecio.Text);

                total = precio * cantidad;

                txtTotal.Text = total.ToString();
            }
            catch (Exception ex)
            {
                if (txtCantidad.Text.Equals(""))
                {
                    txtCantidad.Text = "1";
                    txtCantidad.SelectAll();
                }
            }
        }
        void calculartotal()
        {
            Double suma = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)

            {
                string datosoperartotal = dataGridView1.Rows[i].Cells[4].Value.ToString();
                Double datosConvertidos = Convert.ToDouble(datosoperartotal);


                //suma = suma + datosConvertidos
                suma += datosConvertidos;
                txtTfinal.Text = suma.ToString();


            }
        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            calcular();
            dataGridView1.Rows.Add(txtID.Text, txtNombreProducto.Text, txtPrecio.Text, txtCantidad.Text, txtTotal.Text);
            //Double suma = 0;
            //for (int i= 0;i<dataGridView1.Rows.Count;i++)

            //{
            //    string datosoperartotal = dataGridView1.Rows[i].Cells[4].Value.ToString();
            //    Double datosConvertidos = Convert.ToDouble(datosoperartotal);


            //    //suma = suma + datosConvertidos
            //    suma += datosConvertidos;


            //}
            //txtTfinal.Text = suma.ToString();
            calculartotal();
            txtID.Text = "";
            txtNombreProducto.Clear();
            txtPrecio.Text = "";
            txtCantidad.Clear();
            txtTotal.Text = "";

            dataGridView1.Refresh();
            dataGridView1.ClearSelection();
            int ultimafila = dataGridView1.Rows.Count - 1;
            dataGridView1.FirstDisplayedScrollingRowIndex = ultimafila;
            dataGridView1.Rows[ultimafila].Selected = true;


        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtBusqueda.Text.Equals(""))
            {
                if (e.KeyChar == 13)
                {
                    button1.PerformClick();
                    e.Handled = true;
                    //txtCantidad.Focus();
                }
            }
            else
            {
                if (e.KeyChar == 13)
                {
                    e.Handled = true;
                    ClsProducto prod = new ClsProducto();
                    var busqueda = prod.buscarproducto(Convert.ToInt32(txtBusqueda.Text));


                    if (busqueda.Count < 1)
                    {
                        MessageBox.Show("El codigo no existe");
                        txtBusqueda.Text = "";
                    }


                    foreach (var iterar in busqueda)
                    {
                        txtID.Text = iterar.idProducto.ToString();
                        txtNombreProducto.Text = iterar.nombreProducto;
                        txtPrecio.Text = iterar.precioProducto.ToString();
                        txtCantidad.Text = "1";
                        txtCantidad.Focus();
                        txtBusqueda.Text = "";
                    }
                }
            }


          
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //button1.PerformClick();
                e.Handled = true;
                btnagregar.PerformClick();
                txtBusqueda.Focus();
            }
        }

        private void btnGuardarventa_Click(object sender, EventArgs e)
        {
            try
            {
                ClsDVenta ventas = new ClsDVenta();
                tb_venta venta = new tb_venta();
                venta.iDDocumento = Convert.ToInt32(
                    comboBox1.SelectedValue.ToString());
                venta.iDCliente = Convert.ToInt32
                    (comboBox2.SelectedValue.ToString());
                venta.iDUsuario = 1;
                venta.totalVenta = Convert.ToDecimal(txtTfinal.Text);
                venta.fecha = Convert.ToDateTime(dateTimePicker1.Text);
                ventas.save(venta);

                ClsDDetalle detalle = new ClsDDetalle();
                tb_detalleVenta tbdetallee = new tb_detalleVenta();


                foreach(DataGridViewRow dtgv in dataGridView1.Rows )
                {
                    tbdetallee.idVenta = Convert.ToInt32(txtNomDocumento.Text);
                    tbdetallee.idProducto = Convert.ToInt32(dtgv.Cells[0].Value.ToString());
                    tbdetallee.cantidad = Convert.ToInt32(dtgv.Cells[3].Value.ToString());
                    tbdetallee.precio = Convert.ToDecimal(dtgv.Cells[2].Value.ToString());
                    tbdetallee.total = Convert.ToDecimal(dtgv.Cells[4].Value.ToString());
                    detalle.guardardetalleventa(tbdetallee);
                }
                ultimocorrelativodeventa();
                dataGridView1.Rows.Clear();
                txtTfinal.Text = "";


                MessageBox.Show("Guardar");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }

        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            calculartotal();
        }
    }

    
}
