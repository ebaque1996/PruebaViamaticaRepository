import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Auth } from '../../core/services/auth';
import { Menu } from '../../core/services/menu';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './navbar.html',
  styleUrls: ['./navbar.scss']
})
export class NavbarComponent {
  private authService = inject(Auth);
  private menuService = inject(Menu);

  // Exponemos la Signal directamente
  currentUser = this.authService.currentUser;

  logout(): void {
    this.menuService.clearMenu();
    this.authService.logout();
  }
}
