import { Cenovnik } from './Cenovnik';
import { Slika } from './Slika';
import { Ugovor } from './Ugovor';

export class Slucaj {
    id
    Naziv: String;
    Opis: String;
    Ugovor: Ugovor;
    slike: Slika[];
    KrajnjiRokZaOdgovor;
    PocetakRada;
    GSirina;
    Cenovniks: Cenovnik [];
    GDuzina;
    KorisnikId;
    kategorijaId;
}
