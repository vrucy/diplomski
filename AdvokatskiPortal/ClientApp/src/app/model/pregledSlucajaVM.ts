import { Case } from './Case';
import { Ugovor } from './Ugovor';
import { Craftman } from './Craftman'
import { Contract } from './Contrct';

export class pregledSlucajaVM {
  id;
  Case: Case ;
  Ugovor: Ugovor;
  Craftmans: Craftman [] = [];
  Contracts: Contract [] = [];
  UserId;
  User;
  CaseCraftmans;
  }

