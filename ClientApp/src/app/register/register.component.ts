import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserGroup } from '../models/group';
import { NotificationService } from '../services/notification.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit{
  username!: string;
  password!: string;
  apiUrl: string = environment.apiUrl;
  userGroups: UserGroup[] = [];
  selectedGroup!: number;

  constructor(
    private http: HttpClient,
    private notificationService: NotificationService,
    private router: Router) { }

  ngOnInit() {
    this.getUserGroups();
  }

  register() {
   console.log("this.selectedGroup", this.selectedGroup);
    this.http.post(`${this.apiUrl}/Auth/register`, {
      username: this.username,
      password: this.password,
      group: this.selectedGroup
    }).subscribe({
      next: () => {
        this.notificationService.success("User registered");
        this.router.navigate(['/login']); // redirect to Login
      },
      error: (e) => {
        this.notificationService.error(`Error ocurred, check console`);
        console.error(e);
      }
    });
  }

  getUserGroups() {
    this.http.get<UserGroup[]>(`${this.apiUrl}/Auth/get-user-groups`).subscribe({
      next: (groups) => {
        this.userGroups = groups;
        this.selectedGroup = this.userGroups[0].id;
      },
      error: (e) => {
        this.notificationService.error(`Error ocurred, check console`);
        console.error(e);
      }
    });
  }
}
