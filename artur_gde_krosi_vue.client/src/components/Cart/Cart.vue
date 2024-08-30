<template>
    <div class="app-subheader-cart-content" :key="currentCart.itemsInCart.length">
        <div class="cart-button" 
                @mouseenter="showCartPanel"
                @mouseleave="hideCartPanel"
                ref="cartPanelButton"
                @click="toggleCartMobile">
            <span class="cart-capacity" v-show="currentTotalProductQuantity > 0">{{ currentTotalProductQuantity }}</span>
            <span class="app-subheader-button"><font-awesome-icon :icon="['fas', 'cart-shopping']" /></span>
        </div>
        <transition :name="isMobile() || this.isTablet() ? 'slide' : 'fade'">
            <div class="cart-wrapper" ref="cartPanel" v-show="isCartPanelVisible" @mouseenter="showCartPanel" @mouseleave="hideCartPanel">
                <div class="cart-items">
                    <CartItem v-if="currentCart.itemsInCart.length > 0" v-for="item in currentCart.itemsInCart" :cartItem="item" @click="toggleCartMobile"/>
                    <p v-else class="cart-is-empty-text">Корзина пуста.</p>
                </div>
                <div class="cart-order-section">
                    <p v-if="currentCart.itemsInCart.length > 0">В корзине {{ currentTotalProductQuantity }} {{ declension }} на сумму {{ $store.state.cart.localCart.totalPrice }}₽</p>
                    <BorderedButton v-if="currentCart.itemsInCart.length > 0" @click="openInfoPopup">Оформить заказ</BorderedButton>
                    <BorderedButton v-if="isMobile() || this.isTablet()" @click="toggleCartMobile">Закрыть</BorderedButton>
                </div>
            </div>
        </transition>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapGetters, mapActions, mapMutations} from 'vuex';
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
            ...mapActions([
                'gatherPrices', 
                'fetchCartPrices', 
                'loadLocalCartData', 
                'fetchUserCart', 
                'getLocalTotalPrice'
            ]),
            ...mapGetters([
                'getServerCart',
                'getLocalCart',
                'getLocalCartTotalQuantity',
                'getLocalCartTotalPrice',
                'getServerCartTotalQuantity',
                'getServerCartTotalPrice',
                'isUserAuthorized',
                'isMobile',
                'isTablet'
            ]),
            ...mapMutations(['setPopupVisibility', 'setPopupMode']),

            toggleCartMobile(this: any) {
                if (this.isMobile() || this.isTablet()) this.isCartPanelVisible = !this.isCartPanelVisible;
            },

            showCartPanel(this: any, event: Event) {
                if (!this.isMobile() && !this.isTablet()) {
                    let target = event.target;
                    if (target === this.$refs.cartPanel || target === this.$refs.cartPanelButton) this.clearCartPanelTimeout(this.cartPanelTimeout);
                    this.isCartPanelVisible = true;
                }
            },

            hideCartPanel() {
                if (!this.isMobile() && !this.isTablet()) this.setCartPanelTimeout();
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
            },

            openInfoPopup() {
                this.setPopupMode('why-disabled');
                this.setPopupVisibility(true);
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
            this.fetchUserCart();
            this.loadLocalCartData();
        }
    })
</script>