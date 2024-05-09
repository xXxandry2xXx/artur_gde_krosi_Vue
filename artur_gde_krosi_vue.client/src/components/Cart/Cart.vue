<template>
    <div class="app-subheader-cart-content" :key="currentCart.itemsInCart.length">
        <button class="cart-button" @mouseenter="showCartPanel" @mouseleave="hideCartPanel" ref="cartPanelButton">
            <span class="cart-capacity" v-show="currentTotalProductQuantity > 0">{{ currentTotalProductQuantity }}</span>
            <span class="app-subheader-button"><font-awesome-icon :icon="['fas', 'cart-shopping']" /></span>
        </button>
        <transition name="fade">
            <div class="cart-wrapper" ref="cartPanel" v-show="isCartPanelVisible" @mouseenter="showCartPanel" @mouseleave="hideCartPanel">
                <div class="cart-items">
                    <CartItem v-if="currentCart.itemsInCart.length > 0" v-for="item in currentCart.itemsInCart" :cartItem="item" />
                    <p v-else class="cart-is-empty-text">Корзина пуста.</p>
                </div>
                <div class="cart-order-section" v-if="currentCart.itemsInCart.length > 0">
                    <p>В корзине {{ currentTotalProductQuantity }} {{ declension }} на сумму {{ currentTotalCartPrice }}₽</p>
                    <BorderedButton>Оформить заказ</BorderedButton>
                </div>
            </div>
        </transition>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapGetters, mapActions } from 'vuex';
    import CartItem from '@/components/Cart/CartItem.vue';

    export default defineComponent({

        data() {
            return {
                isCartPanelVisible: false,
                cartPanelTimeout: null
            }
        },

        components: { CartItem },

        methods: {
            ...mapActions(['gatherPrices', 'fetchCartPrices', 'loadLocalCartData']),
            ...mapGetters([
                'getServerCart',
                'getLocalCart',
                'getLocalCartTotalQuantity',
                'getLocalCartTotalPrice',
                'getServerCartTotalQuantity',
                'getServerCartTotalPrice',
                'isUserAuthorized',
            ]),

            showCartPanel(this: any, event: Event) {
                let target = event.target;
                if (target === this.$refs.cartPanel || target === this.$refs.cartPanelButton) this.clearCartPanelTimeout(this.cartPanelTimeout);
                this.isCartPanelVisible = true;
            },

            hideCartPanel() {
                this.setCartPanelTimeout();
            },

            setCartPanelTimeout(this: any) {
                this.cartPanelTimeout = setTimeout(() => this.isCartPanelVisible = false, 500);
            },

            clearCartPanelTimeout(this: any) {
                this.cartPanelTimeout = clearTimeout(this.cartPanelTimeout);
            },

            initCartInfo(this: any) {
                if (this.isUserAuthorized()) {
                    this.currentCart = this.getServerCart();
                    this.currentTotalProductQuantity = this.getServerCartTotalQuantity();
                    this.currentTotalCartPrice = this.getServerCartTotalPrice();
                } else {
                    this.currentCart = this.getLocalCart();
                    this.currentTotalProductQuantity = this.getLocalCartTotalQuantity();
                    this.currentTotalCartPrice = this.getLocalCartTotalPrice();
                }
            }
        },

        computed: {
            declension(this: any) {
                let totalQuantity = this.currentTotalProductQuantity;
                let lastCharacter = totalQuantity.toString().split('').reverse()[0];
                if (totalQuantity > 11 || totalQuantity == 1) {
                    if (lastCharacter === '1') return 'товар';
                }
                if (totalQuantity > 14 || totalQuantity <= 4) {
                    if (lastCharacter > 1 && lastCharacter <= 4) return 'товара';
                }
                if (totalQuantity > 4) {
                    return 'товаров';
                }
            },

            currentCart(this: any) {
                if (this.isUserAuthorized()) {
                    return this.getServerCart();
                } else {
                    return this.getLocalCart();
                }
            },

            currentTotalProductQuantity(this: any) {
                if (this.isUserAuthorized()) {
                    return this.getServerCartTotalQuantity();
                } else {
                    return this.getLocalCartTotalQuantity();
                }
            },

            currentTotalCartPrice(this: any) {
                if (this.isUserAuthorized()) {
                    return this.getServerCartTotalPrice();
                } else {
                    return this.getLocalCartTotalPrice();
                }
            }
        },

        mounted() {
            this.loadLocalCartData();
        }
    })
</script>