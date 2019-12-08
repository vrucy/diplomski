import { NotificationService } from '../../service/notification.service';
import { MajstorService } from '../../service/majstor.service';
import { Component, OnInit, AfterContentInit, Output, Input } from '@angular/core';
import { AuthService } from '../../service/auth.service';

@Component({
  selector: 'app-majstor-header',
  templateUrl: './majstor-header.component.html',
  styleUrls: ['./majstor-header.component.css']
})
export class MajstorHeaderComponent implements OnInit, AfterContentInit {

  badgeCount;
  ulogovaniKorisnik;
  _type: string;
  @Input() notification: string[];
  newNostifation;
  constructor(private majstorService: MajstorService, private auth: AuthService, private notificationService: NotificationService) {
    this._type = this.auth.typeUserValue;
    this.badgeCount = this.notificationService.count;
  }

  ngAfterContentInit(): void {
  }

  ngOnInit() {
    this.ulogovaniKorisnik = localStorage.getItem('userName');
    this._type = localStorage.getItem('typeUser');
    this.majstorService.getNewNostifiation().subscribe( (res: any) => {
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
