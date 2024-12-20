using System.Security.Cryptography.X509Certificates;

namespace blackjack_peli{


public class Pentti : Pelaaja{
    
    public override double Pelimerkit{get; set;} = 2000;
    public Pentti(string nimi) : base(nimi){
    
    }
    public static int erikoiskyky(){
        
            Program ohjelma1 = new Program();
            string kortti = ohjelma1.jaa_kortti();
            int kortti_numerona = ohjelma1.kortin_arvo_(kortti);
            return kortti_numerona;
        
    }

}
}