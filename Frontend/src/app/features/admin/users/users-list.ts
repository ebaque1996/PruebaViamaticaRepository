import { Component, signal, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CreateUserDto, Role, UserAdmin } from '../models/user.models';
import { UserService } from '../services/user.service'; // Asegúrate de que la ruta sea correcta

// Declaración para usar las funciones nativas de Bootstrap JS para el Modal
declare var bootstrap: any;

@Component({
  selector: 'app-users-list',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './users-list.html',
  styleUrls: ['./users-list.scss']
})
export class UsersList implements OnInit {
  private fb = inject(FormBuilder);
  private userService = inject(UserService);

  searchTerm = signal('');
  isLoading = signal(false);
  isEditMode = signal(false);

  userForm!: FormGroup;
  selectedUserId: number | null = null;

  // Señales que ahora recibirán datos reales
  roles = signal<Role[]>([]);
  users = signal<UserAdmin[]>([]);

  ngOnInit(): void {
    this.initForm();
    this.loadData(); // Llamamos a tu backend al iniciar
  }

  // 1. Inicializar el formulario reactivo con el nuevo campo
  private initForm() {
    this.userForm = this.fb.group({
      username: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      password: [''],
      identification: ['', [Validators.required, Validators.minLength(10)]], // NUEVO CAMPO
      rolId: ['', [Validators.required]], // Coincide con tu backend
      statusId: ['ACT'] // Coincide con tu backend
    });
  }

  // 2. Cargar datos desde los endpoints reales
  private loadData() {
    this.isLoading.set(true);

    // Obtenemos los roles primero
    this.userService.getRoles().subscribe({
      next: (rolesData) => {
        this.roles.set(rolesData);

        // Luego obtenemos los usuarios
        this.userService.getUsers().subscribe({
          next: (usersData) => {
            // Cruzamos el rolId con la lista de roles para obtener el nombre
            const usersMapped = usersData.map(user => {
              const matchedRole = this.roles().find(r => r.rolId === user.rolId);
              return {
                ...user,
                rolNameTemp: matchedRole ? matchedRole.rolName : 'Desconocido'
              };
            });
            this.users.set(usersMapped);
            this.isLoading.set(false);
          },
          error: (err) => {
            console.error('Error al cargar usuarios:', err);
            this.isLoading.set(false);
          }
        });
      },
      error: (err) => {
        console.error('Error al cargar roles:', err);
        this.isLoading.set(false);
      }
    });
  }

  // 3. Método para filtrar usuarios en la tabla
  get filteredUsers() {
    const term = this.searchTerm().toLowerCase();
    return this.users().filter(u =>
      u.username.toLowerCase().includes(term) ||
      u.email.toLowerCase().includes(term) ||
      u.identification.toLowerCase().includes(term) ||
      (u.rolNameTemp && u.rolNameTemp.toLowerCase().includes(term))
    );
  }

  // 4. Abrir Modal para CREAR usuario
  openCreateModal() {
    this.isEditMode.set(false);
    this.selectedUserId = null;

    this.userForm.reset({ statusId: 'ACT', rolId: '' });

    this.userForm.get('password')?.setValidators([Validators.required, Validators.minLength(6)]);
    this.userForm.get('password')?.updateValueAndValidity();

    this.showModal();
  }

  // 5. Abrir Modal para EDITAR usuario
  openEditModal(user: UserAdmin) {
    this.isEditMode.set(true);
    this.selectedUserId = user.userId;

    this.userForm.get('password')?.clearValidators();
    this.userForm.get('password')?.updateValueAndValidity();

    // Llenamos el formulario alineado con las propiedades de tu backend
    this.userForm.patchValue({
      username: user.username,
      email: user.email,
      identification: user.identification, // Nuevo campo
      rolId: user.rolId, // Ajustado
      statusId: user.statusId // Ajustado
    });

    this.showModal();
  }

  // 6. Guardar (Preparado para cuando tengas los métodos POST/PUT)
  saveUser() {
    //if (this.userForm.invalid) return;

    const formValues = this.userForm.value;

    if (this.isEditMode() && this.selectedUserId !== null) {
      // AQUÍ IRÁ LA LLAMADA AL ENDPOINT PUT/PATCH (Paso futuro)
      console.log('Falta implementar Editar', formValues);
    } else {

      // 1. Armamos el objeto tal como lo espera tu backend
      const newUser: CreateUserDto = {
        username: formValues.username,
        email: formValues.email,
        password: formValues.password,
        identification: formValues.identification,
        rolId: Number(formValues.rolId)
      };

      console.log('Preparado para crear usuario:', newUser);

      // 2. Llamamos al servicio
      this.userService.createUser(newUser).subscribe({
        next: (response) => {
          console.log('Respuesta del backend:', response.message);

          this.hideModal();
          this.loadData(); // Recargamos la tabla para ver al nuevo usuario

          // Opcional: Aquí podrías mostrar un Toast/SweetAlert de éxito
        },
        error: (err) => {
          console.error('Error al crear usuario:', err);
          // Opcional: Aquí podrías mostrar un Toast/SweetAlert de error
        }
      });
    }
  }

  private showModal() {
    const modalEl = document.getElementById('userModal');
    if (modalEl) {
      const modal = new bootstrap.Modal(modalEl);
      modal.show();
    }
  }

  private hideModal() {
    const modalEl = document.getElementById('userModal');
    if (modalEl) {
      const modal = bootstrap.Modal.getInstance(modalEl);
      if (modal) {
        modal.hide();
      }
    }
  }
}
