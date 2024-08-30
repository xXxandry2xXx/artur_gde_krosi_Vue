import type { MutationTree } from 'vuex';
import type { UserCartState } from '@/store/modules/cart/types';
import type { CartItemInterface } from '@/store/modules/cart/types';

export const mutations: MutationTree<UserCartState> = {
    setLocalCart(state, cartData) {
        state.localCart = cartData;
    },

    setServerCartProducts(state, products) {
        state.serverCart.itemsInCart = products;
    },

    setServerCartTotalPrice(state, price) {
        state.serverCart.totalPrice = price / 100;
    },

    setLocalCartTotalPrice(state, price) {
        state.localCart.totalPrice = price;
    },

    setCurrentChosenProductId(state, id) {
        state.chosenProductId = id;
    },

    setCurrentChosenVariantId(state, id) {
        state.chosenVariantId = id;
    },

    addItemToLocalCart(state, newItem: CartItemInterface) {
        const existingItem = state.localCart.itemsInCart.find((existingItem: CartItemInterface) => existingItem.variantId === newItem.variantId);
        if (existingItem) {
            existingItem.quantity++;
        } else {
            state.localCart.itemsInCart.push(newItem);
        }
    },

    removeItemFromCart(state, item) {
        let itemIndex = state.localCart.itemsInCart.indexOf(item)
        state.localCart.itemsInCart.splice(itemIndex, 1);
    },
}