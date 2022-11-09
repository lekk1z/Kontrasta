using System; 
using System.Drawing;
using System.IO;
public class Program 
{
  static void ProsledjivanjeListe(string lista,int kontrast,int osvetljenje,bool prvoKontrast,bool overwrite)
  {
    StreamReader tekst = new StreamReader(lista);
    if(prvoKontrast&&overwrite){
      while(!tekst.EndOfStream)
    {
      Bitmap slika = new Bitmap(tekst.ReadLine());
        Kontrast(slika,kontrast);
        Osvetljenje(slika,osvetljenje);
    }
    }
    else if(prvoKontrast&&!overwrite){
      while(!tekst.EndOfStream)
    {
        string ime=lista.ReadLine();
      File.Copy(lista,$"{ime}(izmenjeno)");
        Bitmap slika = new Bitmap($"{ime}(izmenjeno)");
         Kontrast(slika,kontrast);
        Osvetljenje(slika,osvetljenje);
    }
    }
    else if(!prvoKontrast&&overwrite){
      while(!tekst.EndOfStream)
    {
      Bitmap slika = new Bitmap(tekst.ReadLine());
        Osvetljenje(slika,osvetljenje);
        Kontrast(slika,kontrast);
    }
    }
    else if(!((prvoKontrast&&overwrite)){
      while(!tekst.EndOfStream)
    {
      string ime=lista.ReadLine();
      File.Copy(lista,$"{ime}(izmenjeno)");
        Bitmap slika = new Bitmap($"{ime}(izmenjeno)");
        Osvetljenje(slika,osvetljenje);
        Kontrast(slika,kontrast);
    }
    }
  }
  static void JednaSlika(string imeSlike,int kontrast,int osvetljenje,bool prvoKontrast,bool overwrite)
  {
    if(prvoKontrast&&overwrite){
     
      Bitmap slika = new Bitmap(imeSlike);
        Kontrast(slika,kontrast);
        Osvetljenje(slika,osvetljenje);
    
    }
    else if(prvoKontrast&&!overwrite){
      File.Copy(imeSlike,$"{imeSLike}(izmenjeno)");
        Bitmap slika = new Bitmap($"{imeSlike}(izmenjeno)");
         Kontrast(slika,kontrast);
        Osvetljenje(slika,osvetljenje);
    
    }
    else if(!prvoKontrast&&overwrite){
    
      Bitmap slika = new Bitmap(imeSlike);
        Osvetljenje(slika,osvetljenje);
        Kontrast(slika,kontrast);
    }
    else if(!((prvoKontrast&&overwrite)){
      
       File.Copy(imeSlike,$"{imeSLike}(izmenjeno)");
        Bitmap slika = new Bitmap($"{imeSlike}(izmenjeno)");
        Osvetljenje(slika,osvetljenje);
        Kontrast(slika,kontrast);
    }
  }
  static void Osvetljenje(Bitmap slika,double procenat)
  {
    
  }
  
  static void Kontrast( Bitmap slika,double procenat)
  {
double contrast,factor;
contrast=(procenat*255)/100;
factor = (259 * (contrast + 255)) / (255 * (259 - contrast));
for (int i = 0; i <slika.Height; i++)
{
  for (int j = 0; j < slika.Width; j++)
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
  }
}
    slika.Save(slika);
    
  }
  
  public static void Main(string[] args) 
  {
   Console.ForegroundColor = ConsoleColor.Red;
    double osvetljenje=0;
    double kontrast=0;
    bool prvoKontrast;
    int overwrite=2;
    int pojedinacnaSLika=2;
    
    if ((args[0].StartWith ("k") || args[0].StartWith ("K")))
      {
	prvoKontrast = true;
	if (!(double.TryParse (args[0].Substring (1), out kontrast)) &&kontrast <
	    -100 && kontrast > 100)
	  {
	    Console.Error.WriteLine ("Kontrast lose upisan/izvan opsega");
	    return;
	  }
      }
    else if (args[0].StartWith ("o") || args[0].StartWith ("O"))
      {
	prvoKontrast = false;
	if (!(double.TryParse (args[0].Substring (1), out osvetljenje))
	    &&osvetljenje < -100 && osvetljenje > 100)
	  {
	    Console.Error.WriteLine ("Osvetljenje lose upisano/izvan opsega");
	    return;
	  }
      }
    else
      {
	Console.Error.WriteLine ("Nepoznata komanda za izmenu slike.");
      }
    if ((args[1].StartWith ("k") || args[1].StartWith ("K")))
      {
	if (prvoKontrast)
	  {
	    Console.Error.WriteLine ("Nemoze isti parametar vise puta");
	  return;}
	else
	  {
	    if (!(double.TryParse (args[1].Substring (1), out kontrast))
		&&kontrast < -100 && kontrast > 100)
	      {
		Console.Error.WriteLine ("Kontrast lose upisan/izvan opsega");
		return;
	      }
	  }
      }
    else if (args[1].StartWith ("k") || args[1].StartWith ("K"))
      {
	if (!prvoKontrast)
	  {
Console.Error.WriteLine ("Nemoze isti parametar vise puta");
	  return;
	  }
      if (!(double.TryParse (args[1].Substring (1), out osvetljenje))
	    &&osvetljenje < -100 && osvetljenje > 100)
	  {
	    Console.Error.WriteLine ("Osvetljenje lose upisano/izvan opsega");
	    return;
	  }
      }
    else if(args[1] == "s" || args[1] == "S"||args[1] == "t" || args[1] == "T"){
      if(args[1] == "s" || args[1] == "S")pojedinacnaSLika=1;
      else pojedinacnaSLika=0;
    }
    else{
      Console.Error.WriteLine("Nepoznata komanda na drugom polju poziva");
      return;
    }
    if((args[2] == "s" || args[2] == "S"||args[2] == "t" || args[2] == "T")&&pojedinacnaSLika==2){
      if(args[2] == "s" || args[2] == "S")pojedinacnaSLika=1;
      else pojedinacnaSLika=0;
    }
    else if((args[2] == "s" || args[2] == "S"||args[2] == "t" || args[2] == "T")&&pojedinacnaSLika!=2){
      Console.Error.WriteLine("Greska dva puta unesen parametar za izvor slike");
    }
    else if(args[2]=="C"||args[2]=="c"||args[2]=="R"||args[2]=="r"){
      if(args[2]=="C"||args[2]=="c")overwrite=0;
      else overwrite=1;
      if(pojedinacnaSlika==1){
        for(int i =3;i<args.Length;i++){
          JednaSlika(args[i],kontrast,osvtljenje,prvoKontrast,overwrite);
        }
        else if(pojedinacnaSlika==0){
          
        }
      }
    }
    else{
      Console.Error.WriteLine("Nepoynata komanda na trecem polju poziva");
      return;
    }
    if((args[3]=="C"||args[3]=="c"||args[3]=="R"||args[3]=="r")&&overwrite==2){
      if(args[3]=="C"||args[3]=="c")overwrite=0;
      else overwrite=1;
       if(pojedinacnaSlika==1){
        for(int i =4;i<args.Length;i++){
          JednaSlika(args[i],kontrast,osvtljenje,prvoKontrast,overwrite);
        }
         else if(pojedinacnaSlika==0){
           ProsledjivanjeListe(args[4],kontrast,osvetljenje,prvoKontrast,osvetljenje);
         }
    }
    else if((args[3]=="C"||args[3]=="c"||args[3]=="R"||args[3]=="r")&&overwrite!=2){
       Console.Error.WriteLine("Greska dva puta unesen parametar za cuvanje slike");
      return;
    }
    
    
    
  }
}