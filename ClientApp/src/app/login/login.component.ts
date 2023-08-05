import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LocalStorageService } from '../services/local-storage.service';
import jwt_decode from 'jwt-decode';
import { Token } from '../models/token';
import { environment } from 'src/environments/environment';
import { NotificationService } from '../services/notification.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  username!: string;
  password!: string;
  apiUrl: string = environment.apiUrl;

  constructor(
    private http: HttpClient,
    private router: Router,
    private localStorageService: LocalStorageService,
    private notificationService: NotificationService) { }

  login() {
    const body = { username: this.username, password: this.password };
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*'
      })
    };

    this.http.post<Token>(`${this.apiUrl}/Auth/login`, body, httpOptions).subscribe({
      next: (response) => {
        const tokenInfo = this.getDecodedAccessToken(response.tokenString); // decode token
        const user = tokenInfo.unique_name;

        this.localStorageService.setItem('user', user);
        this.notificationService.success("Logged in");
        this.router.navigate(['/todo']); // redirect to TodoComponent
      },
      error: (e) => {
        this.notificationService.error(`Error occurred, check console`);
        console.error(e);
      }
    });
  }

  getDecodedAccessToken(token: string): any {
    try {
      return jwt_decode(token);
    } catch (error) {
      return null;
    }
  }
}
