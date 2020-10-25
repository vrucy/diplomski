import { NotificationService } from '../../service/Notification.service';
import { CraftmanService } from '../../service/Craftman.service'
import { Component, OnInit, AfterContentInit, Output, Input } from '@angular/core';
import { AuthService } from '../../service/auth.service';

@Component({
  selector: 'app-craftman-header',
  templateUrl: './craftman-header.component.html',
  styleUrls: ['./craftman-header.component.css']
})
export class CraftmanHeaderComponent implements OnInit, AfterContentInit {

  badgeCount;
  loginUser;
  _type: string;
  @Input() notification: string[];
  newNostifation;
  constructor(private craftmanService: CraftmanService, private auth: AuthService, private notificationService: NotificationService) {
    this._type = this.auth.typeUserValue;
    this.badgeCount = this.notificationService.count;
  }

  ngAfterContentInit(): void {
  }

  ngOnInit() {
    this.loginUser = localStorage.getItem('UserName');
    this._type = localStorage.getItem('typeUser');
    this.craftmanService.GetNewNostification().subscribe( (res: any) => {
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
