# Etapa 1: Construcción (Build)
FROM node:20 AS build
WORKDIR /app

# Copia package.json e instala dependencias
COPY package*.json ./
RUN npm install

# Copia el código fuente y compila para producción
COPY . .
RUN npm run build --configuration=production

# Etapa 2: Servidor Web (Nginx)
FROM nginx:alpine
# IMPORTANTE: Cambia "nombre-de-tu-app" por el nombre que aparece en tu angular.json dentro de "outputPath"
COPY --from=build /app/dist/viamatica-frontend/browser /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
