import { KorisnikService } from './../../service/korisnik.service';
import { NotificationService } from './../../service/notification.service';
import { Component, OnInit, Input } from '@angular/core';
import { AdvokatService } from '../../service/advokat.service';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent implements OnInit {


  constructor(private korisnikService: KorisnikService, public notificationService: NotificationService,
              private advokatService: AdvokatService) { }
  ngOnInit() {
    if (localStorage.getItem('typeUser').endsWith('User')) {
      this.korisnikService.getNewNostifiation().subscribe((res: any[]) => {
        this.notificationService.setNotifications(res);
        // this.notificationService.count;
      });
    } else {
      this.advokatService.getNewNostifiation().subscribe((res: any[]) => {
        console.log(res);
        this.notificationService.setNotifications(res);
        // this.notificationService.count;
      });
    }
  }
}
