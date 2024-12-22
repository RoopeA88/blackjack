using System.Dynamic;
using System.Security.Cryptography.X509Certificates;

namespace blackjack_peli{


public class Anna : Pelaaja{
    public override double Pelimerkit {get; set;} = 2500;
    public Anna(string nimi) : base(nimi)
    {
    
    
    }
    public  void erikoiskyky(){
        
            Console.WriteLine("Erikoiskyky aktivoitu");
            Console.WriteLine("Anna hymyilee leikillisesti jakajalle, katsoo syvälle silmiin ja samalla varastaa pelimerkkejä");
            Console.WriteLine("Pelikassasi suurenee 100 pelimerkillä");
            Pelimerkit +=100;
        }
    }

}
