// See https://aka.ms/new-console-template for more information
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

string jaa_kortti(){

    int randomNumero = random.Next(0,korttien_maara);
    jaettu_kortti = kortit_listana[randomNumero];
    kortit_listana.RemoveAt(randomNumero);
    korttien_maara -=1;
    
    return jaettu_kortti;
}
int kortin_arvo_(string kortti){
    if(kortti[0] == 'K'){
        kortin_arvo = 10;
    } else if(kortti[0] == 'Q'){
        kortin_arvo = 10;
    } else if(kortti[0] == 'J'){
        kortin_arvo = 10;
    } else if(kortti.Length == 3){
        kortin_arvo = 10;
    } else if(kortti[0] == 'A'){
        kortin_arvo = 11;
    } else{
        kortin_arvo = kortti[0] -'0';
    }
    return kortin_arvo;
}
bool panostus(int panos){
    if(panos % 10 == 0 && pelikassa >= panos){
        
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
    bool kierroskaynnissa = true;
    while(true){
        Console.WriteLine("Tervetuloa Casinolle! Haluatko pelata blackjackia (b) vai tarvitseeko sinun nostaa rahaa? (r), lopeta (l)");
        string valinta = Console.ReadLine();
        if (valinta == "l"){
            break;
        }
        if (valinta == "r"){
            pankkiautomaatti();
        } else if (valinta == "b"){
            Console.WriteLine($"Aseta panos, pelikassasi on {pelikassa} euroa.");
            string panos = Console.ReadLine();
            int panos_lukuna = int.Parse(panos);
            if(panostus(panos_lukuna) == false){
                Console.WriteLine("Panos ei kelpaa. pitää olla kymmenlukujen välein (10, 20, 30...100...) tai sinulla ei ole tarpeeksi rahaa pelikassassa.");
                continue;
            } else{
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
                Console.WriteLine($"Blackjack! Onneksi olkoon, voitit {3*panos_lukuna} euroa.");
                pelikassa = pelikassa + (3*panos_lukuna);
                Console.WriteLine($"Pelikassassa on {pelikassa} euroa.");
                continue;
            }
            Console.WriteLine($"Jakajan avoin kortti on {jakajan_avoin}");
            if(jakajan_avoin_numerona == 11){
                Console.WriteLine("Jakajalla mahdollisuus Blackjackiin!");
            }
            
            int jakajan_summa = jakajan_avoin_numerona + jakajan_pimea_numerona;
            kierroskaynnissa = true;
           
            
            
            
            while(kierroskaynnissa){
                Console.WriteLine($"Sinulla on {pelaajan_summa}, haluatko ottaa kortin (1) jäädä tähän (2)");
                string paatos = Console.ReadLine();
                if(paatos == "1"){
                    
                    string lisakortti = jaa_kortti();
                    Console.WriteLine($"Jakaja jakaa pelaajalle {lisakortti}");
                    int lisakortti_numerona = kortin_arvo_(lisakortti);
                    
                    pelaajan_summa += lisakortti_numerona;
                    if(onko_assa == 1 && pelaajan_summa > 21){
                        pelaajan_summa-= 10;
                    }
                    if(lisakortti_numerona == 11 && pelaajan_summa > 21){
                        pelaajan_summa -= 10;
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
                    pelikassa+= panos_lukuna;
                    Console.WriteLine($"Pelikassassa on {pelikassa} euroa.");
                } else{
                    while(jakajan_summa < pelaajan_summa || jakajan_summa == pelaajan_summa){
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
                            pelikassa+= (2*panos_lukuna);
                            Console.WriteLine($"Pelikassassa on {pelikassa} euroa.");
                            kierroskaynnissa = false;
                        } else if(jakajan_summa < pelaajan_summa && jakajan_summa > 16){
                            Console.WriteLine($"Jakaja jää {jakajan_summa}. Voitit! onneksi olkoon.");
                            pelikassa+= (2*panos_lukuna);
                            Console.WriteLine($"Pelikassassa on {pelikassa} euroa.");
                            kierroskaynnissa = false;
                        } else if(jakajan_summa < pelaajan_summa && jakajan_summa <17){
                            continue;
                        } else if(jakajan_summa == pelaajan_summa && jakajan_summa <17){
                            continue;
                        } else if(jakajan_summa == pelaajan_summa && jakajan_summa >16){
                            Console.WriteLine("Tasapeli! Panos palautettu");
                            pelikassa+= panos_lukuna;
                            Console.WriteLine($"Pelikassassa on {pelikassa} euroa.");
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