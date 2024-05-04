import type { GetterTree } from 'vuex';
import type { RootState } from '@/store/types';
import type { UserCartState } from '@/store/modules/cart/types';

export const getters: GetterTree<UserCartState, RootState> = {
    getCartItems: state => {
        return state.itemsInCart;
    },

    getChosenVariant: state => {
        return state.chosenVariantId;
    },

    getTotalQuantity: state => {
        return state.itemsInCart.reduce((accumulator: any, currentValue: any) => {
            let totalQuantity = accumulator + currentValue.quantity;
            return totalQuantity;
        }, 0)
    },

    getTotalCartPrice: state => {
        return state.totalPrice;
    }
}