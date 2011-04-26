﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SistemaCentroSalud.Ventanas_Personal;
using SistemaCentroSalud.Ventanas_Doctor;
using System.Diagnostics;
using Comun;
using SistemaCentroSalud.Ventanas_Ayuda;
using SistemaCentroSalud.Ventanas_Seguridad;

namespace SistemaCentroSalud
{
    public partial class frmPrincipal : Form
    {
        frmBienvenida ventanaBienvenida;
        private int childFormNumber = 0;

        public frmPrincipal(frmBienvenida ventanaBienvenida)
        {
            InitializeComponent();
            this.ventanaBienvenida = ventanaBienvenida;
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Ventana " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro(a) que desea salir del sistema?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                this.Dispose();
                ventanaBienvenida.limpiarCampos();
                ventanaBienvenida.Visible = true;
            }
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsIconos.Visible = smnuBarraHerramientas.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sstInformacion.Visible = smnuBarraEstado.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("¿Está seguro(a) que desea salir del sistema?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                this.Dispose();
                ventanaBienvenida.limpiarCampos();
                ventanaBienvenida.Visible = true;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void smnuPaciente_Click(object sender, EventArgs e)
        {
            frmPaciente ventanaPaciente = new frmPaciente();
            ventanaPaciente.MdiParent = this;
            ventanaPaciente.Show();
        }

        private void doctorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDoctor ventanaDoctor = new frmDoctor();
            ventanaDoctor.MdiParent = this;
            ventanaDoctor.Show();
        }

        private void tsbSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro(a) que desea salir del sistema?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                this.Dispose();
                ventanaBienvenida.limpiarCampos();
                ventanaBienvenida.Visible = true;
            }
        }

        private void tsbPaciente_Click(object sender, EventArgs e)
        {
            frmPaciente ventanaPaciente = new frmPaciente();
            ventanaPaciente.MdiParent = this;
            ventanaPaciente.Show();
        }

        private void smnuEnfermera_Click(object sender, EventArgs e)
        {
            frmEnfermera ventanaEnfermera = new frmEnfermera();
            ventanaEnfermera.MdiParent = this;
            ventanaEnfermera.Show();
        }

        private void smnuTecnico_Click(object sender, EventArgs e)
        {
            frmTecnico ventanaTecnico = new frmTecnico();
            ventanaTecnico.MdiParent = this;
            ventanaTecnico.Show();
        }

        private void smnuContrasena_Click(object sender, EventArgs e)
        {
            frmCambiarContrasena ventanaCambiarContrasena = new frmCambiarContrasena();
            ventanaCambiarContrasena.MdiParent = this;
            ventanaCambiarContrasena.Show();
        }

        private void tsbContrasena_Click(object sender, EventArgs e)
        {
            frmCambiarContrasena ventanaCambiarContrasena = new frmCambiarContrasena();
            ventanaCambiarContrasena.MdiParent = this;
            ventanaCambiarContrasena.Show();
        }

        private void smnuHistoriaClinicaCompleta_Click(object sender, EventArgs e)
        {
            if (smnuHistoriaClinicaCompleta.Checked)
            {
                smnuHistoriaClinicaCompleta.Checked = false;
            }
            else
            {
                smnuHistoriaClinicaCompleta.Checked = true;
            }
        }

        private void smnuCalculadora_Click(object sender, EventArgs e)
        {
            try
            {
                Process prsCalculadora = new Process();
                prsCalculadora.StartInfo.FileName = "calc.exe";
                prsCalculadora.StartInfo.Arguments = "";
                prsCalculadora.Start();
            }
            catch (Exception ex)
            {
                clsComun.registrarErrorLog(ex.ToString());
            }
        }

        private void smnuBlocDeNotas_Click(object sender, EventArgs e)
        {
            try
            {
                Process prsCalculadora = new Process();
                prsCalculadora.StartInfo.FileName = "notepad.exe";
                prsCalculadora.StartInfo.Arguments = "";
                prsCalculadora.Start();
            }
            catch (Exception ex)
            {
                clsComun.registrarErrorLog(ex.ToString());
            }
        }

        private void smnuHistoriaClinica_Click(object sender, EventArgs e)
        {
            bool blnHistoriaClinicaCompleta = false;

            if (smnuHistoriaClinicaCompleta.Checked)
            {
                blnHistoriaClinicaCompleta = true;
            }

            frmHistorialClinica ventanaHistoriaClinica = new frmHistorialClinica(blnHistoriaClinicaCompleta);
            ventanaHistoriaClinica.MdiParent = this;
            ventanaHistoriaClinica.Show();
        }

        private void smnuAdministrativo_Click(object sender, EventArgs e)
        {
            frmAdministrativo ventanaAdministrativo = new frmAdministrativo();
            ventanaAdministrativo.MdiParent = this;
            ventanaAdministrativo.Show();
        }

        private void smnuPerfil_Click(object sender, EventArgs e)
        {
            frmPerfil ventanaPerfil = new frmPerfil();
            ventanaPerfil.MdiParent = this;
            ventanaPerfil.Show();
        }

        private void smnuAcercaDe_Click(object sender, EventArgs e)
        {
            frmAcercaDe ventanaAcercaDe = new frmAcercaDe();
            ventanaAcercaDe.ShowDialog();
        }
    }
}
