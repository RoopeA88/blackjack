// See https://aka.ms/new-console-template for more information
using System.Diagnostics.Contracts;

namespace blackjack_peli{
    class Program{

    

static Random random = new Random();
static string [] kortit = new string[]{"Ah","2h","3h","4h","5h","6h","7h","8h","9h","10h","Jh","Qh","Kh",
"As","2s","3s","4s","5s","6s","7s","8s","9s","10s","Js","Qs","Ks",
"Ad","2d","3d","4d","5d","6d","7d","8d","9d","10d","Jd","Qd","Kd",
"Ac","2c","3c","4c","5c","6c","7c","8c","9c","10c","Jc","Qc","Kc"};
static List<string> kortit_listana = kortit.ToList();
static int korttien_maara = 52;


static string jaettu_kortti;
static int kortin_arvo;
static int laskuri;
static int onko_assa_apuri;
static int erikoiskykylaskuri_pentti;
static int erikoiskykylaskuri_anna;
static int erikoiskykylaskuri_jarkko;
static Pelaaja pelaaja = null;
public static void sekoita(){
    kortit_listana = kortit.ToList();
    int pakan_koko = kortit_listana.Count;
    Console.WriteLine($"kortteja pakassa: {pakan_koko}");
}
public static string jaa_kortti(){

    int randomNumero = random.Next(0,korttien_maara);
    jaettu_kortti = kortit_listana[randomNumero];
    kortit_listana.RemoveAt(randomNumero);
    korttien_maara -=1;
    
    return jaettu_kortti;
}
public static int kortin_arvo_(string kortti){
    if(kortti[0] == 'K' || kortti[0] == 'Q' || kortti[0] == 'J' || kortti.Length == 3){
        kortin_arvo = 10;
    } 
     else if(kortti[0] == 'A'){
        kortin_arvo = 11;
    } else{
        kortin_arvo = kortti[0] -'0';
    }
    return kortin_arvo;
}
public static bool panostus(int panos){
    if(panos % 10 == 0 && pelaaja.Pelimerkit >= panos && panos >0){
        
        return true;
    } else{
        return false;
    }
}

static void blackjack(){
    
    bool kierroskaynnissa = true;
    
    while(pelaaja == null){
     Console.WriteLine("Valitse hahmo: Anna (a), Jarkko (j) tai Pentti (p)");
        string hahmo = Console.ReadLine();
        if(hahmo == "a"){
            pelaaja = new Anna("Anna");
            Console.WriteLine($"Tervetuloa Casinolle, {pelaaja.Nimi}!");
            
        } else if(hahmo == "j"){
            pelaaja = new Jarkko("Jarkko");
            Console.WriteLine($"Tervetuloa Casinolle, {pelaaja.Nimi}!");
            
        } else if (hahmo == "p"){
            pelaaja = new Pentti("Pentti");
            Console.WriteLine($"Tervetuloa Casinolle, {pelaaja.Nimi}!");
            
        } else{
            Console.WriteLine("Sinun on valittava hahmo pelataksesi!");
            
        }
}
    while(pelaaja != null){
       Console.WriteLine($"Hahmollasi on {pelaaja.Pelimerkit} pelimerkkiä. Anna komento 'b' pelataksesi tai komento 'l' lopettaaksesi pelin.");
        
        
        string valinta = Console.ReadLine();
        if (valinta == "l"){
            break;
        }
       else if (valinta == "b"){
            Console.WriteLine($"Aseta panos, pelikassasi on {pelaaja.Pelimerkit} euroa.");
            string panos = Console.ReadLine();
            int panos_lukuna = int.Parse(panos);
            if(panostus(panos_lukuna) == false){
                Console.WriteLine("Panos ei kelpaa. pitää olla kymmenlukujen välein (10, 20, 30...100...) tai sinulla ei ole tarpeeksi rahaa pelikassassa.");
                continue;
            } else{
                erikoiskykylaskuri_pentti+=1;
                erikoiskykylaskuri_anna+=1;
                laskuri+=1;
                if(laskuri == 5){
                    sekoita();
                    Console.WriteLine("Pakka sekoitettu");
                    laskuri = 0;
                }
            if(pelaaja is Anna anna && erikoiskykylaskuri_anna == 10){
                anna.erikoiskyky();
            
            }
            
            Console.WriteLine($"Panos on {panos_lukuna} euroa. Hyvää onnea {pelaaja.Nimi}!");
            
            
            string pelaajan_kortti1 = jaa_kortti();
            int pelaajan_kortti1_numerona = kortin_arvo_(pelaajan_kortti1);
            string pelaajan_kortti2 = jaa_kortti();
            int pelaajan_kortti2_numerona = kortin_arvo_(pelaajan_kortti2);
            
            string jakajan_avoin = jaa_kortti();
            int jakajan_avoin_numerona = kortin_arvo_(jakajan_avoin);
            string jakajan_pimea = jaa_kortti();
            int jakajan_pimea_numerona = kortin_arvo_(jakajan_pimea);
            int pelaajan_summa = pelaajan_kortti1_numerona + pelaajan_kortti2_numerona;
            int onko_assa = 0;
            
            Console.WriteLine($"Korttisi ovat {pelaajan_kortti1} ja {pelaajan_kortti2}");
            
            if(pelaajan_kortti1[0] == 'A' || pelaajan_kortti2[0] == 'A'){
                onko_assa = 1;
            }
            if(pelaajan_summa == 21){
                Console.WriteLine($"Blackjack! Onneksi olkoon, voitit {2*panos_lukuna} euroa.");
                pelaaja.Pelimerkit+= + (2*panos_lukuna);
                Console.WriteLine($"Pelikassassa on {pelaaja.Pelimerkit} euroa.");
                continue;
            }
            Console.WriteLine($"Jakajan avoin kortti on {jakajan_avoin}");
            if(jakajan_avoin_numerona == 11){
                Console.WriteLine("Jakajalla mahdollisuus Blackjackiin!");
            }
            
            int jakajan_summa = jakajan_avoin_numerona + jakajan_pimea_numerona;
            kierroskaynnissa = true;
            onko_assa_apuri = 0;
            
            
            
            while(kierroskaynnissa){
                Console.WriteLine($"Sinulla on {pelaajan_summa}, haluatko ottaa kortin (1) jäädä tähän (2)");
                int lisakortti_numerona;
                string paatos = Console.ReadLine();
                if(paatos == "1"){
                    if(pelaaja.Nimi =="Pentti" && erikoiskykylaskuri_pentti == 10){
                        lisakortti_numerona = Pentti.erikoiskyky();
                        pelaajan_summa += lisakortti_numerona;
                    } else{
                    string lisakortti = jaa_kortti();
                    Console.WriteLine($"Jakaja jakaa pelaajalle {lisakortti}");
                    lisakortti_numerona = kortin_arvo_(lisakortti);
                    
                    
                    pelaajan_summa += lisakortti_numerona;
                    }
                    if(onko_assa == 1 && pelaajan_summa > 21 && onko_assa_apuri == 0){
                        pelaajan_summa-= 10;
                        onko_assa_apuri = 1;
                    }
                    if(lisakortti_numerona == 11 && pelaajan_summa > 21){
                        pelaajan_summa -= 10;
                    }
                    if(pelaaja.Nimi == "Pentti" && erikoiskykylaskuri_jarkko == 10 && pelaajan_summa >21){
                        Console.WriteLine($"Menit yli, korttien arvo on {pelaajan_summa} ");
                        int erikoiskyvyn_uusi_kortti = Jarkko.erikoiskyky();
                        
                        pelaajan_summa-=lisakortti_numerona;
                        pelaajan_summa+= erikoiskyvyn_uusi_kortti;
                        erikoiskykylaskuri_jarkko = 0;
                        if(pelaajan_summa >21){
                            Console.WriteLine($"Summa uuden kortin jälkeen {pelaajan_summa}, ei voi mitään, hävisit superkyvystä huolimatta.");
                        }
                    }
                    if(pelaajan_summa >21){
                        Console.WriteLine($"{pelaajan_summa}.Yli! hävisit.");
                        pelaaja.Pelimerkit-= panos_lukuna;
                        Console.WriteLine($"Pelikassassa on {pelaaja.Pelimerkit} euroa.");
                        kierroskaynnissa = false;
                    } 

                    
            } else if(paatos == "2"){
                Console.WriteLine($"Jäät {pelaajan_summa}");
                Console.WriteLine($"Jakaja kääntää ympäri {jakajan_pimea}. Jakajalla on {jakajan_summa}");
                if(pelaajan_summa < jakajan_summa){
                    Console.WriteLine("Hävisit!");
                    pelaaja.Pelimerkit -= panos_lukuna;
                    Console.WriteLine($"Pelikassassa on {pelaaja.Pelimerkit} euroa.");
                    kierroskaynnissa = false;
                } else if(pelaajan_summa == jakajan_summa && jakajan_summa >16){
                    Console.WriteLine("Tasapeli! panos palautettu.");
                    
                    Console.WriteLine($"Pelikassassa on {pelaaja.Pelimerkit} euroa.");
                    kierroskaynnissa = false;
                } else{
                    while(jakajan_summa < 17){
                        string jakajan_lisakortti = jaa_kortti();
                        
                        Console.WriteLine($"Jakaja jakoi {jakajan_lisakortti}");
                        int jakajan_lisakortti_numerona = kortin_arvo_(jakajan_lisakortti);
                        jakajan_summa += jakajan_lisakortti_numerona;
                        if(jakajan_lisakortti_numerona == 11 && jakajan_summa >21){
                            jakajan_summa-= 10;
                            continue;
                            
                        }
                        else if(jakajan_summa > pelaajan_summa && jakajan_summa <=21){
                            
                            Console.WriteLine($"Hävisit! jakajalla on {jakajan_summa}");
                            pelaaja.Pelimerkit-= panos_lukuna;
                            Console.WriteLine($"Pelikassassa on {pelaaja.Pelimerkit} euroa.");
                            kierroskaynnissa = false;
                        } else if(jakajan_summa > pelaajan_summa && jakajan_summa >21){
                            Console.WriteLine($"Voitit! jakajalla {jakajan_summa} Onneksi olkoon!");
                            pelaaja.Pelimerkit+= panos_lukuna;
                            Console.WriteLine($"Pelikassassa on {pelaaja.Pelimerkit} euroa.");
                            kierroskaynnissa = false;
                        } else if(jakajan_summa < pelaajan_summa && jakajan_summa > 16){
                            Console.WriteLine($"Jakaja jää {jakajan_summa}. Voitit! onneksi olkoon.");
                            pelaaja.Pelimerkit+= panos_lukuna;
                            Console.WriteLine($"Pelikassassa on {pelaaja.Pelimerkit} euroa.");
                            kierroskaynnissa = false;
                        } else if(jakajan_summa < pelaajan_summa && jakajan_summa <17){
                            continue;
                        } else if(jakajan_summa == pelaajan_summa && jakajan_summa <17){
                            continue;
                        } else if(jakajan_summa == pelaajan_summa && jakajan_summa >16){
                            Console.WriteLine("Tasapeli! Panos palautettu");
                            
                            Console.WriteLine($"Pelikassassa on {pelaaja.Pelimerkit} euroa.");
                            kierroskaynnissa = false;
                        }
                    }
                }
            } else{
                continue;
            }
            }
            }
        } else{
            continue;
        }
    }
}


    



    static void Main(){
        blackjack();
    }
}
}


//jou