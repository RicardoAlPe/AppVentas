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
    public partial class FrmMenuVenta : Form
    {
        public FrmMenuVenta()
        {
            InitializeComponent();
        }
        public static FrmVenta frmVenta = new FrmVenta();
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVenta.Show();
        }
    }
}
