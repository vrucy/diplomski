import { Advokat } from './Advokat';
import { Ugovor } from './Ugovor';
import { Cenovnik } from './Cenovnik';
export class SlucajSlanjeVM{
id;
Slucaj;
Ugovor: Ugovor;
Advokats: Advokat [] = [];
Cenovniks: Cenovnik [] = [];
KorisnikId;
Korisnik;
SlucajAdvokats;

}