import { BehaviorSubject, catchError, map } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Constants } from './Constants';
import { User, UserLogin, UserLoginResponse, UserInsert, UserUpdate } from '../models/User';
import { jwtDecode } from 'jwt-decode';

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

  GetList = () =>
    this.http.get<User[]>(`${Constants.USER}/Read`).pipe(
      map((products: User[]) => products),
      catchError(error => { throw error; })
    );

  GetById = (id: number) =>
    this.http.get<User>(`${Constants.USER}/Read/${id}`).pipe(
      map((product: User) => product),
      catchError(error => { throw error; })
    );

  Login = (user: UserLogin) =>
    this.http.post<UserLoginResponse>(`${this.BASE_URL}/Validate`, user).pipe(
      map((response: UserLoginResponse) => {
        sessionStorage.setItem('token', response.token);

        const decodedToken: any = jwtDecode(response.token);
        const roleID = decodedToken?.roleID;

        sessionStorage.setItem('roles', roleID);

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