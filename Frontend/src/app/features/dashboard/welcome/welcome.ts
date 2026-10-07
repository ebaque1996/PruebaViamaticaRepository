import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Auth } from '../../../core/services/auth'; // Ajusta la ruta a tu auth.ts

@Component({
  selector: 'app-welcome',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './welcome.html',
  styleUrls: ['./welcome.scss']
})
export class Welcome {
  private authService = inject(Auth);

  // Exponemos la signal del usuario para usarla en el HTML
  user = this.authService.currentUser;

  // Calculamos un saludo según la hora local del dispositivo
  get greeting(): string {
    const hour = new Date().getHours();
    if (hour < 12) return 'Buenos días';
    if (hour < 19) return 'Buenas tardes';
    return 'Buenas noches';
  }
}
