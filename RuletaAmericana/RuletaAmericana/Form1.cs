using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RuletaAmericana
{
    public partial class Form1 : Form
    {
        Point puntoInicial = new Point(40,510); // 46, 60
        int ancho = 36;
        int separador = 6;
        List<Button> casilla = new List<Button>();
        List<Button> tercia = new List<Button>();
        List<Button> sexta = new List<Button>();
        List<Button> docena = new List<Button>();
        List<Button> columna = new List<Button>();
        List<Button> mitad = new List<Button>();
        List<Color> colores = new List<Color>();

        Random aleatorio = new Random();

        public Form1()
        {
            InitializeComponent();

            // Colores para indicar probabilidad = rojo (alta) - verde (media) - azul (baja)
            colores.Add(new Color());
            colores[0] = Color.FromArgb(1, 1, 1);

            // Creacion tablero con textboxes
            for (int i=0; i < 38; i++)
            {
                casilla.Add(new Button());
                casilla[i].Tag = i.ToString();
                casilla[i].Location = new Point( puntoInicial.X + ancho * ((i-1)/3) , puntoInicial.Y + ancho * ((i - 1) % 3));
                casilla[i].Visible = true;
                casilla[i].Text = "0";
                casilla[i].Parent = this;
                casilla[i].Size = new Size(ancho, ancho);
                casilla[i].FlatStyle = FlatStyle.Flat;
            }

            // 0 en posicion 0 y 00 en posicion 37
            casilla[0].Location = new Point(puntoInicial.X - ancho , puntoInicial.Y);
            casilla[0].Size = new Size(ancho, 3*ancho/2);
            casilla[37].Location = new Point(puntoInicial.X - ancho, puntoInicial.Y + 3 * ancho / 2);
            casilla[37].Size = new Size(ancho, 3 * ancho / 2);

            // Agrupaciones
            //    Tercia
            for (int i = 0; i < 12; i++)
            {
                tercia.Add(new Button());
                tercia[i].Tag = i.ToString();
                tercia[i].Location = new Point(puntoInicial.X + ancho * i, puntoInicial.Y - separador - ancho);
                tercia[i].Visible = true;
                tercia[i].Text = "0";
                tercia[i].Parent = this;
                tercia[i].Size = new Size(ancho, ancho);
                tercia[i].FlatStyle = FlatStyle.Flat;
            }
            //    Sexta
            for (int i = 0; i < 6; i++)
            {
                sexta.Add(new Button());
                sexta[i].Tag = i.ToString();
                sexta[i].Location = new Point(puntoInicial.X + ancho * 2 * i, puntoInicial.Y - separador - 2 * ancho);
                sexta[i].Visible = true;
                sexta[i].Text = "0";
                sexta[i].Parent = this;
                sexta[i].Size = new Size(ancho*2, ancho);
                sexta[i].FlatStyle = FlatStyle.Flat;
            }
            //    Docena
            for (int i = 0; i < 3; i++)
            {
                docena.Add(new Button());
                docena[i].Tag = i.ToString();
                docena[i].Location = new Point(puntoInicial.X + ancho * 4 * i, puntoInicial.Y - separador - 3 * ancho);
                docena[i].Visible = true;
                docena[i].Text = "0";
                docena[i].Parent = this;
                docena[i].Size = new Size(ancho * 4, ancho);
                docena[i].FlatStyle = FlatStyle.Flat;
            }
            //    Docena
            for (int i = 0; i < 3; i++)
            {
                columna.Add(new Button());
                columna[i].Tag = i.ToString();
                columna[i].Location = new Point(puntoInicial.X + ancho * 12 + separador, puntoInicial.Y + ancho * i);
                columna[i].Visible = true;
                columna[i].Text = "0";
                columna[i].Parent = this;
                columna[i].Size = new Size(ancho * 2, ancho);
                columna[i].FlatStyle = FlatStyle.Flat;
            }
            //    Mitad
            for (int i = 0; i < 2; i++)
            {
                mitad.Add(new Button());
                mitad[i].Tag = i.ToString();
                mitad[i].Location = new Point(puntoInicial.X + ancho * 6 * i , puntoInicial.Y - 4* ancho - separador);
                mitad[i].Visible = true;
                mitad[i].Text = "0";
                mitad[i].Parent = this;
                mitad[i].Size = new Size(ancho * 6, ancho);
                mitad[i].FlatStyle = FlatStyle.Flat;
            }
        }

        private void aumentar(Button b)
        {
            b.Text = (Convert.ToInt32(b.Text) + 1).ToString();
        }

        private int sumar(List<Button> lb, bool esCasilla = false)
        {
            int resultado=0;

            if(!esCasilla) for (int i = 0; i < lb.Count; i++) resultado += Convert.ToInt32(lb[i].Text);
            else for (int i = 1; i < lb.Count-1; i++) resultado += Convert.ToInt32(lb[i].Text);

            return resultado;
        }

        private void colorear(List<Button> lb)
        {
            // Encuentra maximo
            int max = Convert.ToInt32(lb[0].Text);
            int min = Convert.ToInt32(lb[0].Text);
            int revision;
            for (int i = 1; i < lb.Count; i++)
            {
                revision = Convert.ToInt32(lb[i].Text);
                if (max < revision) max = revision;
                if (min > revision) min = revision;
            }
            max++;    // Puede salir el mayor, aunque se espera más que lo igualen los menores primero.

            // Coloreado de probabilidades: rojo (alta), verde (media), azul (baja) valores rgb de 0 a 120
            //    desde (0,0,120) 0% hasta (120,0,0) 100%, pasando por (0,120,0) 50% 
            for (int i = 0; i < lb.Count; i++)
            {
                // Probabilidad de que salga según falacia del jugador = (max - ocurrencia) / (max - min)
                double probabilidad = (double)(max - Convert.ToInt32(lb[i].Text)) / (double)(max - min);
                int valmaxcol=250;
                lb[i].BackColor = Color.FromArgb
                (
                    (int)(valmaxcol * probabilidad), 
                    (int)(valmaxcol * (1-2*Math.Abs(0.5 - probabilidad))),
                    (int)(valmaxcol * (1 - probabilidad))
                );
            }

        }


        private void bTirar_Click(object sender, EventArgs e)
        {
            int numero = aleatorio.Next(0, 38);
            aumentar(casilla[numero]);
            listBox1.Items.Add(numero);
            listBox1.SelectedIndex = listBox1.Items.Count - 1;

            if (numero == 0 || numero == 37) return;

            aumentar(mitad[(numero - 1) / 18]);    // 1 al 18  o  19 a 36
            aumentar(docena[(numero - 1) / 12]);
            aumentar(sexta[(numero - 1) / 6]);
            aumentar(tercia[(numero - 1) / 3]);

            aumentar(columna[(numero - 1) % 3]);

            colorear(mitad);
            colorear(docena);
            colorear(sexta);
            colorear(tercia);
            colorear(columna);
            colorear(casilla);
        }

        private void timerTiro_Tick(object sender, EventArgs e)
        {
            bTirar_Click(sender, e);
        }

        private void bAutomatico_Click(object sender, EventArgs e)
        {
            if(timerTiro.Enabled) timerTiro.Enabled = false;
            else timerTiro.Enabled = true;
        }
    }
}
