import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  public notifications: string[];
  public count: number;
  constructor() { }

  public addNotification(notification: string) {
    if (!notification.includes(notification)) {
      this.notifications.push(notification);
    }
  }

  public setNotifications(notifications: string[]) {
    this.notifications = notifications;
    this.count = notifications.length;
  }
}
