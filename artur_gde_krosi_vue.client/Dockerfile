# Используем официальный образ Node.js 20 в качестве базового
FROM node:20 AS build

# Устанавливаем рабочую директорию внутри контейнера
WORKDIR /app

# Копируем файлы package.json и package-lock.json (если есть) в контейнер
COPY package*.json ./

# Устанавливаем зависимости проекта
RUN npm install -g patch-package
RUN npm install vue3-touch-events
RUN npm install

# Копируем остальные файлы проекта в контейнер
COPY . .

# Выполняем сборку проекта
RUN npm run build-only

FROM nginx:1.16.0-alpine
COPY --from=build /app/dist /usr/share/nginx/html
RUN rm /etc/nginx/conf.d/default.conf
COPY nginx.conf /etc/nginx/conf.d
EXPOSE 80

# Запускаем Nginx
CMD ["nginx", "-g", "daemon off;"]