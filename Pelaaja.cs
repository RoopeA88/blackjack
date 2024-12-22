using System.Security.Cryptography.X509Certificates;

namespace blackjack_peli{


public class Pelaaja{
    public string Nimi {get; private set;}
    public virtual double Pelimerkit {get;set;} = 1000;
    public Pelaaja(string nimi_){
    Nimi = nimi_;
    }
    
    }

}
