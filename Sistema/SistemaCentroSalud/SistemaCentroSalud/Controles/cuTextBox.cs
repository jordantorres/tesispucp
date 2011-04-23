﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SistemaCentroSalud.Controles
{
    public partial class cuTextBox : TextBox
    {
        public cuTextBox()
        {
            InitializeComponent();
        }

        public enum TipoTextBox : int
        {
            Mixto = 0, SoloNumeros = 1, SoloLetras = 2
        };

        public enum SoloLectura : int
        {
            falso = 0, verdadero = 1
        };

        TipoTextBox tipoCajaTexto;
        SoloLectura soloLectura;

        public TipoTextBox TipoCajaTexto
        {
            get
            {
                return tipoCajaTexto;
            }

            set
            {
                tipoCajaTexto = value;
            }
        }

        public SoloLectura Solo_Lectura
        {
            get
            {
                return soloLectura;
            }

            set
            {
                soloLectura = value;
                
                if (soloLectura == SoloLectura.verdadero)
                {
                    this.ReadOnly = true;
                    this.BackColor = Color.AliceBlue;
                }
            }
        }

        protected override void OnEnter(EventArgs e)
        {
            if (soloLectura == SoloLectura.verdadero)
            {
                this.BackColor = Color.AliceBlue;
            }
            else
            {
                this.BackColor = Color.LightYellow;
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            if (soloLectura == SoloLectura.falso)
            {
                this.BackColor = Color.White;
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (tipoCajaTexto == TipoTextBox.SoloNumeros)
            {
                if (Char.IsDigit(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            else if (tipoCajaTexto == TipoTextBox.SoloLetras)
            {
                if (Char.IsLetter(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (Char.IsSeparator(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            else
            {
                //No hace nada
            }
        }
    }
}
