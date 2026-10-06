import { Component, signal, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { Auth } from '../../../core/services/auth';

@Component({
  selector: 'app-recover-password',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './recover-password.html',
  styleUrl: './recover-password.scss'
})
export class RecoverPassword {
  private fb = inject(FormBuilder);
  private auth = inject(Auth);

  loading = signal<boolean>(false);
  errorMessage = signal<string | null>(null);
  successMessage = signal<string | null>(null);
  tempPassword = signal<string | null>(null);

  recoverForm: FormGroup = this.fb.group({
    emailOrIdentification: ['', [Validators.required, Validators.minLength(5)]],
    newPassword: [''] // Opcional: si se deja vacío, el backend genera una clave temporal
  });

  get f() {
    return this.recoverForm.controls;
  }

  onSubmit(): void {
    this.errorMessage.set(null);
    this.successMessage.set(null);
    this.tempPassword.set(null);

    if (this.recoverForm.invalid) {
      this.recoverForm.markAllAsTouched();
      return;
    }

    this.loading.set(true);

    this.auth.recoverPassword(this.recoverForm.value).subscribe({
      next: (res) => {
        this.loading.set(false);
        this.successMessage.set(res.message);
        if (res.temporaryPassword) {
          this.tempPassword.set(res.temporaryPassword);
        }
        this.recoverForm.reset();
      },
      error: (err) => {
        this.loading.set(false);
        const msg = err.error?.message || 'Error al procesar la solicitud. Intente nuevamente.';
        this.errorMessage.set(msg);
      }
    });
  }
}
