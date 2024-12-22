using System.Security.Cryptography.X509Certificates;

namespace blackjack_peli{


public class Jarkko : Pelaaja{
    
    public override double Pelimerkit{get; set;} = 1500;
    public Jarkko(string nimi) : base(nimi){
    
    }
    public static int erikoiskyky(){
        string jarkon_lisakortti = Program.jaa_kortti();
        int jarkon_lisakortti_numerona = Program.kortin_arvo_(jarkon_lisakortti);
        Console.WriteLine($"Superkyky aktivoitu! Sinulle jaettiin uusi kortti {jarkon_lisakortti}, jonka arvo on {jarkon_lisakortti_numerona} ");
        return jarkon_lisakortti_numerona;
       
    }

}
}