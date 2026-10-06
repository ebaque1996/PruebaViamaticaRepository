import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { finalize } from 'rxjs';
// Asumimos que crearás un LoadingService para mostrar el spinner
// import { LoadingService } from '../services/loading.service';

export const apiInterceptor: HttpInterceptorFn = (req, next) => {
  // const loadingService = inject(LoadingService);
  // loadingService.show();

  // 1. Obtener el token del LocalStorage (o tu servicio de autenticación)
  const token = localStorage.getItem('jwt_token');

  // 2. Clonar la petición y agregar cabeceras
  let clonedRequest = req;
  if (token) {
    clonedRequest = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
        'Content-Type': 'application/json'
      }
    });
  }

  // 3. Pasar la petición al siguiente manejador y ocultar el loading al finalizar
  return next(clonedRequest).pipe(
    finalize(() => {
      // loadingService.hide();
    })
  );
};
