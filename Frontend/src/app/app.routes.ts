import { Routes } from '@angular/router';
import { MainLayout } from './layout/main-layout/main-layout';
// Importaremos el guard más adelante: import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  {
    path: 'auth',
    loadChildren: () => import('./features/auth/auth.routes').then(m => m.AUTH_ROUTES)
  },
  {
    path: '',
    component: MainLayout,
    // canActivate: [authGuard], <-- Lo activaremos en el Paso 2
    children: [
      {
        path: 'welcome',
        loadComponent: () => import('./features/dashboard/welcome/welcome').then(m => m.Welcome)
      },
      {
        path: 'admin',
        loadChildren: () => import('./features/admin/admin.routes').then(m => m.ADMIN_ROUTES)
      },
      {
        path: 'gestor',
        loadChildren: () => import('./features/gestor/gestor.routes').then(m => m.GESTOR_ROUTES)
      },
      {
        path: 'cajero',
        loadChildren: () => import('./features/cajero/cajero.routes').then(m => m.CAJERO_ROUTES)
      },
      { path: '', redirectTo: 'welcome', pathMatch: 'full' }
    ]
  },
  { path: '**', redirectTo: 'auth/login' }
];
