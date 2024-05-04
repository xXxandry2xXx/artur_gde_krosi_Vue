import type { MutationTree } from 'vuex';
import type { UserCartState } from '@/store/modules/cart/types';

export const mutations: MutationTree<UserCartState> = {

    setProductsInCart(state, products) {
        state.itemsInCart = products;
    },

    setCurrentChosenVariantId(state, id) {
        state.chosenVariantId = id;
    },

    setTotalPrice(state, price) {
        state.totalPrice = price / 100;
    },
}