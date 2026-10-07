export interface Role {
  rolId: number;
  rolName: string;
}

export interface UserAdmin {
  userId: number;
  username: string;
  email: string;
  identification: string; // ¡Nuevo campo!
  statusId: 'ACT' | 'INA' | 'BLO';
  rolId: number;
  creationDate: string;

  // Esta propiedad no viene del backend, la agregaremos nosotros en el frontend
  // cruzando la info con la lista de roles para mostrarla en la tabla
  rolNameTemp?: string;
}

export interface CreateUserDto {
  username: string;
  email: string;
  password?: string;
  identification: string;
  rolId: number;
}
