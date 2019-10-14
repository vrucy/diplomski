import { NotificationService } from './../../service/notification.service';
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

  constructor(private korisnikService: KorisnikService,private notificationService: NotificationService,  private auth: AuthService) {
    this._type = this.auth.typeUserValue;
    this.badgeCount = this.notificationService.count;
    console.log(this.badgeCount);
  }

  ngAfterViewInit(): void {

  }
  ngOnInit() {
    this.ulogovaniKorisnik = localStorage.getItem('userName');
    this.korisnikService.getNewNostifiation().subscribe( (res: any) => {
      this.badgeCount = res;
      this.badgeCount = res.length;
      console.log(this.badgeCount);
    });
  }
  clearCount() {
    if (this.notificationService.notifications.length !== 0) {
      // this.advokatService.putNostificationRead(this.newNostifation).subscribe();
      console.log(this.notificationService.notifications)
      this.badgeCount = 0;
    }
  }

}
