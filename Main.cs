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
  static void Kontrast(Bitmap slika)
  {
    
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