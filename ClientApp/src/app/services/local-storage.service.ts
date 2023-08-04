import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {
  constructor() { /* TODO document why this constructor is empty */ }

  setItem(key: string, value: string) {
    localStorage.setItem(key, value);
  }

  getItem(key: string): string| null {
    return localStorage.getItem(key);
  }

  removeItem(key: string) {
    localStorage.removeItem(key);
  }

  clear() {
    localStorage.clear();
  }
}
