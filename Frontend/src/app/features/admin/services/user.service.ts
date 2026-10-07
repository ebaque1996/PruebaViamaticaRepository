import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserAdmin, Role, CreateUserDto } from '../models/user.models';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private http = inject(HttpClient);

  //private readonly API_URL = 'http://localhost:5298/api';
  private readonly API_URL = 'http://localhost:5000/api';

  // Endpoint 1: Obtener Roles
  getRoles(): Observable<Role[]> {
    return this.http.get<Role[]>(`${this.API_URL}/Rol`); // Ajusta si tu ruta es diferente
  }

  // Endpoint 2: Obtener Usuarios
  getUsers(): Observable<UserAdmin[]> {
    return this.http.get<UserAdmin[]>(`${this.API_URL}/Users`); // Ajusta si tu ruta es diferente
  }

  createUser(user: CreateUserDto): Observable<any> {
    // Ajusta la ruta si tu controlador tiene otra ruta base.
    // Por lo general es `${this.API_URL}/User` o `${this.API_URL}/User/CreateUser`
    return this.http.post(`${this.API_URL}/Users`, user);
  }
}
