using System;
using System.Drawing;
using System.IO;
public class Program
{
    static void ProsledjivanjeListe(string lista, double kontrast, double osvetljenje, bool prvoKontrast, bool overwrite)
    {
        if (File.Exists(lista))
        {
            StreamReader tekst = new StreamReader(lista);
            while (!tekst.EndOfStream)
            {
                string ime = tekst.ReadLine();
                JednaSlika(ime, kontrast, osvetljenje, prvoKontrast, overwrite);
                Console.WriteLine("Slika {0} izmenjena", ime);
            }
        }
        else
        {
            Console.Error.WriteLine("Ne postoji datoteka sa nazivom " + lista);
            return;
        }
    }
    static void JednaSlika(string imeSlike, double kontrast, double osvetljenje, bool prvoKontrast, bool overwrite)
    {
        if (File.Exists(imeSlike))
        {
            string imeFajla = Path.GetFileNameWithoutExtension(imeSlike);
            string ekstenzija = Path.GetExtension(imeSlike);
            if (prvoKontrast && overwrite)
            {

                Bitmap slika = new Bitmap(imeSlike);
                Kontrast(ref slika, kontrast);
                Osvetljenje(ref slika, osvetljenje);
                slika.Save(imeSlike);

            }
            else if (prvoKontrast && !overwrite)
            {
                Bitmap slika = new Bitmap(imeSlike);
                Kontrast(ref slika, kontrast);
                Osvetljenje(ref slika, osvetljenje);
                slika.Save($"{imeFajla}-izmenjeno{ekstenzija}");

            }
            else if (!prvoKontrast && overwrite)
            {
                Bitmap slika = new Bitmap(imeSlike);
                Osvetljenje(ref slika, osvetljenje);
                Kontrast(ref slika, kontrast);
                slika.Save(imeSlike);
            }
            else if (!(prvoKontrast && overwrite))
            {
                Bitmap slika = new Bitmap(imeSlike);
                
                Osvetljenje(ref slika, osvetljenje);
                Kontrast(ref slika, kontrast);
                slika.Save($"{imeFajla}-izmenjeno{ekstenzija}");
            }
            
        }
        else
        {
            Console.Error.WriteLine("Ne postoji datoteka sa nazivom " + imeSlike);
            return;
        }
    }
    static void Osvetljenje(ref Bitmap slika, double procenat)
    {
        double osvetljenje = (procenat * 255) / 100;
        for (int i = 0; i < slika.Height; i++)
        {
            for (int j = 0; j < slika.Width; j++)
            {
                Color pixel = slika.GetPixel(j, i);
                int red = Convert.ToInt32(pixel.R + osvetljenje);
                int green = Convert.ToInt32(pixel.G + osvetljenje);
                int blue = Convert.ToInt32(pixel.B + osvetljenje);
                if (red > 255) red = 255;
                if (green > 255) green = 255;
                if (blue > 255) blue = 255;
                if (red < 0) red = 0;
                if (green < 0) green = 0;
                if (blue < 0) blue = 0;
                Color novo = Color.FromArgb(red, green, blue);
                slika.SetPixel(j, i, novo);
            }
        }
    }

    static void Kontrast(ref Bitmap slika, double procenat)
    {
        double contrast, factor;
        contrast = (procenat * 255) / 100;
        factor = (259 * (contrast + 255)) / (255 * (259 - contrast));
        for (int i = 0; i < slika.Height; i++)
        {
            for (int j = 0; j < slika.Width; j++)
            {
                Color pixel = slika.GetPixel(j, i);
                int red = Convert.ToInt32(factor * (pixel.R - 128) + 128);
                int green = Convert.ToInt32(factor * (pixel.G - 128) + 128);
                int blue = Convert.ToInt32(factor * (pixel.B - 128) + 128);
                if (red > 255) red = 255;
                if (green > 255) green = 255;
                if (blue > 255) blue = 255;
                if (red < 0) red = 0;
                if (green < 0) green = 0;
                if (blue < 0) blue = 0;
                Color novo = Color.FromArgb(red, green, blue);
                slika.SetPixel(j, i, novo);
            }
        }

    }

    public static void Main(string[] args)
    {
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Niste prosledili sve potrebne argumente.");
            return;
        }
        double osvetljenje = 0;
        double kontrast = 0;
        bool prvoKontrast;
        int overwrite = 2;
        int pojedinacnaSlika = 2;
        if (args[0].StartsWith("k") || args[0].StartsWith("K"))
        {
            prvoKontrast = true;
            if (!double.TryParse(args[0].Substring(1), out kontrast) && (kontrast <
                -100 || kontrast > 100))
            {
                Console.Error.WriteLine("Kontrast lose upisan/izvan opsega");
                return;
            }
        }
        else if (args[0].StartsWith("o") || args[0].StartsWith("O"))
        {
            prvoKontrast = false;
            if (!(double.TryParse(args[0].Substring(1), out osvetljenje)) && osvetljenje < -100 && osvetljenje > 100)
            {
                Console.Error.WriteLine("Osvetljenje lose upisano/izvan opsega");
                return;
            }
        }
        else
        {
            Console.Error.WriteLine("Nepoznata komanda za izmenu slike.");
            return;
        }
        if ((args[1].StartsWith("k") || args[1].StartsWith("K")))
        {
            if (prvoKontrast)
            {
                Console.Error.WriteLine("Nemoze isti parametar vise puta");
                return;
            }
            else
            {
                if (!(double.TryParse(args[1].Substring(1), out kontrast))
                && kontrast < -100 && kontrast > 100)
                {
                    Console.Error.WriteLine("Kontrast lose upisan/izvan opsega");
                    return;
                }
            }
        }
        else if (args[1].StartsWith("o") || args[1].StartsWith("O"))
        {
            if (!prvoKontrast)
            {
                Console.Error.WriteLine("Nemoze isti parametar vise puta");
                return;
            }
            if (!(double.TryParse(args[1].Substring(1), out osvetljenje))
              && osvetljenje < -100 && osvetljenje > 100)
            {
                Console.Error.WriteLine("Osvetljenje lose upisano/izvan opsega");
                return;
            }
        }
        else if (args[1] == "s" || args[1] == "S" || args[1] == "t" || args[1] == "T")
        {
            if (args[1] == "s" || args[1] == "S") pojedinacnaSlika = 1;
            else pojedinacnaSlika = 0;
        }
        else
        {
            Console.Error.WriteLine("Nepoznata komanda na drugom polju poziva");
            return;
        }
        if ((args[2] == "s" || args[2] == "S" || args[2] == "t" || args[2] == "T") && pojedinacnaSlika == 2)
        {
            if (args[2] == "s" || args[2] == "S") pojedinacnaSlika = 1;
            else pojedinacnaSlika = 0;
        }
        else if ((args[2] == "s" || args[2] == "S" || args[2] == "t" || args[2] == "T") && pojedinacnaSlika != 2)
        {
            Console.Error.WriteLine("Greska! Dva puta unesen parametar za izvor slike");
        }
        else if (args[2] == "C" || args[2] == "c" || args[2] == "R" || args[2] == "r")
        {
            if (args[2] == "C" || args[2] == "c") overwrite = 0;
            else overwrite = 1;
            if (pojedinacnaSlika == 1)
            {
                for (int i = 3; i < args.Length; i++)
                {
                    bool replace;
                    if (overwrite == 0) replace = false;
                    else replace = true;
                    JednaSlika(args[i], kontrast, osvetljenje, prvoKontrast, replace);

                }
                return;
            }
            else if (pojedinacnaSlika == 0)
            {

                bool replace;
                if (overwrite == 0) replace = false;
                else replace = true;
                ProsledjivanjeListe(args[3], kontrast, osvetljenje, prvoKontrast, replace);
            }
            return;
        }
        else
        {
            Console.Error.WriteLine("Nepoznata komanda na trecem polju poziva");
            return;
        }
        if ((args[3] == "C" || args[3] == "c" || args[3] == "R" || args[3] == "r") && overwrite == 2)
        {
            if (args.Length < 5)
            {
                Console.Error.WriteLine("Niste prosledili sve potrebne argumente.");
                return;
            }
            if (args[3] == "C" || args[3] == "c") overwrite = 0;
            else overwrite = 1;
            if (pojedinacnaSlika == 1)
            {
                for (int i = 4; i < args.Length; i++)
                {
                    bool replace;
                    if (overwrite == 0) replace = false;
                    else replace = true;
                    JednaSlika(args[i], kontrast, osvetljenje, prvoKontrast, replace);
                }
                return;
            }
            else if (pojedinacnaSlika == 0)
            {
                {
                    bool replace;
                    if (overwrite == 0) replace = false;
                    else replace = true;
                    ProsledjivanjeListe(args[4], kontrast, osvetljenje, prvoKontrast, replace);
                }
                return;
            }
            else if ((args[3] == "C" || args[3] == "c" || args[3] == "R" || args[3] == "r") && overwrite != 2)
            {
                Console.Error.WriteLine("Greska dva puta unesen parametar za cuvanje slike");
                return;
            }

        }
        else
        {
            Console.Error.WriteLine("Nepoznata komanda na cetvrtom mestu poziva");
            return;
        }
    }
}