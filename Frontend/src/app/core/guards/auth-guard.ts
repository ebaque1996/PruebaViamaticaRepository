import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { Auth } from '../services/auth';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(Auth);
  const router = inject(Router);

  if (authService.isAuthenticated()) {
    return true;
  }

  // Si no está autenticado, redirige al Login
  router.navigate(['/auth/login'], { queryParams: { returnUrl: state.url } });
  return false;
};

// Guard opcional para verificar Roles específicos en rutas
export const roleGuard = (allowedRoles: string[]): CanActivateFn => {
  return () => {
    const authService = inject(Auth);
    const router = inject(Router);

    const userRole = authService.userRole();

    if (userRole && allowedRoles.includes(userRole)) {
      return true;
    }

    // Redirige a welcome si no tiene permiso para ese módulo específico
    router.navigate(['/welcome']);
    return false;
  };
};
