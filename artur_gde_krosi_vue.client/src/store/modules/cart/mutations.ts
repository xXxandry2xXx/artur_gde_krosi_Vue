import type { MutationTree } from 'vuex';
import type { UserCartState } from '@/store/modules/cart/types';

export const mutations: MutationTree<UserCartState> = {

    setProductsInCart(state, products) {
        state.itemsInCart = products;
    },

    setCurrentChosenVariantId(state, id) {
        state.chosenVariantId = id;
    },

    setPrices(state, pricesArray) {
        state.currentCartPrices = pricesArray;
    },

    addPrice(state, price) {
        state.currentCartPrices.push(price);
    }
}