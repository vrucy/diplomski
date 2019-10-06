import { KorisnikService } from './../../service/korisnik.service';
import { Component, OnInit,  AfterViewInit } from '@angular/core';
import { AuthService } from '../../service/auth.service';

@Component({
  selector: 'app-korisnik-header',
  templateUrl: './korisnik-header.component.html',
  styleUrls: ['./korisnik-header.component.css']
})
export class KorisnikHeaderComponent implements OnInit, AfterViewInit {
  specjalnost;

  dataType: string;
  ulogovaniKorisnik = {};
  regular: string;
  _type: string;
  badgeCount;

  constructor(private korisnikService: KorisnikService, private auth: AuthService) {
    this._type = this.auth.typeUserValue;
  }

  ngAfterViewInit(): void {
    this.korisnikService.getNewNostifiation().subscribe( res => {
      this.badgeCount = res;
    });
  }
  ngOnInit() {
    this.ulogovaniKorisnik = localStorage.getItem('userName');

  }
  clearCount() {
    this.badgeCount = 0;
  }

}
