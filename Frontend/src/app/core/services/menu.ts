import { Injectable, signal } from '@angular/core';

export interface MenuItem {
  id: number;
  title: string;
  label?: string;
  route: string;
  icon: string;
  children?: MenuItem[];
}

@Injectable({
  providedIn: 'root'
})
export class Menu {
  private readonly MENU_KEY = 'app_menu';

  // Estado reactivo del menú mediante Signal
  public menuItems = signal<MenuItem[]>(this.getStoredMenu());

  // Mapea los nombres de íconos del backend a clases de Bootstrap Icons (bi-*)
  private mapIcon(iconName: string): string {
    const iconMap: Record<string, string> = {
      'home': 'bi-house-door',
      'dashboard': 'bi-speedometer2',
      'people': 'bi-people-fill',
      'assignment': 'bi-card-checklist',
      'person': 'bi-person-fill',
      'point_of_sale': 'bi-cash-coin'
    };
    return iconMap[iconName?.toLowerCase()] || 'bi-circle';
  }

  // Guarda y formatea el menú enviado desde el Login
  public setMenu(menu: MenuItem[]): void {
    const formattedMenu = menu.map(item => ({
      ...item,
      label: item.title || item.label,
      icon: this.mapIcon(item.icon),
      children: item.children?.map(child => ({
        ...child,
        label: child.title || child.label,
        icon: this.mapIcon(child.icon)
      }))
    }));

    localStorage.setItem(this.MENU_KEY, JSON.stringify(formattedMenu));
    this.menuItems.set(formattedMenu);
  }

  private getStoredMenu(): MenuItem[] {
    const stored = localStorage.getItem(this.MENU_KEY);
    return stored ? JSON.parse(stored) : [];
  }

  public clearMenu(): void {
    localStorage.removeItem(this.MENU_KEY);
    this.menuItems.set([]);
  }
}
