import { NotificationService } from './../../service/notification.service';
import { AdvokatService } from './../../service/advokat.service';
import { Component, OnInit, AfterContentInit, Output, Input } from '@angular/core';
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
  @Input() notification: string[];
  newNostifation;
  constructor(private advokatService: AdvokatService, private auth: AuthService, private notificationService: NotificationService) {
    this._type = this.auth.typeUserValue;
    this.badgeCount = this.notificationService.count;
  }

  ngAfterContentInit(): void {
  }

  ngOnInit() {
    this.ulogovaniKorisnik = localStorage.getItem('userName');
    this._type = localStorage.getItem('typeUser');
    this.advokatService.getNewNostifiation().subscribe( (res: any) => {
      this.notificationService.setNotifications(res);
      this.badgeCount = this.notificationService.count;
      console.log(this.badgeCount);
    });
  }

  clearCount() {
    if (this.badgeCount !== 0) {
      this.badgeCount = 0;
    }
  }
}
