import { Component, OnInit } from '@angular/core';
import { KorisnikService } from '../../service/korisnik.service';

@Component({
  selector: 'app-kreiranje-slucaja',
  templateUrl: './kreiranje-slucaja.component.html',
  styleUrls: ['./kreiranje-slucaja.component.css']
})
export class KreiranjeSlucajaComponent implements OnInit {
  slucaj = {opis: ''};
  constructor(private korisnikService: KorisnikService) { }

  ngOnInit() {
  }
  kreiranjeSlucaja(){
    this.korisnikService.kreiranjeSlucaja(this.slucaj);
    console.log(this.slucaj)
  }
  snimiSlucaj(){

  }
}
