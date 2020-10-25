import { CraftmanService } from '../../service/Craftman.service'
import { NotificationService } from '../../service/Notification.service';
import { UserService } from '../../service/User.service';
import { Component, OnInit,  AfterViewInit, AfterContentInit } from '@angular/core';
import { AuthService } from '../../service/auth.service';

@Component({
  selector: 'app-user-header',
  templateUrl: './user-header.component.html',
  styleUrls: ['./user-header.component.css']
})
export class UserHeaderComponent implements OnInit,AfterContentInit  {
  dataType: string;
  loginUser ;
  _type: string;
  badgeCount;

  constructor(private notificationService: NotificationService,
              private auth: AuthService, private userService: UserService) {
    this._type = this.auth.typeUserValue;
    this.badgeCount = this.notificationService.count;
    console.log(this.notificationService.count);
  }
  ngAfterContentInit() {
  }

  ngOnInit() {
    this.loginUser = localStorage.getItem('UserName');
    this.userService.GetNewNostifiation().subscribe( (res: any) => {
      this.notificationService.setNotifications(res);
      this.badgeCount = this.notificationService.count;
      console.log(this.badgeCount);
    });
  }
  clearCount() {
    console.log(this.notificationService.count);
    if (this.notificationService.notifications.length !== 0) {
      this.badgeCount = 0;
    }
  }

}
