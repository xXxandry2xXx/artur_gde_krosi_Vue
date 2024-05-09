import type { GetterTree } from 'vuex';
import type { RootState } from '@/store/types';
import type { UserCartState } from '@/store/modules/cart/types';

export const getters: GetterTree<UserCartState, RootState> = {
    getServerCartItems: state => {
        return state.serverCart.itemsInCart;
    },

    getChosenVariant: state => {
        return state.chosenVariantId;
    },

    getChosenProductId: state => {
        return state.chosenProductId;
    },

    getServerCart: state => {
        return state.serverCart;
    },

    getLocalCart: state => {
        const localCart = state.localCart;
        return localCart;
    },

    getLocalCartTotalQuantity: state => {
        return state.localCart.itemsInCart.reduce((accumulator: any, currentValue: any) => {
            let totalQuantity = accumulator + currentValue.quantity;
            return totalQuantity;
        }, 0)
    },

    getLocalCartTotalPrice: state => {
        return state.localCart.totalPrice;
    },

    getServerCartTotalQuantity: state => {
        return state.serverCart.itemsInCart.reduce((accumulator: any, currentValue: any) => {
            let totalQuantity = accumulator + currentValue.quantity;
            return totalQuantity;
        }, 0)
    },

    getServerCartTotalPrice: state => {
        return state.serverCart.totalPrice;
    },
}