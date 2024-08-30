import { fileURLToPath, URL } from 'node:url';

import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';



export default defineConfig({
    plugins: [vue()],
    server: {
        host: '0.0.0.0', // прослушивать все интерфейсы
        port: 4173,
    },
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url))
        }
    }
})