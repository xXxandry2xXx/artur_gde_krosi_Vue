import { createStore } from 'vuex';
import type { RootState } from '@/store/types'

import productsCatalog from '@/store/modules/productsCatalog/index';
import authorization from '@/store/modules/authorization/index';
import account from '@/store/modules/account/index';
import cart from '@/store/modules/cart/index';

export default createStore<RootState>({
    state: {
        authorizedUser: null,
        showPreloader: false,
        showSearchPanel: true,
        showPopup: false,
        popupMode: ''
    },

    mutations: {
        setPreloaderVisibility(state: RootState, value: boolean) {
            state.showPreloader = value;
        },

        setSearchPanelVisibility(state: RootState, value: boolean) {
            state.showSearchPanel = value;
        },

        setPopupVisibility(state, value: boolean) {
            state.showPopup = value;
        },

        setPopupMode(state, value: string) {
            state.popupMode = value;
        },

        setUser(state, user) {
            if(user !== undefined) state.authorizedUser = user;
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
        authorization,
        account,
        cart
    },
})