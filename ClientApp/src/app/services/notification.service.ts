import { Injectable } from '@angular/core';
import * as alertify from 'alertifyjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor() { /* TODO document why this constructor is empty */  }

  success(message: string){
    alertify.success(message);
  }

  warning(message: string){
    alertify.warning(message);
  }

  error(message: string){
    alertify.error(message);
  }

  message(message: string){
    alertify.message(message);
  }
}
