import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: any;

  public startConnection(){
    return new Promise((resolve, reject) => {
      this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7007/notification')
      .build();

      this.hubConnection
      .start()
      .then(() => {
        console.log("Connected to SignalR Hub >>>");
        return resolve(true);
      }).catch((error: any) => {
        console.error(error);
        reject(error);
      })
    });
  }

  public listenToNotifications(callback: (message: string) => void): void {
    this.hubConnection.on('SendNotificationAsync', (message: string) => {
      callback(message);
    });
  }

  public sendNotification(message: string): void {
    this.hubConnection.invoke('SendNotificationToUserAsync', message);
  }
}
