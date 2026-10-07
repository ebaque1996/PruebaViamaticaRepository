import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-dashboard.html',
  styleUrls: ['./admin-dashboard.scss']
})
export class AdminDashboard {
  // 1. Indicadores de Usuarios (Mock data)
  userIndicators = signal({
    activeSessions: 14,
    inactiveUsers: 5,
    blockedUsers: 2
  });

  // 2. Filtros de Fecha
  // Por defecto, filtramos el mes actual
  today = new Date();
  filterDates = signal({
    start: new Date(this.today.getFullYear(), this.today.getMonth(), 1).toISOString().split('T')[0],
    end: this.today.toISOString().split('T')[0]
  });

  // 3. Indicadores Operativos (Cajas, Cajeros, Gestores)
  operationIndicators = signal({
    activeBoxes: 8,          // Cajas operativas
    totalCashiers: 12,       // Cajeros que han trabajado en el rango
    totalManagers: 3,        // Gestores activos
    shiftsAssigned: 345,     // Turnos asignados por gestores
    shiftsCompleted: 310     // Turnos atendidos en cajas
  });

  // Método que se llama al cambiar las fechas
  applyDateFilter() {
    console.log('Filtrando datos desde', this.filterDates().start, 'hasta', this.filterDates().end);
    // Aquí llamarías a tu servicio: this.dashboardService.getMetrics(start, end).subscribe(...)
    // Por ahora simulamos un cambio visual:
    this.operationIndicators.set({
      activeBoxes: Math.floor(Math.random() * 10) + 1,
      totalCashiers: Math.floor(Math.random() * 15) + 5,
      totalManagers: 3,
      shiftsAssigned: Math.floor(Math.random() * 500) + 100,
      shiftsCompleted: Math.floor(Math.random() * 450) + 90
    });
  }
}
