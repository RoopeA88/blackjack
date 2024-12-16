// See https://aka.ms/new-console-template for more information
Random random = new Random();
string [] kortit = new string[]{"1h","2h","3h","4h","5h","6h","7h","8h","9h","10h","Jh","Qh","Kh",
"1s","2s","3s","4s","5s","6s","7s","8s","9s","10s","Js","Qs","Ks",
"1d","2d","3d","4d","5d","6d","7d","8d","9d","10d","J","Qd","Kd",
"1c","2c","3c","4c","5c","6c","7c","8c","9c","10c","Jc","Qc","Kc"};
List<string> kortit_listana = kortit.ToList();
int korttien_maara = 52;
int tilin_saldo = 1000;
int pelikassa = 0;
string jaettu_kortti;
int kortin_arvo;

string jaa_kortti(){

    int randomNumero = random.Next(0,korttien_maara);
    jaettu_kortti = kortit[randomNumero];
    kortit_listana.RemoveAt(randomNumero);
    korttien_maara -=1;
    
    return jaettu_kortti;
}
int kortin_arvo_(string kortti){
    if(kortti[0] == 'K'){
        kortin_arvo = 13;
    } else if(kortti[0] == 'Q'){
        kortin_arvo = 12;
    } else if(kortti[0] == 'J'){
        kortin_arvo = 11;
    } else if(kortti.Length == 3){
        kortin_arvo = 10;
    } else{
        kortin_arvo = kortti[0] -'0';
    }
    return kortin_arvo;
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
            Console.WriteLine("Hyvää onnea pelaaja!");
            
            
            
            string pelaajan_kortti1 = jaa_kortti();
            int pelaajan_kortti1_numerona = kortin_arvo_(pelaajan_kortti1);
            string pelaajan_kortti2 = jaa_kortti();
            int pelaajan_kortti2_numerona = kortin_arvo_(pelaajan_kortti2);
            string jakajan_avoin = jaa_kortti();
            int jakajan_avoin_numerona = kortin_arvo_(jakajan_avoin);
            string jakajan_pimea = jaa_kortti();
            int jakajan_pimea_numerona = kortin_arvo_(jakajan_pimea);

            Console.WriteLine($"Korttisi ovat {pelaajan_kortti1} ja {pelaajan_kortti2}");
            Console.WriteLine($"Jakajan avoin kortti on {jakajan_avoin}");
            int pelaajan_summa = pelaajan_kortti1_numerona + pelaajan_kortti2_numerona;
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
                    if(pelaajan_summa >21){
                        Console.WriteLine($"{pelaajan_summa}.Yli! hävisit.");
                        break;
                    } 

                    
            } else if(paatos == "2"){
                Console.WriteLine($"Jäät {pelaajan_summa}");
                Console.WriteLine($"Jakaja kääntää ympäri {jakajan_pimea}. Jakajalla on {jakajan_summa}");
                if(pelaajan_summa < jakajan_summa){
                    Console.WriteLine("Hävisit!");
                    kierroskaynnissa = false;
                } else{
                    while(jakajan_summa < pelaajan_summa){
                        string jakajan_lisakortti = jaa_kortti();
                        
                        Console.WriteLine($"Jakaja jakoi {jakajan_lisakortti}");
                        int jakajan_lisakortti_numerona = kortin_arvo_(jakajan_lisakortti);
                        jakajan_summa += jakajan_lisakortti_numerona;
                        if(jakajan_summa > pelaajan_summa && jakajan_summa <21){
                            Console.WriteLine($"Jakaja jakaa itselleen {jakajan_lisakortti}");
                            Console.WriteLine($"Hävisit! jakajalla on {jakajan_summa}");
                            kierroskaynnissa = false;
                        } else if(jakajan_summa > pelaajan_summa && jakajan_summa >21){
                            Console.WriteLine($"Voitit! jakajalla {jakajan_summa} Onneksi olkoon!");
                            kierroskaynnissa = false;
                        }
                    }
                }
            } else{
                continue;
            }
            }
        } else{
            continue;
        }
    }
}

blackjack();
//jou