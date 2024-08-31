import axios from 'axios';
import apiUrl from '@/helper'
import type { ActionTree } from 'vuex';
import type { RootState } from '@/store/types';
import type { UserCartState } from '@/store/modules/cart/types';
import type { CartItemInterface } from '@/store/modules/cart/types';

export const actions: ActionTree<UserCartState, RootState> = {

    loadLocalCartData() {
        const cachedCart = localStorage.getItem('localUserCart');
        if (cachedCart !== null) this.commit('setLocalCart', JSON.parse(cachedCart));
        this.dispatch('getLocalTotalPrice');
    },

    async fetchUserCart({ state }: { state: UserCartState }) {
        if (this.getters.isUserAuthorized) {
            try {
                const token = this.getters.getAuthorizedUserToken.slice(1, -1); 
                const response = await axios.get(apiUrl + '/ShoppingСart', {
                    maxBodyLength: Infinity,
                    headers: {
                        'accept': '*/*',
                        'Authorization': 'Bearer ' + token
                    }
                });
                if (response.status === 200) {
                    this.commit('setServerCartProducts', response.data.shoppingCartList);
                    this.commit('setServerCartTotalPrice', response.data.totalPrise);
                }

            } catch (error) {
                console.log(error)
            }
        } else {
            this.dispatch('getLocalTotalPrice');
        }
    },

    async addItemToCart({ state }: { state: UserCartState }, itemVariantID) {
        if (state.chosenProductId !== null && state.chosenVariantId !== null) {
            if (this.getters.isUserAuthorized) {
                try {
                    const token = this.getters.getAuthorizedUserToken.slice(1, -1); 
                    const response = await axios.post(
                        apiUrl + '/ShoppingСart',
                        '',
                        {
                            headers: {
                                'accept': '*/*',
                                'Authorization': 'Bearer ' + token,
                                'VariantId': itemVariantID.toString(),
                                'Content-Type': 'application/x-www-form-urlencoded'
                            }
                        }
                    );
                    if (response.status === 200) this.dispatch('fetchUserCart');

                } catch (error: any) {
                    if (error.response.status === 400) {
                        this.dispatch('increaseItemQuantity', itemVariantID);
                    } else {
                        console.log(error);
                    }
                }
            } else {
                const chosenProductId = this.getters.getChosenProductId;

                const newItem: CartItemInterface = {
                    shoppingСartId: 'none',
                    quantity: 1,
                    availability: true,
                    variantId: itemVariantID,
                    productId: chosenProductId
                }

                this.commit('addItemToLocalCart', newItem);
                await this.dispatch('fetchUserCart');
                localStorage.setItem('localUserCart', JSON.stringify(state.localCart));
            }
        } else {
            console.log('Товара нет в наличии')
        }
    },

    async increaseItemQuantity({ state }: { state: UserCartState }, itemVariantID) {
        if (this.getters.isUserAuthorized) {
            try {
                const token = this.getters.getAuthorizedUserToken.slice(1, -1); 
                const currentCartItem = state.serverCart.itemsInCart.find((item: any) => item.variantId === itemVariantID);
                if (currentCartItem !== undefined) {
                    const response = await axios.put(
                        apiUrl + '/ShoppingСart',
                        '',
                        {
                            params: {
                                'ShoppingСartId': currentCartItem.shoppingСartId.toString(),
                                'quantity': (currentCartItem.quantity + 1).toString()
                            },
                            headers: {
                                'accept': '*/*',
                                'Authorization': 'Bearer ' + token
                            }
                        }
                    );
                    if (response.status === 200) this.dispatch('fetchUserCart');
                }
            } catch (error) {
                console.log(error)
            }
        } else {
            console.log('Пользователь не авторизован');
        }
    },

    async removeItemFromCart({ state }: { state: UserCartState }, cartItemID) {
        if (this.getters.isUserAuthorized) {
            try {
                const token = this.getters.getAuthorizedUserToken.slice(1, -1); 
                const response = await axios.delete(apiUrl + '/ShoppingСart', {
                    params: {
                        'ShoppingСartId': cartItemID.toString()
                    },
                    headers: {
                        'accept': '*/*',
                        'Authorization': 'Bearer ' + token
                    }
                });
                if (response.status === 200) this.dispatch('fetchUserCart');
            } catch (error) {
                console.log(error)
            }
        } else {
            const removeableItem = state.localCart.itemsInCart.find(item => item.variantId === cartItemID);

            this.commit('removeItemFromCart', removeableItem);
            await this.dispatch('fetchUserCart');
            localStorage.setItem('localUserCart', JSON.stringify(state.localCart));
        }
    },

    async getLocalTotalPrice({ state }: { state: UserCartState }) {
        let prices: Array<any> = [];

        for (const item of state.localCart.itemsInCart) {
            const itemResponseData = await this.dispatch('fetchVariant', item.variantId);
            const itemPrice = (itemResponseData.data.prise / 100) * item.quantity;
            prices.push(itemPrice);
        }

        const totalLocalPrice = prices.reduce((accumulator: any, currentValue: any) => {
            let totalPrice = accumulator + currentValue;
            return totalPrice;
        }, 0)

        this.commit('setLocalCartTotalPrice', totalLocalPrice);
    }
}