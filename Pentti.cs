using System.Security.Cryptography.X509Certificates;

namespace blackjack_peli{


public class Pentti : Pelaaja{
    
    public override double Pelimerkit{get; set;} = 2000;
    public Pentti(string nimi) : base(nimi){
    
    }
    public static int erikoiskyky(){
        
            
            string kortti1 = Program.jaa_kortti();
            int kortti1_numerona = Program.kortin_arvo_(kortti1);
            string kortti2 = Program.jaa_kortti();
            int kortti2_numerona = Program.kortin_arvo_(kortti2);
            while(true){
            Console.WriteLine("Erikoiskyky aktivoitu! Saat valita seuraavasti 2 jaettavasta kortista yhden.");
            Console.WriteLine($"Vaihtoedhot ovat {kortti1} (1), jonka arvo on {kortti1_numerona}, tai {kortti2} (2), jonka arvo numerona on {kortti2_numerona}. Ässän arvo on 1 tai 11, riippuenkumpi on sinulle otollisempi");
            string valinta = Console.ReadLine();
            if(valinta == "1"){
                return kortti1_numerona;
            } else if(valinta == "2"){
                return kortti2_numerona;
            } else{
                Console.WriteLine("Virheellinen valinta! vaihtoehdot ovat (1) tai (2)");
                continue;
            }
    }

}
}
}