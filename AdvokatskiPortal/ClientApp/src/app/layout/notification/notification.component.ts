import { KorisnikService } from './../../service/korisnik.service';
import { NotificationService } from './../../service/notification.service';
import { Component, OnInit, Input } from '@angular/core';
import { AdvokatService } from '../../service/advokat.service';
import * as moment from 'moment';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent implements OnInit {


  constructor(private korisnikService: KorisnikService, public notificationService: NotificationService,
              private advokatService: AdvokatService) { }
  ngOnInit() {
    this.setupCustomMomentLabels();
    if (localStorage.getItem('typeUser').endsWith('User')) {
    //   this.korisnikService.getNewNostifiation().subscribe((res: any[]) => {
    //     console.log(res);
    //     this.notificationService.setNotifications(res);
    //     // this.notificationService.count;
    //   });
    // } else {
    //   this.advokatService.getNewNostifiation().subscribe((res: any[]) => {
    //     console.log(res);
    //     this.notificationService.setNotifications(res);
    //     // this.notificationService.count;
    //   });
    }
  }
  private setupCustomMomentLabels() {
    moment.locale('en', {
      relativeTime: {
        future: 'u %s',
        past: 'pre %s',
        s:  'sekundi',
        ss: '%d par sekundi',
        m:  'minut',
        mm: '%d minuta',
        h:  'sat',
        hh: '%d sati',
        d:  'dan',
        dd: '%d dana',
        M:  'mesec',
        MM: '%d meseci',
        y:  'godina',
        yy: '%d godina'
      }
    });
  }
  writeNotification(not): string {
    const name = not.notificationText;
    const time = not.timeStamp;
    const deltaTime = moment(time).local().fromNow();
    return `${name} ${deltaTime}`;
  }
}
