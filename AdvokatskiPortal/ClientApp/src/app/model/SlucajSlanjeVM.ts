import { Slucaj } from './Slucaj';
import { Majstor } from './Majstor';
import { Ugovor } from './Ugovor';
import { Cenovnik } from './Cenovnik';
export class SlucajSlanjeVM {
id;
Slucaj: Slucaj ;
Ugovor: Ugovor;
Advokats: Majstor [] = [];
Cenovniks: Cenovnik [] = [];
KorisnikId;
Korisnik;
SlucajAdvokats;

}

