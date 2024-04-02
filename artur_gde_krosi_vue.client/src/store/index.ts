import { createStore } from 'vuex';
import type { RootState } from '@/store/types'

import productsCatalog from '@/store/modules/productsCatalog/index';

export default createStore<RootState>({
    state: {
        showPreloader: false,
        showSearchPanel: false,
        showLogInPopup: false,
        loginPopupMode: ''
    },

    mutations: {
        setPreloaderVisibility(state: RootState, value: boolean) {
            state.showPreloader = value;
        },

        setSearchPanelVisibility(state: RootState, value: boolean) {
            state.showSearchPanel = value;
        },

        setLogInPopupVisibility(state: RootState, value: boolean) {
            state.showLogInPopup = value;
        },

        openLoginPopup(state: RootState, mode: string) {
            state.showLogInPopup = true;
            state.loginPopupMode = mode;
        }
    },

    modules: {
        productsCatalog
    },
})