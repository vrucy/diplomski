import { Component } from '@angular/core';
import { KorisnikService } from '../../service/korisnik.service';

@Component({
  selector: 'app-kreiranje-slucaja',
  templateUrl: './kreiranje-slucaja.component.html',
  styleUrls: ['./kreiranje-slucaja.component.css']
})
export class KreiranjeSlucajaComponent {
  slucaj = {opis: ''};
  constructor(private korisnikService: KorisnikService) { }

  kreiranjeSlucaja(){
    this.korisnikService.kreiranjeSlucaja(this.slucaj);
    console.log(this.slucaj)
  }
  
}
