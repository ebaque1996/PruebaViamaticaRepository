import { Injectable, signal, computed } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { LoginRequest, LoginResponse, User, MenuItem, RecoverPasswordRequest } from '../models/auth.models';

@Injectable({
  providedIn: 'root'
})
export class Auth {
  //private readonly API_URL = 'http://localhost:5298/api'; // Ajusta la URL/Puerto de tu backend ASP.NET Core
  private readonly API_URL = 'http://localhost:5000/api'; // Ajusta la URL/Puerto de tu backend ASP.NET Core

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
        // 1. Formatear el menú recibido del backend (mapear íconos y labels)
        const formattedMenu = this.formatMenu(response.menu);

        // 2. Guardar sesión en LocalStorage
        localStorage.setItem('jwt_token', response.token);
        localStorage.setItem('auth_user', JSON.stringify(response.user));
        localStorage.setItem('auth_menu', JSON.stringify(formattedMenu));

        // 3. Actualizar Signals
        this.currentUser.set(response.user);
        this.currentMenu.set(formattedMenu); // Guardamos el menú ya formateado
      })
    );
  }

  recoverPassword(data: RecoverPasswordRequest): Observable<{ message: string; temporaryPassword?: string }> {
    return this.http.post<{ message: string; temporaryPassword?: string }>(
      `${this.API_URL}/Auth/recover-password`,
      data
    );
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

  // --- MÉTODOS PRIVADOS PARA EL FORMATEO DEL MENÚ ---

  private formatMenu(menu: MenuItem[]): MenuItem[] {
    if (!menu) return [];

    return menu.map(item => ({
      ...item,
      // Adaptamos "title" (que viene del backend) a "label" (que usa tu HTML)
      label: item.title /*|| item.label*/,
      icon: this.mapIcon(item.icon ?? ""),
      children: item.children?.map(child => ({
        ...child,
        label: child.title /*|| child.label*/,
        icon: this.mapIcon(child.icon ?? "")
      }))
    }));
  }

  private mapIcon(iconName: string): string {
    const iconMap: Record<string, string> = {
      'home': 'bi-house-door',
      'dashboard': 'bi-speedometer2',
      'people': 'bi-people-fill',
      'assignment': 'bi-card-checklist',
      'person': 'bi-person-fill',
      'point_of_sale': 'bi-cash-coin'
    };
    // Si no encuentra el ícono en el diccionario, pone un círculo por defecto
    return iconMap[iconName?.toLowerCase()] || 'bi-circle';
  }
}
