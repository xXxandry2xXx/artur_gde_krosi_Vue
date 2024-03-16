<template>
    <div class="main-page-popular-products">
        <h1>ПОПУЛЯРНОЕ</h1>
        <div class="main-page-popular-products-list-viewport">
            <div class="main-page-popular-products-list" ref="popularProductsList" @mouseenter="removeScrollInterval" @mouseleave="setScrollInterval">
                <div class="product" style="display: none" ref="popularProductCard"></div>
                <Product v-for="(product, index) in popularProducts" :product="product" />
            </div>
        </div>
        <div class="main-page-popular-products-scroll-buttons">
            <button class="main-page-scroll-button" @click="scrollProducts('left')" @mouseenter="removeScrollInterval" @mouseleave="setScrollInterval">
                <font-awesome-icon :icon="['fas', 'chevron-left']" />
            </button>
            <button class="main-page-scroll-button" @click="scrollProducts('right')" @mouseenter="removeScrollInterval" @mouseleave="setScrollInterval">
                <font-awesome-icon :icon="['fas', 'chevron-right']" />
            </button>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapGetters, mapActions, mapMutations } from 'vuex';
    import type { NavigationGuardNext, RouteLocationNormalized } from 'vue-router';
    import Product from '@/components/ProductsPage/Product.vue';

    export default defineComponent({

        components: { Product },

        data() {
            return {
                popularProducts: [],
                viewportProductAmount: 3,
                currentOffset: 0,
                productWidth: 0,
                productsListGap: 0,
                scrollIntervalId: null
            }
        },

        methods: {
            ...mapGetters(['getProductsData']),
            ...mapActions(['fetchProducts']),
            ...mapMutations(['setPreloaderVisibility']),

            scrollProducts(this: any, direction: string) {
                if (direction === 'right') {
                    this.currentOffset !== -this.countTotalSrollWidth ? this.currentOffset -= this.countScrollOffset : this.currentOffset = 0;
                } else if (direction === 'left') {
                    this.currentOffset !== 0 ? this.currentOffset += this.countScrollOffset : this.currentOffset = -this.countTotalSrollWidth;
                }

                this.$refs.popularProductsList.style.transform = 'translateX(' + this.currentOffset + 'px)';
            },

            setScrollInterval(this: any) {
                this.scrollIntervalId = setInterval(() => this.scrollProducts('right'), 5000);
            },

            removeScrollInterval(this: any) {
                clearInterval(this.scrollIntervalId);
            },
        },

        computed: {
            getProductWidth(this: any) {
                if (this.productWidth === 0) {
                    this.productWidth = parseInt(window.getComputedStyle(this.$refs.popularProductCard).getPropertyValue('width'));
                }
                return this.productWidth;
            },

            getProductsListGap(this: any) {
                if (this.productsListGap === 0) {
                    this.productsListGap = parseInt(window.getComputedStyle(this.$refs.popularProductsList).getPropertyValue('gap'));
                }
                return this.productsListGap;
            },

            countScrollOffset(this: any) {
                return (this.getProductWidth * this.viewportProductAmount) + (this.getProductsListGap * this.viewportProductAmount);
            },

            countTotalSrollWidth(this: any) {
                return (this.getProductWidth * this.popularProducts.length) + (this.getProductsListGap * this.viewportProductAmount);
            }
        },

        async beforeMount() {
            this.setPreloaderVisibility(true);
            await this.fetchProducts().then(this.setPreloaderVisibility(false));
            this.popularProducts = this.getProductsData().products;
        },

        mounted() {
            this.setScrollInterval();

            this.$router.beforeEach((to: RouteLocationNormalized, from: RouteLocationNormalized, next: NavigationGuardNext) => {
                clearInterval(this.scrollIntervalId);
                next();
            })
        }
    })
</script>