import { EventEmitter, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class EventHandlerService {
  notificationEvent: EventEmitter<any> = new EventEmitter<any>();
}
