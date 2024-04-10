import { createStore } from 'vuex';
import type { RootState } from '@/store/types'

import productsCatalog from '@/store/modules/productsCatalog/index';
import authorization from '@/store/modules/authorization/index';

export default createStore<RootState>({
    state: {
        authorizedUser: null,
        showPreloader: false,
        showSearchPanel: false,
    },

    mutations: {
        setPreloaderVisibility(state: RootState, value: boolean) {
            state.showPreloader = value;
        },

        setSearchPanelVisibility(state: RootState, value: boolean) {
            state.showSearchPanel = value;
        },

        setUser(state, user) {
            state.authorizedUser = user;
        }
    },

    getters: {
        isUserAuthorized: () => {
            return localStorage.getItem('token') !== null;
        },

        getAuthorizedUser: () => {
            let userData = localStorage.getItem('userData');
            if (userData !== null) {
                return JSON.parse(userData);
            }
        }
    },

    modules: {
        productsCatalog,
        authorization
    },
})