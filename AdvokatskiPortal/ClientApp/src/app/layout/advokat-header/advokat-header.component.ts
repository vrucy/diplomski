import { AdvokatService } from './../../service/advokat.service';
import { Component, OnInit, AfterContentInit } from '@angular/core';
import { AuthService } from '../../service/auth.service';

@Component({
  selector: 'app-advokat-header',
  templateUrl: './advokat-header.component.html',
  styleUrls: ['./advokat-header.component.css']
})
export class AdvokatHeaderComponent implements OnInit, AfterContentInit {

  badgeCount;
  ulogovaniKorisnik;
  _type: string;
  newNostifation;
  constructor(private advokatService: AdvokatService, private auth: AuthService) {
    this._type = this.auth.typeUserValue;
   }

   ngAfterContentInit(): void {
    this.advokatService.getNewNostifiation().subscribe( res => {
      console.log(res);
      this.newNostifation = res;
      this.badgeCount = res.length;
    });
  }
  ngOnInit() {
    this.ulogovaniKorisnik = localStorage.getItem('userName');
    this._type = localStorage.getItem('typeUser')
  }
  putReadTrue() {

    this.clearCount();
  }
  clearCount() {
    if ( this.newNostifation.length !== 0) {
      this.advokatService.putNostificationRead(this.newNostifation).subscribe();
      this.badgeCount = 0;
    }
  }
}
