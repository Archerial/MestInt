using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MI_App
{
    class Program
    {

        static int[] rekeszek = new int[33];
        static int utolsoBekerultSzam = 0;
        static int player;
        static int aiRnd;

        static void Main(string[] args)
        {

            Console.WriteLine("Játék szabály \n ");
            Console.WriteLine("Adott 32 egymás mellett elhelyezett rekesz. \nA játékosok felváltva következnek lépni. \nMinden lépésben egy kavicsot kell elhelyezni egy olyan üres rekeszbe, amellyel szomszédos rekeszek szintén üresek." +
                "\nA lépni következő játékos veszít, ha nincs olyan rekesz, amelybe kavicsot lehet helyezni. \n" +
                "Amennyiben érvénytelen pozicióra raknánk be követ, úgy automatikusan a másik játékos következik. \n");


            List<int> foglaltPoziciok = new List<int>();

            Random rnd = new Random();
           
            for (int i = 0; i < rekeszek.Length; i++)
            {
                rekeszek[i] = 0;
            }

            while (!gameOver())
            {
                Console.WriteLine("Kérek egy poziciót ahová tenni szeretnéd a kavicsod: ");
                player = int.Parse(Console.ReadLine());
                if (player <= 0 || player > 32)
                {
                    Console.WriteLine("Hibás érték, az AI következik.");
                    break;
                }
                if (rakhatE(rekeszek, player))
                {
                    belerak(player);
                    Console.WriteLine("Bekerült");
                    foglaltPoziciok.Add(player);
                    utolsoBekerultSzam = player;
                }
                else
                {
                    Console.WriteLine("A pozició már foglalt, az AI következik.");
                }
                Console.WriteLine("A folytatáshoz nyomj egy Enter-t");
                Console.ReadKey();
                //Itt kezdődik a számítógép játéka:
                Console.WriteLine("Az AI jön.:");
                aiRnd = rnd.Next(1, 32);  //véletlenszerűen generál egy számor, 1 és 32 között.
                while (foglaltPoziciok.Contains(aiRnd))  //Amennyiben a pozició már foglalt...
                {
                    aiRnd = rnd.Next(1, 32); //generál egy újat.
                    if (rekeszek[aiRnd - 1] != 0 || rekeszek[aiRnd + 1] != 0){
                        aiRnd = rnd.Next(1, 32);
                    }
                }
                if (rakhatE(rekeszek,aiRnd))
                {
                    Console.WriteLine("A kavics berakása a {0}. pozícióra",aiRnd);
                    belerak(aiRnd); 
                    foglaltPoziciok.Add(aiRnd);
                    utolsoBekerultSzam = aiRnd;
                }else
                {
                    Console.WriteLine("Nem sikerült beraknom, a kavicsit a(z) {0}. pozicióra, te következel", aiRnd);
                }
                Console.WriteLine("Te következel:");
                /*if (utolsoBekerultSzam == aiRnd)
                {
                    Console.WriteLine("Te vesztettél");
                } else if(utolsoBekerultSzam == player)
                {
                    Console.WriteLine("Gratulálok, te nyertél!");
                }*/
            }
            Console.WriteLine("A játéknak vége");
            if (utolsoBekerultSzam == player)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Nyertél");
                Console.ResetColor();

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Vesztettél");
                Console.ResetColor();
            }

            for (int i = 1; i < rekeszek.Length; i++)
            {
                Console.Write(i + ".    rekeszben ");
                if (rekeszek[i] == 1)
                    Console.WriteLine("van kavics!");
                else
                    Console.WriteLine("nincs kavics!");
            }
        




            Console.ReadLine();

        }

        public static bool rakhatE(int[] rekeszek,int rakni) //Itt elennörzöm, hogy az adott pozició érvényes-e vagy sem
        {
            if (rakni == 1) //2 speciális esetet különböztetek meg, az egyik amikor a legelső pozicóra akarunk kavicsot rakni..
            {
                if(rekeszek[rakni+1] == 0 && rekeszek[rakni] == 0)
                {
                    return true;
                }
            }

            else if(rakni == 32) //.. a másik amikor a legutolsó pozicióra, ilyenkor csak egy irányt kell ellenőrizni, 
            {                   //1-es esetén a 2-es poziciót, 32 esetén a 31-es poziciót, valamint, hogy maguk a poziciók üresek-e még
                if (rekeszek[rakni - 1] == 0 && rekeszek[rakni] == 0)
                {
                    return true;
                }
            }
            //Itt pedig általánosan ellenörzöm, hogy az adott szám előtt és után üres-e még a tömb.
            else if(rekeszek[rakni + 1] == 0 && rekeszek[rakni - 1] == 0 && rekeszek[rakni] == 0)  
            {
                return true;
            }
            return false;

        } 

        public static void belerak(int ide)
        {
            if (rakhatE(rekeszek,ide)) //felhasználom, a rakhatE metódusomat, ami eldönti, hogy az adott pozicióba rakható-e kavics
            {
                rekeszek[ide] = 1; //ha rakható, az adott pozición a csupa nullából álló tömb értékét 1-esre változtatom
            }
            else
            {
                Console.WriteLine("Nem rakhatsz bele");
            }
        }

        public static bool gameOver() //Addig megy a játék, amíg van szabadhely.
        {
            if (!vanEmegSzabadHely()) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool vanEmegSzabadHely() //Itt azt ellenörzöm, hogy van-e még szabad hely.
        {
            if (rekeszek[0] == 0 && rekeszek[0+1] == 0)
            {
                return true;
            }
            if (rekeszek[32] == 0 && rekeszek[32-1] == 0)
            {
                return true;
            }
            for (int i = 2; i < rekeszek.Length-2; i++)
            {
                if(rekeszek[i+1] == 0 && rekeszek[i-1] == 0 && rekeszek[i] == 0)
                {
                    return true;
                }
            }
            return false;
        }

    }
}

