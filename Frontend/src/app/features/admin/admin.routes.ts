import { Routes } from '@angular/router';

export const ADMIN_ROUTES: Routes = [
  {
    path: 'dashboard',
    loadComponent: () => import('./dashboard/admin-dashboard').then(m => m.AdminDashboard)
  },
  {
    path: 'users',
    loadComponent: () => import('./users/users-list').then(m => m.UsersList)
  },
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
];
