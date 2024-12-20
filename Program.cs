// See https://aka.ms/new-console-template for more information
using System.Diagnostics.Contracts;

namespace blackjack_peli{
    class Program{

    

Random random = new Random();
static string [] kortit = new string[]{"Ah","2h","3h","4h","5h","6h","7h","8h","9h","10h","Jh","Qh","Kh",
"As","2s","3s","4s","5s","6s","7s","8s","9s","10s","Js","Qs","Ks",
"Ad","2d","3d","4d","5d","6d","7d","8d","9d","10d","Jd","Qd","Kd",
"Ac","2c","3c","4c","5c","6c","7c","8c","9c","10c","Jc","Qc","Kc"};
List<string> kortit_listana = kortit.ToList();
int korttien_maara = 52;
int tilin_saldo = 1000;
public static double pelikassa = 0;
string jaettu_kortti;
int kortin_arvo;
int laskuri;
int onko_assa_apuri;
int erikoiskykylaskuri;
void sekoita(){
    kortit_listana = kortit.ToList();
    int pakan_koko = kortit_listana.Count;
    Console.WriteLine($"kortteja pakassa: {pakan_koko}");
}
public string jaa_kortti(){

    int randomNumero = random.Next(0,korttien_maara);
    jaettu_kortti = kortit_listana[randomNumero];
    kortit_listana.RemoveAt(randomNumero);
    korttien_maara -=1;
    
    return jaettu_kortti;
}
public int kortin_arvo_(string kortti){
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
bool panostus(int panos){
    if(panos % 10 == 0 && pelikassa >= panos && panos >0){
        
        return true;
    } else{
        return false;
    }
}



void pankkiautomaatti(){
    Console.WriteLine($"Kuinka paljon nostetaan? tilin saldo on {tilin_saldo} euroa.");
    string nostosumma = Console.ReadLine();
    int nostosumma_lukuna = int.Parse(nostosumma);
    if(nostosumma_lukuna > tilin_saldo){
        Console.WriteLine("Tilillä ei ole tarpeeksi rahaa");
    } else{
        pelikassa+=nostosumma_lukuna;
        Console.WriteLine($"pelikassassa on nyt {pelikassa} euroa");
    }

}

void blackjack(){
    Pelaaja pelaaja = null;
    bool kierroskaynnissa = true;
    while(true){
        Console.WriteLine("Valitse hahmo: Anna (a), Jarkko (j) tai Pentti (p)");
        string hahmo = Console.ReadLine();
        if(hahmo == "a"){
            pelaaja = new Anna("Anna");
            Console.WriteLine($"Tervetuloa Casinolle, {pelaaja.Nimi}! Hahmollasi on {pelaaja.Pelimerkit} pelimerkkiä. Anna komento 'b' pelataksesi tai komento 'l' lopettaaksesi pelin.");
        } else if(hahmo == "j"){
            pelaaja = new Jarkko("Jaakko");
            Console.WriteLine($"Tervetuloa Casinolle, {pelaaja.Nimi}! Hahmollasi on {pelaaja.Pelimerkit} pelimerkkiä. Anna komento 'b' pelataksesi tai komento 'l' lopettaaksesi pelin.");
        } else if (hahmo == "p"){
            pelaaja = new Pentti("Pentti");
            Console.WriteLine($"Tervetuloa Casinolle, {pelaaja.Nimi}! Hahmollasi on {pelaaja.Pelimerkit} pelimerkkiä. Anna komento 'b' pelataksesi tai komento 'l' lopettaaksesi pelin.");
        } else{
            Console.WriteLine("Sinun on valittava hahmo pelataksesi!");
            continue;
        }
        
        
        string valinta = Console.ReadLine();
        if (valinta == "l"){
            break;
        }
        if (valinta == "r"){
            pankkiautomaatti();
        } else if (valinta == "b"){
            Console.WriteLine($"Aseta panos, pelikassasi on {pelaaja.Pelimerkit} euroa.");
            string panos = Console.ReadLine();
            int panos_lukuna = int.Parse(panos);
            if(panostus(panos_lukuna) == false){
                Console.WriteLine("Panos ei kelpaa. pitää olla kymmenlukujen välein (10, 20, 30...100...) tai sinulla ei ole tarpeeksi rahaa pelikassassa.");
                continue;
            } else{
                erikoiskykylaskuri+=1;
                if(laskuri == 5){
                    sekoita();
                    laskuri = 0;
                }
                laskuri+=1;
            Console.WriteLine($"Panos on {panos_lukuna} euroa. Hyvää onnea pelaaja!");
            
            
            
            
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
                pelikassa = pelikassa + (2*panos_lukuna);
                Console.WriteLine($"Pelikassassa on {pelikassa} euroa.");
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
                string paatos = Console.ReadLine();
                if(paatos == "1"){
                    
                    string lisakortti = jaa_kortti();
                    Console.WriteLine($"Jakaja jakaa pelaajalle {lisakortti}");
                    int lisakortti_numerona = kortin_arvo_(lisakortti);
                    
                    pelaajan_summa += lisakortti_numerona;
                    if(onko_assa == 1 && pelaajan_summa > 21 && onko_assa_apuri == 0){
                        pelaajan_summa-= 10;
                        onko_assa_apuri = 1;
                    }
                    if(lisakortti_numerona == 11 && pelaajan_summa > 21){
                        pelaajan_summa -= 10;
                    }
                    if(pelaaja.Nimi == "Pentti" && erikoiskykylaskuri == 10 && pelaajan_summa >21){
                        Console.WriteLine($"Menit yli, korttien arvo on {pelaajan_summa} ");
                        int erikoiskyvyn_uusi_kortti = Pentti.erikoiskyky();
                        Console.WriteLine($"Superkyky aktivoitu! Sinulle jaettiin uusi kortti, jonka arvo on {erikoiskyvyn_uusi_kortti} ");
                        pelaajan_summa-=lisakortti_numerona;
                        pelaajan_summa+= erikoiskyvyn_uusi_kortti;
                        erikoiskykylaskuri = 0;
                        if(pelaajan_summa >21){
                            Console.WriteLine($"Summa uuden kortin jälkeen {pelaajan_summa}, ei voi mitään, hävisit superkyvystä huolimatta.");
                        }
                    }
                    if(pelaajan_summa >21){
                        Console.WriteLine($"{pelaajan_summa}.Yli! hävisit.");
                        pelikassa-= panos_lukuna;
                        Console.WriteLine($"Pelikassassa on {pelikassa} euroa.");
                        kierroskaynnissa = false;
                    } 

                    
            } else if(paatos == "2"){
                Console.WriteLine($"Jäät {pelaajan_summa}");
                Console.WriteLine($"Jakaja kääntää ympäri {jakajan_pimea}. Jakajalla on {jakajan_summa}");
                if(pelaajan_summa < jakajan_summa){
                    Console.WriteLine("Hävisit!");
                    pelikassa -= panos_lukuna;
                    Console.WriteLine($"Pelikassassa on {pelikassa} euroa.");
                    kierroskaynnissa = false;
                } else if(pelaajan_summa == jakajan_summa && jakajan_summa >16){
                    Console.WriteLine("Tasapeli! panos palautettu.");
                    
                    Console.WriteLine($"Pelikassassa on {pelikassa} euroa.");
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
                            pelikassa-= panos_lukuna;
                            Console.WriteLine($"Pelikassassa on {pelikassa} euroa.");
                            kierroskaynnissa = false;
                        } else if(jakajan_summa > pelaajan_summa && jakajan_summa >21){
                            Console.WriteLine($"Voitit! jakajalla {jakajan_summa} Onneksi olkoon!");
                            pelikassa+= panos_lukuna;
                            Console.WriteLine($"Pelikassassa on {pelikassa} euroa.");
                            kierroskaynnissa = false;
                        } else if(jakajan_summa < pelaajan_summa && jakajan_summa > 16){
                            Console.WriteLine($"Jakaja jää {jakajan_summa}. Voitit! onneksi olkoon.");
                            pelikassa+= panos_lukuna;
                            Console.WriteLine($"Pelikassassa on {pelikassa} euroa.");
                            kierroskaynnissa = false;
                        } else if(jakajan_summa < pelaajan_summa && jakajan_summa <17){
                            continue;
                        } else if(jakajan_summa == pelaajan_summa && jakajan_summa <17){
                            continue;
                        } else if(jakajan_summa == pelaajan_summa && jakajan_summa >16){
                            Console.WriteLine("Tasapeli! Panos palautettu");
                            
                            Console.WriteLine($"Pelikassassa on {pelikassa} euroa.");
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
        Program ohjelma = new Program();
        ohjelma.blackjack();
    }
}
}


//jou