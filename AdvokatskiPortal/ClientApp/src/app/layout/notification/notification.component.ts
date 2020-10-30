import { NotificationService } from '../../service/Notification.service';
import { Component, OnInit, Input } from '@angular/core';
import * as moment from 'moment';
import { MatDialog } from '@angular/material';
import { PreviewCaseCraftmanComponent } from '../../craftman/dialog/preview-case-craftman/preview-case-craftman.component';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent implements OnInit {


  constructor( public notificationService: NotificationService, public dialog: MatDialog) { }
  ngOnInit() {
    this.setupCustomMomentLabels();
    if (localStorage.getItem('typeUser').endsWith('User')) {
    }
  }
  private setupCustomMomentLabels() {
    moment.locale('en', {
      relativeTime: {
        future: 'in %s',
        past: 'pre %s',
        s:  'few second',
        ss: '%d few seconds',
        m:  'minute',
        mm: '%d minute',
        h:  'hour',
        hh: '%d hours',
        d:  'day',
        dd: '%d days',
        M:  'month',
        MM: '%d months',
        y:  'year',
        yy: '%d years'
      }
    });
  }
  private remapImagesForDisplay(data) {
    console.log(data)
      const baseSlike = data.pictures.map(s => {
        s.pictureBytes = 'data:image/jpeg;base64,' + s.pictureBytes;
        return s;
    });
  }
  openDialogPreviewCase(element): void {
    console.log(element);
    this.remapImagesForDisplay(element);
    const dialogRef = this.dialog.open(PreviewCaseCraftmanComponent, {
      maxWidth: '40%',
      maxHeight: '70%',
      data: { name: element.name, description: element.description, pictures: element.pictures }
    });
  }
  writeNotification(not): string {
    console.log(not)
    if (not === null || not === undefined ) {
      return 'Do not have notificaiton';
    }

    const name = not.notificationText;
    const time = not.timeStamp;
    const deltaTime = moment(time).local().fromNow();
    return `${name} ${deltaTime}`;
  }
}
