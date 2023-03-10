import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../user/user.model';

@Injectable({
  providedIn: 'root',
})
export class DataStoreService {
  constructor(private http: HttpClient) {}

  getUsers() {
    return this.http.get<User[]>('/api/users');
  }
}
