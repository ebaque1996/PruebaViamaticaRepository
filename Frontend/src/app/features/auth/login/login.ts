import { Component, signal, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { Auth } from '../../../core/services/auth';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class Login {
  private fb = inject(FormBuilder);
  private auth = inject(Auth);
  private router = inject(Router);

  loading = signal<boolean>(false);
  errorMessage = signal<string | null>(null);

  loginForm: FormGroup = this.fb.group({
    emailOrUsername: ['', [Validators.required, Validators.minLength(3)]],
    passwordHash: ['', [Validators.required, Validators.minLength(4)]]
  });

  // Acceso rápido a las propiedades del formulario para las validaciones HTML
  get f() {
    return this.loginForm.controls;
  }

  onSubmit(): void {
    this.errorMessage.set(null);

    // 1. Validar que los datos hayan sido ingresados correctamente antes de consumir la API
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.loading.set(true);

    // 2. Consumir el servicio de autenticación
    this.auth.login(this.loginForm.value).subscribe({
      next: () => {
        this.loading.set(false);
        this.router.navigate(['/welcome']);
      },
      error: (err) => {
        this.loading.set(false);
        console.log('Error en login:', err);
        const msg = err.error?.message || 'Credenciales incorrectas o error de conexión con el servidor.';
        this.errorMessage.set(msg);
      }
    });
  }
}
