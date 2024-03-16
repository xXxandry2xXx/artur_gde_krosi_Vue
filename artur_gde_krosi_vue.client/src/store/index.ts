import { createStore } from 'vuex';
import type { RootState } from '@/store/types'

import productsCatalog from '@/store/modules/productsCatalog/index';

export default createStore<RootState>({
    state: {
        showPreloader: false,
    },

    mutations: {
        setPreloaderVisibility(state, value) {
            state.showPreloader = value;
        }
    },

    modules: {
        productsCatalog
    },
})