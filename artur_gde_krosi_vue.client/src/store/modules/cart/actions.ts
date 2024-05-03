import axios from 'axios';
import type { ActionTree } from 'vuex';
import type { RootState } from '@/store/types';
import type { UserCartState } from '@/store/modules/cart/types';

export const actions: ActionTree<UserCartState, RootState> = {

    async fetchUserCart() {
        if (this.getters.isUserAuthorized) {
            try {
                let authorizedUser = this.getters.getAuthorizedUser;
                const response = await axios.get('http://localhost:5263/api/ShoppingСart', {
                    params: {
                        'name': authorizedUser.userName
                    },
                    headers: {
                        'accept': '*/*'
                    }
                });

                if (response.status === 200) {
                    this.commit('setProductsInCart', response.data);
                    //this.dispatch('gatherPrices');
                }

            } catch (error) {
                console.log(error);
            }
        } else {
            console.log('Нужно сделать локальную корзину');
        }
    },

    async addItemToCart({ state }: { state: UserCartState }, itemVariantID) {
        if (this.getters.isUserAuthorized) {
            try {
                let authorizedUser = this.getters.getAuthorizedUser;
                const response = await axios.post(
                    'http://localhost:5263/api/ShoppingСart',
                    '',
                    {
                        params: {
                            'name': authorizedUser.userName,
                            'VariantId': itemVariantID
                        },
                        headers: {
                            'accept': '*/*',
                            'content-type': 'application/x-www-form-urlencoded'
                        }
                    }
                );
                if (response.status === 200) this.dispatch('fetchUserCart');

            } catch (error: any) {
                if (error.response.status === 400) {
                    this.dispatch('increaseQuantityIfItemExists', itemVariantID);
                } else {
                    console.log(error);
                }
            }
        } else {
            console.log('Пользователь не авторизован, нужна локальная корзина')
        }
    },

    async increaseItemQuantity({ state }: { state: UserCartState }, form) {
        try {
            const response = await axios.put(
                'http://localhost:5263/api/ShoppingСart',
                form,
                {
                    headers: {
                        'accept': '*/*',
                        'Content-Type': 'multipart/form-data'
                    }
                }
            );
            if (response.status === 200) this.dispatch('fetchUserCart');

        } catch (error) {
            console.log(error)
        }
    },

    increaseQuantityIfItemExists({ state }: { state: UserCartState }, itemVariantID) {
        const currentCartItem = state.itemsInCart.find((item: any) => item.variantId === itemVariantID);
        if (currentCartItem !== undefined) {
            const form = new FormData();

            form.append('ShoppingСartId', currentCartItem.shoppingСartId.toString());
            form.append('quantity', (currentCartItem.quantity + 1).toString());

            this.dispatch('increaseItemQuantity', form);
        }
    },

    async removeItemFromCart({ state }: { state: UserCartState }, cartItemID) {
        try {
            const response = await axios.delete('http://localhost:5263/api/ShoppingСart', {
                headers: {
                    'accept': '*/*',
                    'Content-Type': 'multipart/form-data'
                },
                data: `{\n  "ShoppingСartId": "${cartItemID}" \n}`
            });
        } catch (error) {
            console.log(error)
        }
    },

    async fetchCartPrices({ state }: { state: UserCartState }, item: any) {
        try {
            const response = await axios.get('http://localhost:5263/api/Product/GetProduct', {
                params: {
                    'ProductId': item.productId
                },
                headers: {
                    'accept': '*/*'
                }
            });

            let currentVariantPrice = (response.data.variants.find((variant: any) => variant.variantId === item.variantId).prise / 100) * item.quantity;

            this.commit('addPrice', currentVariantPrice);
        } catch (error) {
            console.log(error)
        }
    },

    gatherPrices({ state }: { state: UserCartState }) {
        let items = this.getters.getCartItems;
        this.commit('setPrices', [0])

        items.forEach((item: any) => {
            this.dispatch('fetchCartPrices', item);
        });
    },
}