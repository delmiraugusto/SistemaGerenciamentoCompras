import { BehaviorSubject, catchError, map } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Constants } from './Constants';
import { User, UserLogin, UserLoginResponse, UserInsert, UserUpdate } from '../models/User';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private BASE_URL: string = Constants.USER;
  private currentUserSource = new BehaviorSubject<any | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  getCurrentUser = () => sessionStorage.getItem('name');
  getCurrentAccessLevel = () => sessionStorage.getItem('roles');
  getCurrentToken = () => sessionStorage.getItem('token');

  Login = (user: UserLogin) =>
    this.http.post<UserLoginResponse>(`${this.BASE_URL}User/Validate`, user).pipe(
      map((response: UserLoginResponse) => {
        sessionStorage.setItem('token', response.token);
        this.currentUserSource.next(response);
        return response;
      }),
      catchError(error => { throw error; })
    );

  Logout = () => {
    sessionStorage.clear();
    this.currentUserSource.next(null);
  };
}