import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import uiComponents from '@/components/ui/index';

const app = createApp(App);

uiComponents.forEach(component => {
    app.component(component.name, component);
})

app.mount('#app')
