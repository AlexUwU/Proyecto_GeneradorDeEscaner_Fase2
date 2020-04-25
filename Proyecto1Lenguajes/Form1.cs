using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1Lenguajes
{
    public partial class frmExaminar : Form
    {
        OpenFileDialog Explorador = new OpenFileDialog();
        string archivo;

        public frmExaminar()
        {
            archivo = null;
            InitializeComponent();            
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            Explorador.ShowDialog();
            txtArchivo.Text = Explorador.FileName;

            if (txtArchivo.Text != "")
            {
                archivo = txtArchivo.Text;
            }
            else
            {
                MessageBox.Show("No ha seleccionado ningun archivo");
            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Lector lector = new Lector();
            MessageBox.Show(lector.LeerArchivo(archivo));
        }
    }
}
