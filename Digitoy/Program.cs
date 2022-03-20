using System;
using System.Collections.Generic;
using System.Linq;

namespace Digitoy
{
    static class Program
    {
        static Random rnd = new Random();
        static List<int> Taslar;
        static List<Tas> user1 = new List<Tas>();
        static List<Tas> user2 = new List<Tas>();
        static List<Tas> user3 = new List<Tas>();
        static List<Tas> user4 = new List<Tas>();
        static int okey;
        static int gosterge;
        static void Main(string[] args)
        {
            TaslariOlustur();
            Karistir();
            OkeyiSec();
            Dagit();
            int bestHand = BirinciyiBul();
            Console.WriteLine("En iyi ele sahip oyuncu : user" + bestHand);
            Console.Read();
        }
        private static int BirinciyiBul()
        {
            int user1point = PuanHesapla(user1);
            int user2point = PuanHesapla(user2);
            int user3point = PuanHesapla(user3);
            int user4point = PuanHesapla(user4);
            if (user1point > user2point)
            {
                if (user3point > user4point)
                {
                    if (user3point > user1point)
                    {
                        return 3;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    if (user4point > user1point)
                    {
                        return 4;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            else
            {
                if (user3point > user4point)
                {
                    if (user3point > user2point)
                    {
                        return 3;
                    }
                    else
                    {
                        return 2;
                    }
                }
                else
                {
                    if (user4point > user2point)
                    {
                        return 4;
                    }
                    else
                    {
                        return 2;
                    }
                }
            }
        }
        private static int PuanHesapla(List<Tas> istaka)
        {
            int point = 0;
            int jokerCount = istaka.Where(s => s.Sayi.Equals(okey)).Count();
            point += jokerCount * 3;
            istaka.Where(w => w.Sayi.Equals(okey)).ToList().ForEach(s => s.Sayi = 999);
            istaka.Where(w => w.Sayi.Equals(52)).ToList().ForEach(s => s.Sayi = okey);
            istaka.Sort(new SortByNumber());
            var groupsYellow = istaka.Where(x => ((int)(x.Sayi / 13)) == 0).GruptakiArdisiklar();
            var groupsBlue = istaka.Where(x => ((int)(x.Sayi / 13)) == 1).GruptakiArdisiklar();
            var groupsBlack = istaka.Where(x => ((int)(x.Sayi / 13)) == 2).GruptakiArdisiklar();
            var groupsRed = istaka.Where(x => ((int)(x.Sayi / 13)) == 3).GruptakiArdisiklar();

            for (int i = 0; i < groupsYellow.Count(); i++)
            {
                int plusPoint = GetPoint(groupsYellow.ToList()[i].Count());
                if (groupsYellow.ToList()[i].Count() > 2)
                {
                    foreach (var item in groupsYellow.ToList()[i])
                    {
                        istaka.Where(x => x.Sayi == item).First().Kullanim = true;
                    }
                }
                point += plusPoint;
            }
            for (int i = 0; i < groupsBlue.Count(); i++)
            {
                int plusPoint = GetPoint(groupsBlue.ToList()[i].Count());
                if (groupsBlue.ToList()[i].Count() > 2)
                {
                    foreach (var item in groupsBlue.ToList()[i])
                    {
                        istaka.Where(x => x.Sayi == item).First().Kullanim = true;
                    }
                }
                point += plusPoint;
            }
            for (int i = 0; i < groupsBlack.Count(); i++)
            {
                int plusPoint = GetPoint(groupsBlack.ToList()[i].Count());
                if (groupsBlack.ToList()[i].Count() > 2)
                {
                    foreach (var item in groupsBlack.ToList()[i])
                    {
                        istaka.Where(x => x.Sayi == item).First().Kullanim = true;
                    }
                }
                point += plusPoint;
            }
            for (int i = 0; i < groupsRed.Count(); i++)
            {
                int plusPoint = GetPoint(groupsRed.ToList()[i].Count());
                if (groupsRed.ToList()[i].Count() > 2)
                {
                    foreach (var item in groupsRed.ToList()[i])
                    {
                        istaka.Where(x => x.Sayi == item).First().Kullanim = true;
                    }
                }
                point += plusPoint;

            }
            for (int i = 0; i < istaka.Count; i++)
            {
                if (!istaka[i].Kullanim)
                {
                    var lst = istaka.Where(x => !x.Kullanim && (x.Sayi == istaka[i].Sayi + 13
                                    || x.Sayi == istaka[i].Sayi + 26
                                    || x.Sayi == istaka[i].Sayi + 39)
                     );
                    int count = lst.Count() + 1;
                    if (count == 2)
                    {
                        point += 1;
                    }
                    else if (count == 3)
                    {
                        point += 3;
                    }
                    else if (count == 4)
                    {
                        point += 4;
                    }
                    if (count > 2)
                    {
                        foreach (var item in lst)
                        {
                            istaka.Where(x => x.Sayi == item.Sayi).First().Kullanim = true;
                        }
                        istaka.Where(x => x.Sayi == istaka[i].Sayi).First().Kullanim = true;

                    }
                }
            }


            int twinPoint = 0;
            for (int i = 0; i < istaka.Count; i++)
            {
                if (istaka[i].Sayi != 999 && istaka.Where(x => x.Sayi == istaka[i].Sayi).Count() == 2)
                {
                    twinPoint += 2;
                }
            }
            twinPoint += (jokerCount * 2);



            for (int i = 0; i < istaka.Count; i++)
            {
                if (istaka[i].Sayi < 13)
                {
                    Console.WriteLine(istaka[i].Sayi % 13 + 1 + " sari");
                }
                else if (istaka[i].Sayi < 26)
                {
                    Console.WriteLine(istaka[i].Sayi % 13 + 1 + " mavi");

                }
                else if (istaka[i].Sayi < 39)
                {
                    Console.WriteLine(istaka[i].Sayi % 13 + 1 + " siyah");

                }
                else if (istaka[i].Sayi < 52)
                {
                    Console.WriteLine(istaka[i].Sayi % 13 + 1 + " kirmizi");

                }
            }
            Console.WriteLine(jokerCount + " Adet Okey");
            Console.WriteLine(Math.Max(point, twinPoint) + " Puan");
            Console.WriteLine("*******************************");

            return Math.Max(point, twinPoint);
        }
        private static int GetPoint(int v)
        {
            int newPoint = 0;
            switch (v)
            {
                case 0:
                case 1:
                    break;
                case 2:
                    newPoint = 1;
                    break;
                case 3:
                case 4:
                    newPoint = v;
                    break;
                case 5:
                    newPoint = 4;
                    break;
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                    newPoint = v;
                    break;
                default:
                    break;
            }

            return newPoint;
        }
        private static void Dagit()
        {
            int gostergeGivenNum = 0;
            for (int i = 0; i < 56; i++)
            {
                int TasToAdd = Taslar[i];

                #region Gosterge 2 kere dagitilmasin kontrolu
                if (gosterge == Taslar[i])
                {
                    gostergeGivenNum++;
                    if (gostergeGivenNum == 2)
                    {
                        TasToAdd = Taslar[57];
                    }
                }
                #endregion

                if (i < 14)
                {
                    user1.Add(new Tas() { Sayi = TasToAdd });
                }
                else if (i < 28)
                {
                    user2.Add(new Tas() { Sayi = TasToAdd });
                }
                else if (i < 42)
                {
                    user3.Add(new Tas() { Sayi = TasToAdd });
                }
                else if (i < 56)
                {
                    user4.Add(new Tas() { Sayi = TasToAdd });
                }
            }



            int newTas = gostergeGivenNum == 2 ? Taslar[58] : Taslar[57];
            if (gostergeGivenNum != 0 && newTas == gosterge)
            {
                newTas = Taslar[59];
            }
            int fifteenTasUser = rnd.Next(0, 4);
            switch (fifteenTasUser)
            {
                case 0:
                    user1.Add(new Tas() { Sayi = newTas });
                    break;
                case 1:
                    user2.Add(new Tas() { Sayi = newTas });
                    break;
                case 2:
                    user3.Add(new Tas() { Sayi = newTas });
                    break;
                case 3:
                    user4.Add(new Tas() { Sayi = newTas });
                    break;
                default:
                    break;
            }

        }
        private static void OkeyiSec()
        {
            gosterge = rnd.Next(0, 53);
            switch (gosterge)
            {
                case 12:
                    okey = 0;
                    break;
                case 25:
                    okey = 13;
                    break;
                case 38:
                    okey = 26;
                    break;
                case 51:
                    okey = 39;
                    break;
                default:
                    okey = gosterge + 1;
                    break;
            }

        }
        private static void Karistir()
        {
            var result = Taslar.OrderBy(item => rnd.Next());
            Taslar = result.ToList();
        }
        private static void TaslariOlustur()
        {
            Taslar = new List<int>();
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < 53; i++)
                {
                    Taslar.Add(i);
                }
            }
        }
        public static IEnumerable<IEnumerable<int>> GruptakiArdisiklar(this IEnumerable<Tas> list)
        {
            var group = new List<int>();
            foreach (var i in list)
            {
                if (group.Count == 0 || i.Sayi - group[group.Count - 1] <= 1)
                {
                    if (!group.Contains(i.Sayi))
                        group.Add(i.Sayi);
                }
                else if (i.Sayi % 13 == 12 && group.Count > 1 && list.Where(x => x.Sayi == 0).Count() > 0)
                {
                    group.Add(i.Sayi);
                }
                else
                {
                    yield return group;
                    group = new List<int> { i.Sayi };
                }
            }
            yield return group;
        }
    }

}
