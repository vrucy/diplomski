import { Slika } from './Slika';
import { Ugovor } from './Ugovor';

export class Slucaj {
    id
    Naziv: String;
    Opis: String;
    Ugovor : Ugovor;
    Slike: Slika[];
    KrajnjiRokZaOdgovor;
    PocetakRada;
    ZavrsetakRada;
    GSirina;
    GDuzina;
    KorisnikId;
}
