<template>
    <article class="product" @mouseenter="toggleSizesPanel" @mouseleave="toggleSizesPanel">
        <img class="product-preview-image" :src="product.images[0].imgSrc" alt="preview" />
        <div class="product-info">
            <transition name="fade-main">
                <div class="product-info-main" v-show="!showSizes">
                    <h3 class="product-info-brand">{{ product.brend_Name }}</h3>
                    <span class="product-info-name">{{ product.name }}</span>
                    <span class="product-info-price">{{ product.variants[0].prise / 100 }} </span>
                </div>
            </transition>

            <transition name="fade-sizes">
                <div class="product-info-sizes" v-show="showSizes">
                    <span class="product-info-sizes-title">Размеры</span>
                    <div class="product-info-sizes-list">
                        <span class="product-size" v-for="size in availableProductSizes">{{ size }}</span>
                    </div>
                </div>
            </transition>
        </div>
    </article>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import type ProductInterface from '@/types/productInterface';

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
                let gatheredProductSizes = this.product.variants.map((variant: any) => variant.shoeSize);
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