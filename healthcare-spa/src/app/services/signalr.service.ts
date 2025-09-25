import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { NotificationMessage } from '../models/notification.model';
import { AuthService } from './auth.service';

@Injectable({ providedIn: 'root' })
export class SignalRService {
  private hubConnection!: signalR.HubConnection;
  private notifications$ = new BehaviorSubject<NotificationMessage[]>([]);
  public notificationsObservable = this.notifications$.asObservable();

  constructor(private auth: AuthService) {}

  startConnection() {
    const token = this.auth.getToken();
    if (!token) { console.error('No JWT'); return; }

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5000/hubs/notifications', {
        accessTokenFactory: () => token
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start()
      .then(() => console.log('SignalR connected'))
      .catch(err => console.error(err));

    this.hubConnection.on('ReceiveNotification', (msg: any) => {
      const item: NotificationMessage = {
        id: msg.id,
        title: msg.title,
        body: msg.body,
      };
      this.notifications$.next([item, ...this.notifications$.value]);
    });



    this.hubConnection.on('NotificationMarkedAsRead', (payload: any) => {
      const updated = this.notifications$.value.map(n => n.id === payload.id ? { ...n, isRead: true } : n);
      this.notifications$.next(updated);
    });
  }
}
