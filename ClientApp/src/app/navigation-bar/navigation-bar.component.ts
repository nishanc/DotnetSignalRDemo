import { Component, DoCheck, OnInit } from '@angular/core';
import * as jwtDecode from 'jwt-decode';
import { LocalStorageService } from '../services/local-storage.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navigation-bar',
  templateUrl: './navigation-bar.component.html',
  styleUrls: ['./navigation-bar.component.scss']
})
export class NavigationBarComponent implements OnInit, DoCheck {
  username!: string | null;

  constructor(private localStorageService: LocalStorageService, private router: Router) { }

  ngOnInit() {
    this.updateUsername();
  }

  ngDoCheck() {
    this.updateUsername();
  }

  private updateUsername() {
    this.username = this.localStorageService.getItem("user");
  }

  logout() {
    this.localStorageService.clear();
    this.router.navigate(['/']);
  }
}
