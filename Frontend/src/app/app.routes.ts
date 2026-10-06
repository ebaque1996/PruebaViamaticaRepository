import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainLayout } from './layout/main-layout/main-layout';
import { authGuard } from './core/guards/auth-guard';
import { roleGuard } from './core/guards/role-guard';

export const routes: Routes = [
  {
    path: 'auth',
    loadChildren: () => import('./features/auth/auth.routes').then(m => m.AUTH_ROUTES)
  },
  {
    path: '',
    component: MainLayout,
    canActivate: [authGuard],
    children: [
      {
        path: 'welcome',
        loadComponent: () => import('./features/dashboard/welcome/welcome').then(m => m.Welcome)
      },
      {
        path: 'admin',
        canActivate: [roleGuard],
        data: { expectedRole: 'Administrador' },
        loadChildren: () => import('./features/admin/admin.routes').then(m => m.ADMIN_ROUTES)
      },
      {
        path: 'gestor',
        canActivate: [roleGuard],
        data: { expectedRole: 'Gestor' },
        loadChildren: () => import('./features/gestor/gestor.routes').then(m => m.GESTOR_ROUTES)
      },
      {
        path: 'cajero',
        canActivate: [roleGuard],
        data: { expectedRole: 'Cajero' },
        loadChildren: () => import('./features/cajero/cajero.routes').then(m => m.CAJERO_ROUTES)
      },
      { path: '', redirectTo: 'welcome', pathMatch: 'full' }
    ]
  },
  { path: '**', redirectTo: 'auth/login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
