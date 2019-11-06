import { PrikazSlucajComponent } from './../../advokat/dialog/prikaz-slucaj/prikaz-slucaj.component';
import { KorisnikService } from './../../service/korisnik.service';
import { NotificationService } from './../../service/notification.service';
import { Component, OnInit, Input } from '@angular/core';
import { AdvokatService } from '../../service/advokat.service';
import * as moment from 'moment';
import { MatDialog } from '@angular/material';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent implements OnInit {


  constructor(private korisnikService: KorisnikService, public notificationService: NotificationService,
              private advokatService: AdvokatService, public dialog: MatDialog) { }
  ngOnInit() {
    this.setupCustomMomentLabels();
    if (localStorage.getItem('typeUser').endsWith('User')) {
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
  openDialogPrikazSlucaja(element): void {
    const dialogRef = this.dialog.open(PrikazSlucajComponent, {
      maxWidth: '40%',
      maxHeight: '70%',
      data: { naziv: element.slucaj.naziv, opis: element.slucaj.opis, slike: element.slucaj.slike }
    });
  }
  writeNotification(not): string {
    if (not === null || not === undefined) {
      return 'Nema obavestenja';
    }
    console.log(not)
    const name = not.notificationText;
    const time = not.timeStamp;
    const deltaTime = moment(time).local().fromNow();
    return `${name} ${deltaTime}`;
  }
}
