using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RuletaEuropea
{
    public partial class Form1 : Form
    {
        List<Button> lbt;
        List<ProgressBar> lpb;
        List<TextBox> ltb;
        List<RadioButton> lrb;
        int[] apariciones;
        int[] ap18, ap12, ap6, ap3;
        int tiros;
        double probUnNumero;
        int rbActivo;


        public Form1()
        {
            InitializeComponent();
            lbt = new List<Button>
            {
                bt00, bt01, bt02, bt03, bt04, bt05, bt06, bt07, bt08, bt09,
                bt10, bt11, bt12, bt13, bt14, bt15, bt16, bt17, bt18, bt19,
                bt20, bt21, bt22, bt23, bt24, bt25, bt26, bt27, bt28, bt29,
                bt30, bt31, bt32, bt33, bt34, bt35, bt36
            };
            lpb = new List<ProgressBar>
            {
                pb00, pb01, pb02, pb03, pb04, pb05, pb06, pb07, pb08, pb09,
                pb10, pb11, pb12, pb13, pb14, pb15, pb16, pb17, pb18, pb19,
                pb20, pb21, pb22, pb23, pb24, pb25, pb26, pb27, pb28, pb29,
                pb30, pb31, pb32, pb33, pb34, pb35, pb36
            };
            ltb = new List<TextBox>
            {
                tb00, tb01, tb02, tb03, tb04, tb05, tb06, tb07, tb08, tb09,
                tb10, tb11, tb12, tb13, tb14, tb15, tb16, tb17, tb18, tb19,
                tb20, tb21, tb22, tb23, tb24, tb25, tb26, tb27, tb28, tb29,
                tb30, tb31, tb32, tb33, tb34, tb35, tb36
            };
            lrb = new List<RadioButton>
            {
                radioButton1, radioButton2, radioButton3, radioButton6, radioButton5, radioButton4,
                radioButton9, radioButton8, radioButton7, radioButton12, radioButton11, radioButton10,
                radioButton15, radioButton14, radioButton13, radioButton18, radioButton17, radioButton16
            };

            tbnivel0.Text = "36.788%";
            tbnivel0.BackColor = Color.Red;
            tbnivel1.Text = "73.576%";
            tbnivel1.BackColor = Color.Yellow;
            tbnivel2.Text = "91.970%";
            tbnivel2.BackColor = Color.GreenYellow;
            tbnivel3.Text = "98.101%";
            tbnivel3.BackColor = Color.Cyan;
            tbnivel4.Text = "99.634%";
            tbnivel4.BackColor = Color.Blue;
            tbnivel5.Text = "99.941%";
            tbnivel5.BackColor = Color.BlueViolet;
            tbnivel6.Text = "99.992%";
            tbnivel6.BackColor = Color.Violet;
            tbnivel7.Text = "99.999%";
            tbnivel7.BackColor = Color.Magenta;

            Reiniciar();
        }

        private void Reiniciar()
        {
            rbActivo = 0;
            probUnNumero = 1.0 / 37.0;
            apariciones = new int[37];
            for (int k = 0; k < 37; k++)
            {
                lbt[k].Text = k.ToString();
                lpb[k].Value = Convert.ToInt32(100 * probUnNumero);
                ltb[k].Text = "";
                apariciones[k] = 0;
            }
            ap18 = new int[] { 0, 0, 0, 0, 0, 0 };
            ap12 = new int[] { 0, 0, 0, 0, 0, 0 };
            ap6 = new int[] { 0, 0, 0, 0, 0, 0 };
            ap3 = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            tiros = 0;
            tbTiros.Text = tiros.ToString();
            lbHistoria.Items.Clear();
            Actualizar(0);
        }

        private void bt_Click(object sender, EventArgs e)
        {
            // Salio este numero
            int posicion = Convert.ToInt32(((Button)sender).Tag);
            apariciones[posicion]++;
            tiros++;
            tbTiros.Text = tiros.ToString();
            lbHistoria.Items.Insert(0,posicion);
            Actualizar(posicion);
        }
        private void pb_Click(object sender, EventArgs e)
        {
            //if (DialogResult.No == MessageBox.Show("Deshacer tiro?", "", MessageBoxButtons.YesNo))
            if(!cbDeshacerTiro.Checked)
            {
                return;
            }
            
            // Para deshacer click en boton accidental
            int posicion = Convert.ToInt32(((ProgressBar)sender).Tag);
            if (apariciones[posicion] != 0)
            {
                apariciones[posicion]--;
                tiros--;
                Actualizar(posicion);
            }
        }

        private void btContador_Click(object sender, EventArgs e)
        {
            rbActivo = (rbActivo + 1) % 18;
            lrb[rbActivo].Checked = true;
        }
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if(((RadioButton)sender).Checked) rbActivo = Convert.ToInt32(((RadioButton)sender).Tag);
        }

        private void bReiniciar_Click(object sender, EventArgs e)
        {
            Reiniciar();
        }

        private void Actualizar(int numero)
        {
            // Cada casilla
            for (int k = 0; k < 37; k++)
            {
                probabilidadAparecer(k);
            }

            // Por 18 casillas
            switch (numero)
            {
                case 1: case 3: case 5: case 7: case 9: case 12:
                case 14: case 16: case 18: case 19: case 21: case 23:
                case 25: case 27: case 30: case 32: case 34: case 36:
                    // rojo
                    ap18[0]++;
                    break;
                case 0:
                    break;
                default:
                    // negro
                    ap18[1]++;
                    break;
            }
            if (numero != 0)
            {
                if (numero < 19) ap18[2]++;
                else ap18[3]++;

                if (numero % 2 == 0) ap18[4]++;
                else ap18[5]++;
            }
            probabilidadAparecerEspecial(ap18[0], 18, pb18rojo, tb18rojo);
            probabilidadAparecerEspecial(ap18[1], 18, pb18negro, tb18negro);
            probabilidadAparecerEspecial(ap18[2], 18, pb18bajo, tb18bajo);
            probabilidadAparecerEspecial(ap18[3], 18, pb18alto, tb18alto);
            probabilidadAparecerEspecial(ap18[4], 18, pb18par, tb18par);
            probabilidadAparecerEspecial(ap18[5], 18, pb18impar, tb18impar);

            // Por doce casillas
            if (numero != 0)
            {
                if (numero < 13) ap12[0]++;
                else if (numero < 25) ap12[1]++;
                else ap12[2]++;

                if (numero % 3 == 0) ap12[3]++;
                else if (numero % 3 == 2) ap12[4]++;
                else ap12[5]++;
            }
            probabilidadAparecerEspecial(ap12[0], 12, pb12doce1, tb12doce1);
            probabilidadAparecerEspecial(ap12[1], 12, pb12doce2, tb12doce2);
            probabilidadAparecerEspecial(ap12[2], 12, pb12doce3, tb12doce3);
            probabilidadAparecerEspecial(ap12[3], 12, pb12col3, tb12col3);
            probabilidadAparecerEspecial(ap12[4], 12, pb12col2, tb12col2);
            probabilidadAparecerEspecial(ap12[5], 12, pb12col1, tb12col1);

            // Por seis casillas
            if (numero != 0)
            {
                ap6[(numero-1)/6]++;
            }
            probabilidadAparecerEspecial(ap6[0], 6, pb61, tb61);
            probabilidadAparecerEspecial(ap6[1], 6, pb62, tb62);
            probabilidadAparecerEspecial(ap6[2], 6, pb63, tb63);
            probabilidadAparecerEspecial(ap6[3], 6, pb64, tb64);
            probabilidadAparecerEspecial(ap6[4], 6, pb65, tb65);
            probabilidadAparecerEspecial(ap6[5], 6, pb66, tb66);

            // Por tres casillas
            if (numero != 0)
            {
                ap3[(numero - 1) / 3]++;
            }
            probabilidadAparecerEspecial(ap3[0], 3, pb301, tb301);
            probabilidadAparecerEspecial(ap3[1], 3, pb302, tb302);
            probabilidadAparecerEspecial(ap3[2], 3, pb303, tb303);
            probabilidadAparecerEspecial(ap3[3], 3, pb304, tb304);
            probabilidadAparecerEspecial(ap3[4], 3, pb305, tb305);
            probabilidadAparecerEspecial(ap3[5], 3, pb306, tb306);
            probabilidadAparecerEspecial(ap3[6], 3, pb307, tb307);
            probabilidadAparecerEspecial(ap3[7], 3, pb308, tb308);
            probabilidadAparecerEspecial(ap3[8], 3, pb309, tb309);
            probabilidadAparecerEspecial(ap3[9], 3, pb310, tb310);
            probabilidadAparecerEspecial(ap3[10], 3, pb311, tb311);
            probabilidadAparecerEspecial(ap3[11], 3, pb312, tb312);

            RelacionGrupal();
        }

        private void probabilidadAparecer(int posicion)
        {
            double indicador;

            // 1 aparicion justa cada 37 tiros
            if (apariciones[posicion] == 0) indicador = 50;
            else indicador = tiros * probUnNumero/(double)apariciones[posicion] ;    // esperado/visto
            ltb[posicion].Text = Math.Round(indicador,2).ToString();

            switch (Convert.ToInt32(indicador))
            {
                /*
                 ppois(0:12,1)     lambda 1 = np = num tiros * prob de aparecer
                    [0] 0.3678794
                    [1] 0.7357589 
                    [2] 0.9196986 
                    [3] 0.9810118 
                    [4] 0.9963402 
                    [5] 0.9994058 
                    [6] 0.9999168  Seis lambda
                    [7] 0.9999898
                    [8] 0.9999989 
                    [9] 0.9999999 
                   [10] 1.0000000
                */
                case 0:
                    lpb[posicion].ForeColor = Color.Red;
                    break;
                case 1:
                    lpb[posicion].ForeColor = Color.Yellow;
                    break;
                case 2:
                    lpb[posicion].ForeColor = Color.GreenYellow;
                    break;
                case 3:
                    lpb[posicion].ForeColor = Color.Cyan;
                    break;
                case 4:
                    lpb[posicion].ForeColor = Color.Blue;
                    break;
                case 5:
                    lpb[posicion].ForeColor = Color.BlueViolet;
                    break;
                case 6:
                    lpb[posicion].ForeColor = Color.Violet;
                    break;
                case 50:
                    lpb[posicion].ForeColor = Color.Gray;
                    break;
                default:
                    lpb[posicion].ForeColor = Color.Magenta;
                    break;
            }
            indicador /= 5; // 5 lambda 99.94%
            if (indicador > 1) indicador = 1;
            lpb[posicion].Value = Convert.ToInt32(indicador*100);
        }
        private void probabilidadAparecerEspecial(int apariciones1, int numerosAFavor, ProgressBar pb, TextBox tb)
        {
            double indicador;
            double prob = (double)numerosAFavor / 37.0;

            if (apariciones1 == 0) indicador = 50;
            else indicador = tiros * prob / (double)apariciones1;    // esperado/visto
            //tb.Text = apariciones1.ToString();
            tb.Text = Math.Round(indicador, 2).ToString();

            switch (Convert.ToInt32(Math.Truncate(indicador)))
            {
                /*
                 ppois(1:12,1)     lambda 1 = np = num tiros * prob de aparecer
                    [0] 0.3678794
                    [1] 0.7357589 
                    [2] 0.9196986 
                    [3] 0.9810118 
                    [4] 0.9963402 
                    [5] 0.9994058 
                    [6] 0.9999168  Seis lambda
                    [7] 0.9999898
                    [8] 0.9999989 
                    [9] 0.9999999 
                   [10] 1.0000000
                */
                case 0:
                    pb.ForeColor = Color.Red;
                    break;
                case 1:
                    pb.ForeColor = Color.Yellow;
                    break;
                case 2:
                    pb.ForeColor = Color.GreenYellow;
                    break;
                case 3:
                    pb.ForeColor = Color.Cyan;
                    break;
                case 4:
                    pb.ForeColor = Color.Blue;
                    break;
                case 5:
                    pb.ForeColor = Color.BlueViolet;
                    break;
                case 6:
                    pb.ForeColor = Color.Violet;
                    break;
                case 50:
                    pb.ForeColor = Color.Gray;
                    break;
                default:
                    pb.ForeColor = Color.Magenta;
                    break;
            }
            indicador /= (36/numerosAFavor + 1);
            if (indicador > 1) indicador = 1;
            pb.Value = Convert.ToInt32(indicador * 100);
        }

        private void RelacionGrupal()
        {
            double a,b,c,pot=2+tiros/20;

            a = Math.Pow(Convert.ToDouble(tb18rojo.Text), pot);
            b = Math.Pow(Convert.ToDouble(tb18negro.Text), pot);
            if (a + b == 0.0) pbRN.Value = 50;
            else pbRN.Value = Convert.ToInt32(100*a/(a + b));

            a = Math.Pow(Convert.ToDouble(tb18bajo.Text), pot);
            b = Math.Pow(Convert.ToDouble(tb18alto.Text), pot);
            if (a + b == 0.0) pbBA.Value = 50;
            else pbBA.Value = Convert.ToInt32(100 * a / (a + b));

            a = Math.Pow(Convert.ToDouble(tb18par.Text), pot);
            b = Math.Pow(Convert.ToDouble(tb18impar.Text), pot);
            if (a + b == 0.0) pbPI.Value = 50;
            else pbPI.Value = Convert.ToInt32(100 * a / (a + b));

            a = Math.Pow(Convert.ToDouble(tb12doce1.Text), pot);
            b = Math.Pow(Convert.ToDouble(tb12doce2.Text), pot);
            c = Math.Pow(Convert.ToDouble(tb12doce3.Text), pot);
            if (a + b + c == 0.0)
            {
                pbD1.Value = 33;
                pbD2.Value = 33;
                pbD3.Value = 33;
            }
            else
            {
                pbD1.Value = Convert.ToInt32(100 * a / (a + b + c));
                pbD2.Value = Convert.ToInt32(100 * b / (a + b + c));
                pbD3.Value = Convert.ToInt32(100 * c / (a + b + c));
            }

            a = Math.Pow(Convert.ToDouble(tb12col3.Text),pot);
            b = Math.Pow(Convert.ToDouble(tb12col2.Text), pot);
            c = Math.Pow(Convert.ToDouble(tb12col1.Text), pot);
            if (a + b + c == 0.0)
            {
                pbCH.Value = 33;
                pbCM.Value = 33;
                pbCL.Value = 33;
            }
            else
            {
                pbCH.Value = Convert.ToInt32(100 * a / (a + b + c));
                pbCM.Value = Convert.ToInt32(100 * b / (a + b + c));
                pbCL.Value = Convert.ToInt32(100 * c / (a + b + c));
            }
        }
    }
}