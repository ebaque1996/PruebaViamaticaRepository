import { Injectable, signal, computed } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { LoginRequest, LoginResponse, User, MenuItem, RecoverPasswordRequest } from '../models/auth.models';

@Injectable({
  providedIn: 'root'
})
export class Auth {
  private readonly API_URL = 'http://localhost:5298/api'; // Ajusta la URL/Puerto de tu backend ASP.NET Core

  // Signals para el estado global de autenticación
  currentUser = signal<User | null>(this.getUserFromStorage());
  currentMenu = signal<MenuItem[]>(this.getMenuFromStorage());

  // Computados
  isAuthenticated = computed(() => !!this.currentUser() && !!this.getToken());
  userRole = computed(() => this.currentUser()?.roleName || null);

  constructor(
    private http: HttpClient,
    private router: Router
  ) {}

  login(credentials: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.API_URL}/Auth/login`, credentials).pipe(
      tap((response) => {
        // Guardar sesión en LocalStorage
        localStorage.setItem('jwt_token', response.token);
        localStorage.setItem('auth_user', JSON.stringify(response.user));
        localStorage.setItem('auth_menu', JSON.stringify(response.menu));

        // Actualizar Signals
        this.currentUser.set(response.user);
        this.currentMenu.set(response.menu);
      })
    );
  }

  recoverPassword(data: RecoverPasswordRequest): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(`${this.API_URL}/auth/recover-password`, data);
  }

  logout(): void {
    localStorage.removeItem('jwt_token');
    localStorage.removeItem('auth_user');
    localStorage.removeItem('auth_menu');

    this.currentUser.set(null);
    this.currentMenu.set([]);

    this.router.navigate(['/auth/login']);
  }

  getToken(): string | null {
    return localStorage.getItem('jwt_token');
  }

  private getUserFromStorage(): User | null {
    const userJson = localStorage.getItem('auth_user');
    return userJson ? JSON.parse(userJson) : null;
  }

  private getMenuFromStorage(): MenuItem[] {
    const menuJson = localStorage.getItem('auth_menu');
    return menuJson ? JSON.parse(menuJson) : [];
  }
}
