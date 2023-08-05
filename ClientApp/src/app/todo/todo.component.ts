import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Todo } from '../models/todo';
import { environment } from 'src/environments/environment';
import { NotificationService } from '../services/notification.service';
import { SignalRService } from '../services/signalr.service';
import { EventHandlerService } from '../services/event-handler.service';


@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.scss']
})
export class TodoComponent {
  apiUrl: string = environment.apiUrl;
  newTodo!: string;
  todos: Todo[] = [];

  constructor(private http: HttpClient,
    private notificationService: NotificationService,
    private signalRService: SignalRService,
    private eventHandlerService: EventHandlerService) {
      this.eventHandlerService.notificationEvent.subscribe((data: string) => {
        this.onNotificationEvent(data);
      });
    }

  onNotificationEvent(data: string) {
    if(data === "refresh"){
      this.getTodos();
    }
  }

  ngOnInit() {
    this.getTodos();
  }

  getTodos() {
    this.http.get<Todo[]>(`${this.apiUrl}/todo`).subscribe({
      next: (todos) => {
        this.todos = todos;
      },
      error: (e) => {
        this.notificationService.error(`Error ocurred, check console`);
        console.error(e);
      }
    });
  }

  addTodo() {
    if (this.newTodo === null || this.newTodo === "") {
      return;
    }

    this.http.post(`${this.apiUrl}/todo`, {
      name: this.newTodo
    }).subscribe({
      next: () => {
        this.getTodos();
        this.newTodo = '';
      },
      error: (e) => {
        this.notificationService.error(`Error ocurred, check console`);
        console.error(e);
      }
    });
  }

  deleteTodo(id: number) {
    this.http.delete(`${this.apiUrl}/todo/${id}`).subscribe({
      next: () => {
        this.todos = this.todos.filter(t => t.id !== id);
        this.signalRService.sendNotification("Item Deleted Notification from Client")
      },
      error: (e) => {
        this.notificationService.error(`Error ocurred, check console`);
        console.error(e);
      }
    });
  }
}
