import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../service/auth.service';

@Component({
  selector: 'app-korisnik-header',
  templateUrl: './korisnik-header.component.html',
  styleUrls: ['./korisnik-header.component.css']
})
export class KorisnikHeaderComponent implements OnInit {

  specjalnost;

  dataType: string;
  ulogovaniKorisnik = {};
  regular: string;
  _type: string;


  constructor( private auth: AuthService) {
    this._type = this.auth.typeUserValue;

    console.log(localStorage.getItem('user'));
  }


    private change(mytype: string): void {
      this.dataType = mytype;
    }

  ngOnInit() {
    this.ulogovaniKorisnik = localStorage.getItem('userName');

  }


}
