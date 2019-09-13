import { AdvokatService } from './../../service/advokat.service';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../service/auth.service';

@Component({
  selector: 'app-advokat-header',
  templateUrl: './advokat-header.component.html',
  styleUrls: ['./advokat-header.component.css']
})
export class AdvokatHeaderComponent implements OnInit {
  badgeCount;
  ulogovaniKorisnik;
  _type: string;
  dataType: string;

  constructor(private advokatService: AdvokatService, private auth: AuthService) {
    this._type = this.auth.typeUserValue;
    console.log(this.dataType);
   }

  ngOnInit() {
    this.ulogovaniKorisnik = localStorage.getItem('userName');
    this._type = this.auth.typeUserValue;
    this.advokatService.getNewNostifiation().subscribe( res => {
      console.log(res);
      this.badgeCount = res;
    });
  }
  private change(mytype : string) :void{
    this.dataType = mytype;
  }
  clearCount() {
    this.badgeCount = 0;
  }
}
