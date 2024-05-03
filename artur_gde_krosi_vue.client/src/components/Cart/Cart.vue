<template>
    <div class="app-subheader-cart-content">
        <button class="cart-button" @mouseenter="showCartPanel" @mouseleave="hideCartPanel" ref="cartPanelButton">
            <span class="cart-capacity">{{ getTotalQuantity() }}</span>
            <span class="app-subheader-button"><font-awesome-icon :icon="['fas', 'cart-shopping']" /></span>
        </button>
        <transition name="fade">
            <div class="cart-wrapper" ref="cartPanel" v-show="isCartPanelVisible" @mouseenter="showCartPanel" @mouseleave="hideCartPanel">
                <div class="cart-items">
                    <CartItem v-if="$store.state.cart.itemsInCart.length > 0" v-for="item in $store.state.cart.itemsInCart" :cartItemId="item.shoppingСartId" :productId="item.productId" :variantId="item.variantId" />
                    <p v-else class="cart-is-empty-text">Корзина пуста.</p>
                </div>
                <div class="cart-order-section" v-if="$store.state.cart.itemsInCart.length > 0">
                    <p>{{ getTotalQuantity() }} {{ declension }} на сумму </p>
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
            ...mapActions(['gatherPrices', 'fetchCartPrices']),
            ...mapGetters(['getTotalQuantity', 'getCartItems', 'countTotalCartPrice']),

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
        },

        computed: {
            declension(this: any) {
                let totalQuantity = this.getTotalQuantity();
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
        }
    })
</script>