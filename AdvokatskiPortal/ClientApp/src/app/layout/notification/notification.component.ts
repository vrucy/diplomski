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
  private remapImagesForDisplay(data) {
    console.log(data);
      const baseSlike = data.slike.map(s => {
        console.log(s)
        s.slikaProp = 'data:image/jpeg;base64,' + s.slikaProp;
        return s;
    });
  }
  openDialogPrikazSlucaja(element): void {
    console.log(element);
    this.remapImagesForDisplay(element);
    const dialogRef = this.dialog.open(PrikazSlucajComponent, {
      maxWidth: '40%',
      maxHeight: '70%',
      data: { naziv: element.naziv, opis: element.opis, slike: element.slike }
    });
  }
  writeNotification(not): string {
    if (not === null || not === undefined) {
      return 'Nema obavestenja';
    }

    const name = not.notificationText;
    const time = not.timeStamp;
    const deltaTime = moment(time).local().fromNow();
    return `${name} ${deltaTime}`;
  }
}
