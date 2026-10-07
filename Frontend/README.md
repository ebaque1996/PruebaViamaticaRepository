# Viamatica Frontend (Angular 17+)

Aplicación cliente desarrollada en Angular para la gestión de usuarios, roles e identificaciones dentro del sistema Viamatica.

---

## 🛠️ Tecnologías y Características

- **Framework:** Angular 17+ (Componentes Standalone y Signals)
- **Estilos:** Bootstrap 5 & Bootstrap Icons
- **Servidor de Producción:** Nginx (dentro del contenedor Docker)
- **Formularios:** Reactive Forms con validaciones integradas (incluyendo `identification`)

---

## 🛠️ Requisitos Previos

- [Node.js](https://nodejs.org/) (Versión 18 o 20 LTS recomendada)
- [Angular CLI](https://angular.dev/) (`npm install -g @angular/cli`)

---

## 🚀 Ejecución en Desarrollo (Sin Docker)

1. Instalar dependencias:

```bash
npm install
```

2. Verificar la URL de la API en el servicio correspondiente (`user.service.ts`):
   - Debe apuntar a la URL del backend local (ej. `http://localhost:5000/api`).

3. Iniciar el servidor de desarrollo local:

```bash
ng serve
```

4. Navegar en el explorador a: `http://localhost:4200`

---

## 📦 Compilación para Producción

Para generar el paquete optimizado de producción:

```bash
ng build --configuration=production
```

Los archivos compilados se generarán en la carpeta `dist/`.

---

## 🐳 Docker (Standalone)

Si deseas construir y ejecutar únicamente el contenedor del frontend:

```bash
docker build -t viamatica-frontend -f Dockerfile .
docker run -d -p 4200:80 --name frontend-app viamatica-frontend
```
