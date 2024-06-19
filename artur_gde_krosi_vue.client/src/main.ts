import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import uiComponents from '@/components/ui';
import store from '@/store';
import router from '@/router/router'

import { library } from '@fortawesome/fontawesome-svg-core'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { fas } from "@fortawesome/free-solid-svg-icons";
import { fab } from "@fortawesome/free-brands-svg-icons";

import Vue3TouchEvents, { type Vue3TouchEventsOptions } from "vue3-touch-events";

library.add(fas, fab);

const app = createApp(App);

app.component('font-awesome-icon', FontAwesomeIcon);

uiComponents.forEach(component => {
    app.component(component.name, component);
})

app
    .use(store)
    .use(router)
    .use(Vue3TouchEvents, { disableClick: true })
    .mount('#app')
