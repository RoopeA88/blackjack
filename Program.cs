﻿// See https://aka.ms/new-console-template for more information
Random random = new Random();
string [] kortit = new string[]{"1h","2h","3h","4h","5h","6h","7h","8h","9h","10h","11h","12h","13h",
"1s","2s","3s","4s","5s","6s","7s","8s","9s","10s","11s","12s","13s",
"1d","2d","3d","4d","5d","6d","7d","8d","9d","10d","11d","12d","13d",
"1c","2c","3c","4c","5c","6c","7c","8c","9c","10c","11c","12c","13c"};
List<string> kortit_listana = kortit.ToList();
int korttien_maara = 52;
int tilin_saldo = 1000;
int pelikassa = 0;

string jaa_kortti(){

    int randomNumero = random.Next(0,korttien_maara);
    string jaettu_kortti = kortit[randomNumero];
    kortit_listana.RemoveAt(randomNumero);
    korttien_maara -=1;
    return jaettu_kortti;
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
        Console.WriteLine("Tervetuloa Casinolle! Haluatko pelata blackjackia (b) vai tarvitseeko sinun nostaa rahaa? (r)");
        string valinta = Console.ReadLine();
        if (valinta == "r"){
            pankkiautomaatti();
        } else if (valinta == "b"){
            Console.WriteLine("Hyvää onnea pelaaja!");
            int pelaajan_summa;
            int jakajan_summa;
            string pelaajan_kortti1 = jaa_kortti();
            string pelaajan_kortti2 = jaa_kortti();
            string jakajan_avoin = jaa_kortti();
            string jakajan_pimea = jaa_kortti();
            Console.WriteLine($"Korttisi ovat {pelaajan_kortti1} ja {pelaajan_kortti2}");
            Console.WriteLine($"Jakajan avoin kortti on {jakajan_avoin}");
            int pelaajan_kortti1_numerona = pelaajan_kortti1[0] - '0';
            int pelaajan_kortti2_numerona = pelaajan_kortti2[0] - '0';
            int jakajan_avoin_numerona = jakajan_avoin[0] - '0';
            int jakajan_pimea_numerona = jakajan_pimea[0] - '0';
            pelaajan_summa = pelaajan_kortti1_numerona + pelaajan_kortti2_numerona;
            jakajan_summa = jakajan_avoin_numerona + jakajan_pimea_numerona;
            while(kierroskaynnissa){
                Console.WriteLine($"Sinulla on {pelaajan_summa}, haluatko ottaa kortin (1) jäädä tähän (2)");
                string paatos = Console.ReadLine();
                if(paatos == "1"){
                    
                    string lisakortti = jaa_kortti();
                    Console.WriteLine($"Jakaja jakaa pelaajalle {lisakortti}");
                    int lisakortti_numerona = lisakortti[0] - '0';
                    pelaajan_summa += lisakortti_numerona;
                    if(pelaajan_summa >21){
                        Console.WriteLine("Yli! hävisit.");
                        break;
                    } 

                    
            } else if(paatos == "2"){
                Console.WriteLine($"Jäät {pelaajan_summa}");
                Console.WriteLine($"Jakajalla on {jakajan_summa}");
                if(pelaajan_summa < jakajan_summa){
                    Console.WriteLine("Hävisit!");
                    kierroskaynnissa = false;
                } else{
                    while(jakajan_summa < pelaajan_summa){
                        string jakajan_lisakortti = jaa_kortti();
                        
                        Console.WriteLine($"Jakaja jakoi{jakajan_lisakortti}");
                        int jakajan_lisakortti_numerona = jakajan_lisakortti[0] - '0';
                        jakajan_summa += jakajan_lisakortti_numerona;
                        if(jakajan_summa > pelaajan_summa && jakajan_summa <21){
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