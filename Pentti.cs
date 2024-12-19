using System.Security.Cryptography.X509Certificates;

namespace blackjack_peli{


public class Pentti{
    public string Nimi {get; private set;}
    public double Pelimerkit{get; set;} = 2500;
    public Pentti(string nimi_){
    Nimi = nimi_;
    }
    public void erikoiskyky(int laskuri){
        if(laskuri == 10){
            Console.WriteLine("Erikoiskyky aktivoitu");
            Console.WriteLine("Anna hymyilee leikillisesti jakajalle, katsoo syvälle silmiin ja samalla varastaa pelimerkkejä");
            Console.WriteLine("Pelikassasi suurenee 100 pelimerkillä");
            Program.pelikassa += 100;
        }
    }

}
}