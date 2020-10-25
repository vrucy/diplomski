import { Contract } from './Contrct';
import { Picture } from './Picture';
import { Ugovor } from './Ugovor';

export class Case {
    id
    Name: String;
    Description: String;
    //Description: Ugovor;
    Pictures: Picture[];
    DeadLineForAnswer;
    StartDate;
    GSirina;
    Contracts: Contract [];
    GDuzina;
    UserId;
    CategoryId;
}
