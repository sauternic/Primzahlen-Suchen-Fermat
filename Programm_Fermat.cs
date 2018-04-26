using System;
using System.Diagnostics;
using System.Numerics;

namespace Program_Fermat
{
    class Program
    {

        // 16 Stellen:   1000000000000000
        // 17 Stellen:   10000000000000000
        // 20 Stellen:   10000000000000000000
        // 21 Stellen :  100000000000000000000
        // 22 Stellen :  1000000000000000000000
        // Max ulong =   18446744073709551615;
        // Max decimal = 79228162514264337593543950335M;

        // Field
        static int P_Nr = 0;

        static void _Main()
        {
            BigInteger anfang = 0;
            BigInteger ende = 0;
            int letzteBasis = 2;

            Console.Write("\n\n   Primzahlenauflisten nach Fermat!\n\n");
            Console.Write("\n   Untere Grenze Eingeben? ");
            anfang = BigInteger.Parse(Console.ReadLine());

            Console.Write("   Obere  Grenze Eingeben? ");
            ende = BigInteger.Parse(Console.ReadLine());
            Console.WriteLine();// Leerzeile

            Console.Write(
                "   Basis?\n\n" +
                "   Es werden alle bis zur letzten gerechnet.\n" +
                "   Ausser wenn ein Ergebnis > 1 wird sofort Abgebrochen\n\n" +   
                "   Von Basis 2 bis Basis: ");
            letzteBasis = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();// Leerzeile


            SuchePrimzahlen(anfang, ende, letzteBasis);
        }

        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
        static void SuchePrimzahlen(BigInteger anfang, BigInteger ende, int letzteBasis)
        {
            Stopwatch s = new Stopwatch();


            // Schleife Ersetzt Hand Eingabe.
            for (; anfang <= ende; anfang++)
            {
                s.Start();

                // Primzahlen Engine nach Fermat! :)))
                if (!IstPrimzahlFermat(anfang, letzteBasis))
                    continue;

                s.Stop();
                TimeSpan timeSpan = s.Elapsed;


                // Ausgabe
                ++P_Nr;
                string ausgString = String.Format("\nPrimzahl {0} :)  {1:#,#}\n", P_Nr, anfang);
                ausgString += String.Format("Time: {0}h {1}m {2}s {3}ms\n", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);

                Console.WriteLine(ausgString);
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++
            }
            Console.WriteLine("Fertig! :)");
            Console.WriteLine("\n\tCopyright © Nicolas Sauter");
            Console.ReadLine();
        }

        static bool IstPrimzahlFermat(BigInteger bigZahl, int letzteBasis)
        {
            BigInteger bigErgebnis;

            //Vorselektion nach Liste:
            if (bigZahl == 0 || bigZahl == 1)
                return false;

            if (bigZahl == 2)
                return true;

            if (bigZahl == 5)
                return true;

            //Vorselektion mit Modulo, gerade Zahlen und 5 am Schluss Raus:
            if ((bigZahl % 2) == 0 || bigZahl % 5 == 0)
                return false;

            //Beliebig viele Durchläufe Angefangen mit Basis 2 bis letzteBasis;
            for (int i = 2; i <= letzteBasis; i++)
            {
                //Test nach Fermat mit ModPow() Methode! :)))))
                bigErgebnis = BigInteger.ModPow(i, bigZahl - 1, bigZahl);

                // 7 Gibt bei Basis 7, 14, 21 usw.  eine Null obwohl Prim! 
                // 0 ist auch Prim
                // gibts z.B. bei prime 7 Basis 7 oder prime 11 Basis 11 usw.
                // (auch bei allen vielfachen der Basis)
                if (bigErgebnis > 1)
                    return false;
            }
            return true;
        }
    }
}
