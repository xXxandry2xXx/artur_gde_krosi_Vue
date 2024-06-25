FROM node:18-alpine

# Установка зависимостей
WORKDIR /app
COPY package*.json .
RUN npm install

# Копирование кода приложения
COPY . .

## Настройка переменных окружения
#ENV NODE_ENV=production

# Запуск приложения
CMD ["npm", "run", "run-p type-check \"build-only {@}\" --"]