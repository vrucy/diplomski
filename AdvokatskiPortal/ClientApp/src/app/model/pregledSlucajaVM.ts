import { Slucaj } from './Slucaj';
import { Ugovor } from './Ugovor';
import { Majstor } from './Majstor';
import { Cenovnik } from './Cenovnik';

export class pregledSlucajaVM {
  id;
  Slucaj: Slucaj ;
  Ugovor: Ugovor;
  Majstors: Majstor [] = [];
  Cenovniks: Cenovnik [] = [];
  KorisnikId;
  Korisnik;
  SlucajAdvokats;
  }

