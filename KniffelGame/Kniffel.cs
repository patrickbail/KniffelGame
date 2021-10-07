using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KniffelGame
{
    struct Punkte
    {
        public int pkt;
        public bool isSet;
    }

    class Kniffel
    {
        private int[] mwuerfel = new int[5];
        private Punkte[] mpunkte = new Punkte[13];
        private int setCounter = 13;

        public Kniffel()  //Konstruktor
        {
            //Setzte alle Punkte beim Konstruktor aufruf auf 0 und false
            for (int i = 0; i < mpunkte.Length; i++)
            {
                Punkte punkt;
                punkt.pkt = 0;
                punkt.isSet = false;
                mpunkte[i] = punkt;
            }
        }

        public bool istKleineStrasse()
        {
            //Erstelle 3 bool arrays für alle drei moeglichen kleinen Strassen
            bool[] strasse1 = new bool[4];  //1-2-3-4
            bool[] strasse2 = new bool[4];  //2-3-4-5
            bool[] strasse3 = new bool[4];  //3-4-5-6
            foreach (int wuerfel in mwuerfel) {
                if (wuerfel == 1)
                {
                    strasse1[0] = true;
                }
                else if (wuerfel == 2)
                {
                    strasse1[1] = true;
                    strasse2[0] = true;
                }
                else if (wuerfel == 3)
                {
                    strasse1[2] = true;
                    strasse2[1] = true;
                    strasse3[0] = true;
                }
                else if (wuerfel == 4)
                {
                    strasse1[3] = true;
                    strasse2[2] = true;
                    strasse3[1] = true;
                }
                else if (wuerfel == 5)
                {
                    strasse2[3] = true;
                    strasse3[2] = true;
                }
                else if (wuerfel == 6)
                {
                    strasse3[3] = true;
                }
            }
            //Ueberpruefe ob eins der arrays false enthaelt
            bool fstStr = true;
            bool sndStr = true;
            bool trdStr = true;
            for (int i = 0; i < strasse1.Length; i++)
            {
                if (!strasse1[i])
                {
                    fstStr = false;
                }
                if (!strasse2[i])
                {
                    sndStr = false;
                }
                if (!strasse3[i])
                {
                    trdStr = false;
                }
            }
            return fstStr || sndStr || trdStr;
        }
        public bool istGrosseStrasse()
        {
            //Erstelle 2 bool arrays für beide moeglichen grosse Strassen
            bool[] strasse1 = new bool[5];  //1-2-3-4-5
            bool[] strasse2 = new bool[5];  //2-3-4-5-6
            foreach (int wuerfel in mwuerfel)
            {
                if (wuerfel == 1)
                {
                    strasse1[0] = true;
                }
                else if (wuerfel == 2)
                {
                    strasse1[1] = true;
                    strasse2[0] = true;
                }
                else if (wuerfel == 3)
                {
                    strasse1[2] = true;
                    strasse2[1] = true;
                }
                else if (wuerfel == 4)
                {
                    strasse1[3] = true;
                    strasse2[2] = true;
                }
                else if (wuerfel == 5)
                {
                    strasse1[4] = true;
                    strasse2[3] = true;
                }
                else if (wuerfel == 6)
                {
                    strasse2[4] = true;
                }
            }
            //Ueberpruefe ob eins der arrays false enthaelt
            bool fstStr = true;
            bool sndStr = true;
            for (int i = 0; i < strasse1.Length; i++)
            {
                if (!strasse1[i])
                {
                    fstStr = false;
                }
                if (!strasse2[i])
                {
                    sndStr = false;
                }
            }
            return fstStr || sndStr;
        }
        private int[] countOccurance()
        //Zaehlt die Vorkomnisse der Augenzahlen auf den Wuerfeln
        {
            int[] count = new int[6];
            foreach (int wuerfel in mwuerfel)
            {
                if (wuerfel != 0)
                {
                    count[wuerfel - 1]++;
                }
            }
            return count;
        }

        public int dreierPasch()
        {
            int[] count = countOccurance();
            //Iteriere durch die Liste der Augenzahl Vorkomnisse
            for (int i = 0; i < count.Length; i++)
            {
                //Falls eine Augenzahl 3 mal vorkommt gebe den vierfachen der Augenzahl zurueck
                if (count[i] >= 3)
                {
                    return 3 * (i + 1);
                }
            }
            return 0;
        }
        public int viererPasch()
        {
            int[] count = countOccurance();
            //Iteriere durch die Liste der Augenzahl Vorkomnisse
            for (int i = 0; i < count.Length; i++)
            {
                //Falls eine Augenzahl 4 mal vorkommt gebe den vierfachen der Augenzahl zurueck
                if (count[i] >= 4)
                {
                    return 4 * (i + 1);
                }
            }
            return 0;
        }
        public bool istFullHouse()
        {
            //Bool variablen ob ein zweier und dreier Pasch existieren
            bool dreier = false;
            bool zweier = false;
            int[] count = countOccurance();
            //Falls eine Augenzahl in count 3 oder 2 mal vorkmommt setzte die jeweilige bool variable auf true
            foreach (int augenzahl in count)
            {
                if (augenzahl == 3)
                {
                    dreier = true;
                }
                else if (augenzahl == 2)
                {
                    zweier = true;
                }
            }
            return dreier && zweier;
        }
        public bool istKnfiffel()
        {
            //Iteriere über das Wuerfel array und überprüfe ob alle Wuerfel gleich dem ersten sind
            for (int i = 1; i < 5; i++)
            {
                if (mwuerfel[0] != mwuerfel[i])
                {
                    return false;
                }
            }
            return true;
        }
        public int Chance()
        {
            //Iteriere über alle Wuerfel und rechne die Summe aller Augenzahlen aus
            int summe = 0;
            foreach (int wuerfel in mwuerfel)
            {
                summe += wuerfel;
            }
            return summe;
        }
        public void wuerfeln(bool eins, bool zwei, bool drei, bool vier, bool fuenf)
        {
            //Objekt der Klasse Random erstellen und mit Next(1,7) eine zufaellige zahl 1 <= i < 7 zu bekommen 
            Random rnd = new Random();
            //Es wird nur gewuerfelt fuer den wuerfel wo true uebergeben wurde
            if (eins) {
                mwuerfel[0] = rnd.Next(1, 7);
            }
            if (zwei)
            {
                mwuerfel[1] = rnd.Next(1, 7);
            }
            if (drei)
            {
                mwuerfel[2] = rnd.Next(1, 7);
            }
            if (vier)
            {
                mwuerfel[3] = rnd.Next(1, 7);
            }
            if (fuenf)
            {
                mwuerfel[4] = rnd.Next(1, 7);
            }
        }
        public bool setzePunkte(int nr, int pkt)
        {
            //Falls der Punkt an der Stelle noch nicht gesetzt wurde setzte isSet auf true und platziere die Punkte
            if (!mpunkte[nr].isSet)
            {
                mpunkte[nr].pkt = pkt;
                mpunkte[nr].isSet = true;
                setCounter--;
                return true;
            }
            //Sonst gebe false zurueck da die Punkte an der Stelle nr schon gesetzt wurden
            return false;
        }
        public int getBonus()
        {
            //Falls obere Summe mindestens 63 ist gebe 35 zurueck
            if (getSummeOben() >= 63)
            {
                return 35;
            }
            return 0;
        }
        public int getWert(int nr)
        {
            //Gib den Punktewert an der Stelle nr zurueck
            return mpunkte[nr].pkt;
        }
        public bool isDone()
        {
            if (setCounter == 0)
            {
                return true;
            }
            return false;
        }
        public int getWuerfel(int nr)
        {
            //Gib den wuerfel der nummer nr zurueck
            return mwuerfel[nr];
        }
        public int getSummeOben()
        {
            //Berechne die Summe der oberen Punkte von 0 bis 5
            int obereSumme = 0;
            for (int i = 0; i < 6; i++)
            {
                obereSumme += mpunkte[i].pkt;
            }
            return obereSumme;
        }
        public int getSummeUnten()
        {
            //Berechne die Summe der unteren Punkte von 6 bis 12
            int untereSumme = 0;
            for (int i = 6; i < 13; i++)
            {
                untereSumme += mpunkte[i].pkt;
            }
            return untereSumme;
        }
        public void reset()
        {
            for (int i = 0; i < mwuerfel.Length; i++)
            {
                mwuerfel[i] = 0;
            }
            for (int i = 0; i < mpunkte.Length; i++)
            {
                Punkte punkt;
                punkt.pkt = 0;
                punkt.isSet = false;
                mpunkte[i] = punkt;
            }
            setCounter = 13;
        }
    }
}
