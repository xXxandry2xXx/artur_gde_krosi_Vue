<template>
    <div class="products-slider">
        <h1 v-if="sliderTitle">{{ sliderTitle }}</h1>
        <div class="products-slider-list-viewport" @touchstart="removeScrollInterval" @touchend="setScrollInterval" v-touch:swipe.left="swipeScrollLeft" v-touch:swipe.right="swipeScrollRight">
            <div class="products-slider-list" ref="productsSlider" @mouseenter="removeScrollInterval" @mouseleave="setScrollInterval">
                <div class="product" style="display: none" ref="sliderCard"></div>
                <Product v-for="(product, index) in slidedProducts" :product="product" />
            </div>
        </div>
        <div class="products-slider-scroll-buttons" v-if="sliderArray.length > viewportProductAmount">
            <div class="products-slider-scroll-button" @click="scrollProducts('left')" @mouseenter="removeScrollInterval" @mouseleave="setScrollInterval">
                <font-awesome-icon :icon="['fas', 'chevron-left']" />
            </div>
            <div class="products-slider-scroll-button" @click="scrollProducts('right')" @mouseenter="removeScrollInterval" @mouseleave="setScrollInterval">
                <font-awesome-icon :icon="['fas', 'chevron-right']" />
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import type { NavigationGuardNext, RouteLocationNormalized } from 'vue-router';
    import Product from '@/components/ProductsPage/Product.vue';

    export default defineComponent({

        components: { Product },

        props: {
            sliderArray: {
                type: Array,
                required: true
            },
            sliderTitle: {
                type: String,
                required: false
            },
            definedViewportProductAmount: {
                type: Number,
                required: false
            },
        },

        data() {
            return {
                slidedProducts: [],
                currentOffset: 0,
                productWidth: 0,
                productsListGap: 0,
                scrollIntervalId: null
            }
        },

        methods: {
            scrollProducts(this: any, direction: string) {
                if (direction === 'right') {
                    if(this.currentOffset >= -this.countTotalSrollWidth && (this.currentOffset - this.countScrollOffset) >= -this.countTotalSrollWidth) {
                        this.currentOffset -= this.countScrollOffset
                    } else {
                        this.currentOffset = 0;
                    }
                } else if (direction === 'left') {
                    if(this.currentOffset <= 0 && (this.currentOffset + this.countScrollOffset) <= 0) {
                        this.currentOffset += this.countScrollOffset
                    } else {
                        if(this.viewportProductAmount > 1) {
                            this.currentOffset = -(this.countTotalSrollWidth);
                        } else {
                            this.currentOffset = -(this.countTotalSrollWidth + this.productsListGap * 2);
                        }
                    }
                }

                if (this.$refs.productsSlider) {
                    if (this.sliderArray.length > this.viewportProductAmount) this.$refs.productsSlider.style.transform = 'translateX(' + this.currentOffset + 'px)';
                }
            },

            setScrollInterval(this: any) {
                this.scrollIntervalId = setInterval(() => this.scrollProducts('right'), 5000);
            },

            removeScrollInterval(this: any) {
                clearInterval(this.scrollIntervalId);
            },

            swipeScrollLeft(this: any) {
                this.scrollProducts('right');
            },

            swipeScrollRight(this: any) {
                this.scrollProducts('left');
            },
        },

        computed: {
            getProductWidth(this: any) {
                if (this.productWidth === 0) {
                    this.productWidth = parseInt(window.getComputedStyle(this.$refs.sliderCard).getPropertyValue('width'));
                }
                return this.productWidth;
            },

            getProductsListGap(this: any) {
                if (this.productsListGap === 0) {
                    this.productsListGap = parseInt(window.getComputedStyle(this.$refs.productsSlider).getPropertyValue('gap'));
                }
                return this.productsListGap;
            },

            countScrollOffset(this: any) {
                return (this.getProductWidth * this.viewportProductAmount) + (this.getProductsListGap * this.viewportProductAmount);
            },

            countTotalSrollWidth(this: any) {
                if (this.slidedProducts === undefined) return 0;

                let totalWidth = (this.getProductWidth * this.slidedProducts.length) + (this.getProductsListGap * this.viewportProductAmount);
                if (this.slidedProducts.length % this.viewportProductAmount < 2) totalWidth -= this.getProductWidth;
                return totalWidth;
            },

            viewportProductAmount(this: any) {
                if (!this.definedViewportProductAmount) {
                    if (window.innerWidth <= 425) {
                        return 1
                    } else if (window.innerWidth <= 768) {
                        return 2
                    } else {
                        return 3
                    };
                } else {
                    return this.definedViewportProductAmount;
                }
            },
        },

        mounted() {
            this.slidedProducts = this.sliderArray;
            this.setScrollInterval();

            this.$router.beforeEach((to: RouteLocationNormalized, from: RouteLocationNormalized, next: NavigationGuardNext) => {
                clearInterval(this.scrollIntervalId);
                next();
            })
        }
    })
</script>