import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { Auth } from '../services/auth';

export const roleGuard: CanActivateFn = (route) => {
  const authService = inject(Auth);
  const router = inject(Router);

  const expectedRole = route.data['expectedRole'] as string | string[];
  const userRole = authService.userRole();

  if (userRole) {
    // Si viene como arreglo de roles
    if (Array.isArray(expectedRole) && expectedRole.map(r => r.toLowerCase()).includes(userRole.toLowerCase())) {
      return true;
    }
    // Si viene como string unico
    if (typeof expectedRole === 'string' && userRole.toLowerCase() === expectedRole.toLowerCase()) {
      return true;
    }
  }

  // Redirige a welcome si no coincide el rol
  router.navigate(['/welcome']);
  return false;
};
