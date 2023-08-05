import { Component } from '@angular/core';
import { SignalRService } from './services/signalr.service';
import { NotificationService } from './services/notification.service';
import { EventHandlerService } from './services/event-handler.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'ClientApp';
  constructor(private signalRService: SignalRService,
    private notificationService: NotificationService,
    private eventHandlerService: EventHandlerService) {}
  ngOnInit(): void {
      this.signalRService.startConnection();
      this.signalRService.listenToNotifications((message) => {
        this.notificationService.message(message);
        this.eventHandlerService.notificationEvent.emit("refresh");
      });
  }
}
