import { Component } from '@angular/core';
import { SignalRService } from './services/signalr.service';
import { NotificationService } from './services/notification.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'ClientApp';
  constructor(private signalRService: SignalRService,
    private notificationService: NotificationService) {}
  ngOnInit(): void {
      this.signalRService.startConnection();
      this.signalRService.listenToNotifications((message) => {
        this.notificationService.message(message);
      });
  }
}
