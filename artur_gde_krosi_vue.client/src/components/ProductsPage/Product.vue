<template>
    <article class="product" @mouseenter="toggleSizesPanel" @mouseleave="toggleSizesPanel" @click="$router.push(`/products/productId=${product.productId}`)">
        <img class="product-preview-image" :src="product.imgSrc" alt="preview" />
        <div class="product-info">
                <div class="product-info-main">
                    <div class="product-info-categories">
                        <span class="product-info-category">{{ product.brend_Name }}, </span>
                        <span class="product-info-category">{{ product.modelKrosovock }}</span>
                    </div>
                    <span class="product-info-name">{{ product.name }}</span>
                    <span class="product-info-price">{{ product.variants[0].prise / 100 }} </span>
                </div>
            <transition name="fade-sizes">
                <div class="product-info-sizes" v-show="showSizes">
                    <span class="product-size" v-for="size in availableProductSizes">{{ size }}</span>
                </div>
            </transition>
        </div>
    </article>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import type { ProductInterface } from '@/store/modules/productsCatalog/types';

    export default defineComponent({
        data() {
            return {
                showSizes: false,
            }
        },

        props: {
            product: {
                type: Object as () => ProductInterface,
                require: true
            }
        },

        methods: {
            toggleSizesPanel(this: any) {
                this.showSizes = !this.showSizes;
            }
        },

        computed: {
            availableProductSizes(this: any) {
                let gatheredProductSizes = this.product.variants.map((variant: any) => {
                    if (variant.quantityInStock > 0) return variant.shoeSize
                });
                gatheredProductSizes = gatheredProductSizes.sort((a: number, b: number) => a - b);
                return gatheredProductSizes;
            }
        }
    })
</script>

<style scoped>
    .fade-main-enter-active,
    .fade-main-leave-active {
        transition: opacity 0.2s ease;
    }

    .fade-main-enter-from,
    .fade-main-leave-to {
        opacity: 0;
    }

    .fade-sizes-enter-active,
    .fade-sizes-leave-active {
        transition: opacity 0.2s ease;
    }

    .fade-sizes-enter-from,
    .fade-sizes-leave-to {
        opacity: 0;
    }
</style>