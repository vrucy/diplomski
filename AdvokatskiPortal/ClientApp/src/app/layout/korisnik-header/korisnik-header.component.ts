import { AdvokatService } from './../../service/advokat.service';
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

  constructor(private korisnikService: KorisnikService,private notificationService: NotificationService,
              private auth: AuthService, private advokatService:AdvokatService) {
    this._type = this.auth.typeUserValue;
    this.badgeCount = this.notificationService.count;
    console.log(this.notificationService.count);
  }

  ngAfterViewInit(): void {

  }
  ngOnInit() {
    this.ulogovaniKorisnik = localStorage.getItem('userName');
    // this.korisnikService.resetNotification().subscribe(res => {
    //   console.log(res);
    // });
    this.advokatService.getNewNostifiation().subscribe( (res: any) => {
      this.notificationService.setNotifications(res);
      this.badgeCount = this.notificationService.count;
      console.log(this.badgeCount);
    });
  }
  clearCount() {
    console.log(this.notificationService.count);
    if (this.notificationService.notifications.length !== 0) {
      // this.advokatService.putNostificationRead(this.newNostifation).subscribe();
      this.badgeCount = 0;
    }
  }

}
