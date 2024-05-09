import type { Module } from 'vuex';
import type { RootState } from '@/store/types';
import type { UserCartState } from '@/store/modules/cart/types';
import { mutations } from '@/store/modules/cart/mutations';
import { getters } from '@/store/modules/cart/getters';
import { actions } from '@/store/modules/cart/actions';

const state: UserCartState = {
    serverCart: {
        itemsInCart: [],
        totalPrice: 0
    },

    localCart: {
        itemsInCart: [],
        totalPrice: 0
    },

    chosenVariantId: '',
    chosenProductId: ''
}

const cart: Module<UserCartState, RootState> = {
    state,
    mutations,
    getters,
    actions
}

export default cart;