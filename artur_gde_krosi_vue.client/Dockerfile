FROM node:18

# Установка зависимостей
WORKDIR /app
COPY package*.json .
RUN npm install vite@3.2.2
RUN npm install @fortawesome/fontawesome-svg-core@1.x  --force  --legacy-peer-deps
RUN npm install @fortawesome/vue-fontawesome --force  --legacy -peer-deps
RUN npm install @fortawesome/free-solid-svg-icons --force  --legacy-peer-deps
RUN npm install @fortawesome/free-brands-svg-icons --force  --legacy-peer-deps
RUN npm install vuex --force  --legacy-peer-deps
RUN npm install vue-router --force  --legacy-peer-deps
RUN npm install axios --force  --legacy-peer-deps
RUN npm install vue3-touch-events --force  --legacy-peer-deps

RUN npm install --save-dev --ignore-scripts esbuild --force  --legacy-peer-deps
RUN npm install typescript@5.3.3 --force  --legacy-peer-deps

#RUN npm i vite-plugin-mkcert -D --force  --legacy-peer-deps
#
# Копирование кода приложения
COPY . . 

## Настройка переменных окружения
#ENV NODE_ENV=production
EXPOSE 4173

# Запуск приложения
CMD ["npm", "run", "preview"]