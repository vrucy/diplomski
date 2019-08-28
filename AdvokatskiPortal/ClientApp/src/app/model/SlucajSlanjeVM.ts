import { Advokat } from './Advokat';
import { Ugovor } from './Ugovor';
import { Cenovnik } from './Cenovnik';
export class SlucajSlanjeVM{
id;
opis: string;
ugovor: Ugovor;
advokati: Advokat;
cenovnik: Cenovnik;

}
