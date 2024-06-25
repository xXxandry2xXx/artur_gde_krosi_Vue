import { fileURLToPath, URL } from 'node:url';
import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-vue';
import fs from 'fs';
import { keyFilePath, certFilePath } from './vite.config';

export default defineConfig({
    plugins: [
        plugin({
            template: {
                compilerOptions: {
                    isCustomElement: (tag) => ['AGK-'].includes(tag),
                },
            }
        })
    ],
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url))
        }
    },
    server: {
        proxy: {
            devServer: {
                target: 'http://192.144.14.63/',
                secure: false
            }
        },
        port: 5173,
        https: {
            key: fs.readFileSync(keyFilePath),
            cert: fs.readFileSync(certFilePath),
        }
    }
});
