import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import uiComponents from '@/components/ui';
import store from '@/store';
import router from '@/router/router'
const app = createApp(App);

uiComponents.forEach(component => {
    app.component(component.name, component);
})

app
    .use(store)
    .use(router)
    .mount('#app')
