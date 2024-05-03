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

    countTotalCartPrice: state => {
        const totalPrice = state.currentCartPrices.reduce((accumulator: any, currentValue: any) => {
            let price = accumulator + currentValue;
            return price;
        }, 0)

        return totalPrice;
    }
}