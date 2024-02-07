import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import cors from 'cors';
import axios from 'axios';
import uiComponents from '@/components/ui/index';

axios.defaults.baseURL = 'http://localhost:5173/api/swagger';

const app = createApp(App);
app.use(cors());

uiComponents.forEach(component => {
    app.component(component.name, component);
})

app.mount('#app')
