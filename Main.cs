using System; 
using System.Drawing;
public class Program 
{
  static void ProsledjivanjeListe(string lista,int kontrast,int osvetljenje)
  {
    
  }
  static void JednaSlika(string imeSlike,int kontrast,int osvetljenje)
  {
    
  }
  static void Osvetljenje(Bitmap slika)
  {
    
  }
  
  static void Kontrast(ref Bitmap slika,double procenat)
  {
double contrast,factor;
contrast=(procenat*255)/100;
if(procenat==0)return;
factor = (259 * (contrast + 255)) / (255 * (259 - contrast));
for (int i = 0; i <img.Height; i++)
{
  for (int j = 0; j < img.Width; j++)
  {
   Color pixel = slika.GetPixel(j,i);
    int red=Convert.ToInt32(Math.Truncate(factor * (pixel.R - 128) + 128));
    int green=Convert.ToInt32(Math.Truncate(factor * (pixel.G - 128) + 128));
    int blue=Convert.ToInt32(Math.Truncate(factor * (pixel.B - 128) + 128));
    if(red>255)red=255;
    if(green>255)green=255;
    if(blue>255)blue=255;
    if(red<0)red=0;
    if(green<0)green=0;
    if(blue<0)blue=0;
    Color novo=Color.FromArgb(red,green,blue);
    slika.SetPixel(j,i,novo);
    slika.Save("menjano.bmp");//Aleksa stavi naziv promenjene
  }
}
    
  }
  public static void Main(string[] args) 
  {
    int osvetljenje;
    int kontrast;
    if (args[0] == "s" | args[0] == "S")
            {
                for (int i = 1; i < args.Length; i++)
                {
                    if (args[i].EndsWith(".bmp")|| args[i].EndsWith(".gif")|| args[i].EndsWith(".png")|| args[i].EndsWith(".jpeg")|| args[i].EndsWith(".tiff") || args[i].EndsWith(".exif"))
                    {
                      JednaSlika(args[i]);
                    }
                    else if (!args[i].Contains('.'))
                    {
                      break;
                    }
                    else
                    {
                        Console.Error.WriteLine("Nepodrzana ekstenzija za slike!");
                    }
                }
            }
            else if (args[0] == "t" || args[0] == "T")
            {

            }
            else
            {
                Console.Error.WriteLine("Nepoznata komanda za odabir fajlova!");
            }
  }
}