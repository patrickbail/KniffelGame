using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KniffelGame
{
    public partial class Form1 : Form
    {
        private Kniffel kniffel;  //Instanz kniffel von der Klasse Kniffel
        private int countWuerfe;  //int Variable um die Anzahl der Wuerfe zu zaehlen
        private RadioButton[] radioButtonsArray;  //Array aller Radiobuttons der Punkteoptionen
        private Bitmap w1Img = Properties.Resources.Wuerfel1;
        private Bitmap w2Img = Properties.Resources.Wuerfel2;
        private Bitmap w3Img = Properties.Resources.Wuerfel3;
        private Bitmap w4Img = Properties.Resources.Wuerfel4;
        private Bitmap w5Img = Properties.Resources.Wuerfel5;
        private Bitmap w6Img = Properties.Resources.Wuerfel6;
        public Form1()
        {
            kniffel = new Kniffel();
            countWuerfe = 3;
            radioButtonsArray = new RadioButton[13];
            InitializeComponent();
            //Itereiere durch den Child Container des tableLayoutPanel1
            int i = 0;
            foreach (Control c in tableLayoutPanel1.Controls)
            {
                //Falls das Child in Spalte 0 und kein Label ist zu radiobuttonsArray hinzufuegen
                if (tableLayoutPanel1.GetColumn(c) == 0 && c.GetType() != typeof(Label))
                {
                    radioButtonsArray[i] = (RadioButton)c;
                    i++;
                }
            }
            //Aendere Sizemode jeder PictureBox damit das Bild angepasst wird
            wuerfel1.SizeMode = PictureBoxSizeMode.StretchImage;
            wuerfel2.SizeMode = PictureBoxSizeMode.StretchImage;
            wuerfel3.SizeMode = PictureBoxSizeMode.StretchImage;
            wuerfel4.SizeMode = PictureBoxSizeMode.StretchImage;
            wuerfel5.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void resetWuerfelCheckBoxes()
        {
            //Setzte die Wuerfelcheckboxes auf Standard Einstellungen zurueck
            //und die Anzahl der Wuerfe auf 3
            checkBoxWuerfel1.Checked = true;
            checkBoxWuerfel2.Checked = true;
            checkBoxWuerfel3.Checked = true;
            checkBoxWuerfel4.Checked = true;
            checkBoxWuerfel5.Checked = true;
            checkBoxWuerfel1.Enabled = false;
            checkBoxWuerfel2.Enabled = false;
            checkBoxWuerfel3.Enabled = false;
            checkBoxWuerfel4.Enabled = false;
            checkBoxWuerfel5.Enabled = false;
            countWuerfe = 3;
        }

        private Bitmap evalImg(int i)
        {
            //Evaluiert das richtige Bitmap image
            if (i == 1)
            {
                return w1Img;
            }
            else if (i == 2)
            {
                return w2Img;
            }
            else if (i == 3)
            {
                return w3Img;
            }
            else if (i == 4)
            {
                return w4Img;
            }
            else if (i == 5)
            {
                return w5Img;
            }
            else
            {
                return w6Img;
            }
        }

        private void zeichneWuerfel()
        {
            //Aktualisiere die Bilder der Würfel
            wuerfel1.Image = evalImg(kniffel.getWuerfel(0));
            wuerfel2.Image = evalImg(kniffel.getWuerfel(1));
            wuerfel3.Image = evalImg(kniffel.getWuerfel(2));
            wuerfel4.Image = evalImg(kniffel.getWuerfel(3));
            wuerfel5.Image = evalImg(kniffel.getWuerfel(4));
        }

        private void wuerfeln_Click(object sender, EventArgs e)
        {
            //bool variablen fuer Wuerfel die gewuerfelt werden sollen
            bool eins = checkBoxWuerfel1.Checked;
            bool zwei = checkBoxWuerfel2.Checked;
            bool drei = checkBoxWuerfel3.Checked;
            bool vier = checkBoxWuerfel4.Checked;
            bool fuenf = checkBoxWuerfel5.Checked;
            //Wuerfeln ueber dem objekt kniffel
            kniffel.wuerfeln(eins, zwei, drei, vier, fuenf);
            //Zeichne die Wuerfel
            zeichneWuerfel();
            //Anzahl der Wuerfe dekrementieren
            countWuerfe--;
            //Fallse Anzahl der Wuerfe 0 dann Reset
            if (countWuerfe == 0)
            {
                resetWuerfelCheckBoxes();
                //Wuerfel Button disablen damit nicht gewuerfelt werden kann
                wuerfeln.Enabled = false;
            }
            else
            {
                //Sonst alle Wuerfel Checkboxen enablen und setPoint Button enablen
                checkBoxWuerfel1.Enabled = true;
                checkBoxWuerfel2.Enabled = true;
                checkBoxWuerfel3.Enabled = true;
                checkBoxWuerfel4.Enabled = true;
                checkBoxWuerfel5.Enabled = true;
                setPoint.Enabled = true;
            }
            anzahlWuerfeLabel.Text = "Noch " + countWuerfe + " Wuerfe";
        }

        private int evalPoints(int i)
        {
            int points = 0;
            //Fallse index i zwischen 0 und 5 dann handelt es sich um oberen Teil der Punkte
            if (i >= 0 && i <= 5)
            {
                //Iteriere über alle Wuerfel
                for (int j = 0; j < 5; j++)
                {
                    int wuerfel = kniffel.getWuerfel(j);
                    //Ueberpruefe ob der Wuerfel die Augenzahl zur jeweiligen Kategorie
                    //(einer, zweier, dreier, ...) uebereinstimmt und summiere die Augenzahlen
                    if (wuerfel == i + 1)
                    {
                        points += wuerfel; 
                    }
                }
            }
            else if (i == 6)
            {
                points = kniffel.dreierPasch();
            }
            else if (i == 7)
            {
                points = kniffel.viererPasch();
            }
            else if (i == 8)
            {
                if (kniffel.istFullHouse())
                {
                    points = 25;
                }
            }
            else if (i == 9)
            {
                if (kniffel.istKleineStrasse())
                {
                    points = 30;
                }
            }
            else if (i == 10)
            {
                if (kniffel.istGrosseStrasse())
                {
                    points = 40;
                }
            }
            else if (i == 11)
            {
                if (kniffel.istKnfiffel())
                {
                    points = 50;
                }
            }
            else
            {
                points = kniffel.Chance();
            }
            return points;
        }
        private void printPunkte()
        {
            //Setzte jedes Label der Punktetabelle auf ihren aktuellen Werten
            einerLabel.Text = kniffel.getWert(0).ToString();
            zweierLabel.Text = kniffel.getWert(1).ToString();
            dreierLabel.Text = kniffel.getWert(2).ToString();
            viererLabel.Text = kniffel.getWert(3).ToString();
            fuenferLabel.Text = kniffel.getWert(4).ToString();
            sechserLabel.Text = kniffel.getWert(5).ToString();
            summeObenLabel.Text = kniffel.getSummeOben().ToString();
            bonusLabel.Text = kniffel.getBonus().ToString();
            dreierPaschLabel.Text = kniffel.getWert(6).ToString();
            viererPaschLabel.Text = kniffel.getWert(7).ToString();
            fullHouseLabel.Text = kniffel.getWert(8).ToString();
            ksLabel.Text = kniffel.getWert(9).ToString();
            gsLabel.Text = kniffel.getWert(10).ToString();
            kniffelLabel.Text = kniffel.getWert(11).ToString();
            chanceLabel.Text = kniffel.getWert(12).ToString();
            summeUntenLabel.Text = kniffel.getSummeUnten().ToString();
            gesammtSummeLabel.Text = (kniffel.getSummeOben() + kniffel.getSummeUnten() + kniffel.getBonus()).ToString();

        }
        private void setPoint_Click(object sender, EventArgs e)
        {
            //Wuerfel Resetn
            resetWuerfelCheckBoxes();
            wuerfeln.Enabled = true;
            //setPoint Button disablen damit nicht sofort wieder Punkte gesetzt werden koennen
            //mit den gleichen Wuerfeln
            setPoint.Enabled = false;
            anzahlWuerfeLabel.Text = "Noch " + countWuerfe + " Wuerfe";
            //Fuer jeden RadioButton
            DialogResult choice = DialogResult.Yes;
            for (int i = 0; i < radioButtonsArray.Length; i++)
            {
                //Finde heraus welcher RadioButton ein Haekchen hat
                if (radioButtonsArray[i].Checked)
                {
                    //Versuche den evaluierten Punkt an dieser Stelle der Punktetabelle zu setzten
                    if (evalPoints(i) == 0)
                    {
                        choice =  MessageBox.Show("Bist du sicher das du deine Punkte bei " + radioButtonsArray[i].Text
                            + " setzten moechtest? Dir werden 0 Punkte eingetragen", 
                            "Warnung", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    }
                    if (choice == DialogResult.Yes)
                    {
                        if (kniffel.setzePunkte(i, evalPoints(i)))
                        {
                            errorLabel.Text = "";
                            printPunkte();
                            if (kniffel.isDone())
                            {
                                DialogResult choice2 = MessageBox.Show("Das Spiel wurde mit " + gesammtSummeLabel.Text
                                    + " Punkten beendet. Moechten sie neu starten?", "Spiel Ende", 
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (choice2 == DialogResult.No)
                                {
                                    Application.Exit();
                                }
                                else
                                {
                                    kniffel.reset();
                                    printPunkte();
                                    resetWuerfelCheckBoxes();
                                    setPoint.Enabled = false;
                                }
                            }
                        }
                        //Falls es nicht moeglich ist soll ein anderer Button gewaehlt werden
                        else
                        {
                            errorLabel.Text = "Punkte bei " + radioButtonsArray[i].Text + " wurden bereits gesetzt";
                            wuerfeln.Enabled = false;
                            setPoint.Enabled = true;
                        }
                    }
                    else
                    {
                        wuerfeln.Enabled = false;
                        setPoint.Enabled = true;
                    }
                }
            }
        }
    }
}
