import type { Module } from 'vuex';
import type { RootState } from '@/store/types';
import type { UserCartState } from '@/store/modules/cart/types';
import { mutations } from '@/store/modules/cart/mutations';
import { getters } from '@/store/modules/cart/getters';
import { actions } from '@/store/modules/cart/actions';

const state: UserCartState = {
    itemsInCart: [],
    currentCartPrices: [] as Array<number>,
    chosenVariantId: ''
}

const cart: Module<UserCartState, RootState> = {
    state,
    mutations,
    getters,
    actions
}

export default cart;