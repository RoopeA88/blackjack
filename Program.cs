// See https://aka.ms/new-console-template for more information
Random random = new Random();
string [] kortit = new string[]{"1h","2h","3h","4h","5h","6h","7h","8h","9h","10h","11h","12h","13h",
"1s","2s","3s","4s","5s","6s","7s","8s","9s","10s","11s","12s","13s",
"1d","2d","3d","4d","5d","6d","7d","8d","9d","10d","11d","12d","13d",
"1c","2c","3c","4c","5c","6c","7c","8c","9c","10c","11c","12c","13c"};
List<string> kortit_listana = kortit.ToList();
int korttien_maara = 52;
string jaa_kortti(){

int randomNumero = random.Next(0,korttien_maara);
string jaettu_kortti = kortit[randomNumero];
kortit_listana.RemoveAt(randomNumero);
korttien_maara -=1;
return jaettu_kortti;
}
