<template>
    <div class="product-page-main-content">
        <div class="product-main">
            <ProductsImageSlider v-if="productImages.length > 0" :images="productImages" />
            <div class="product-page-info">
                <div class="product-page-info-details">
                    <h1 class="product-name">{{ productData.name }}</h1>
                    <p class="product-page-detail">Бренд: <span class="product-page-detail-value">{{ productData.modelKrosovock && productData.modelKrosovock.brend && productData.modelKrosovock.brend.name }}</span></p>
                    <p class="product-page-detail">Модель: <span class="product-page-detail-value">{{ productData.modelKrosovock && productData.modelKrosovock.name }}</span></p>
                    <p class="product-page-detail">Наличие: <span class="product-page-detail-value is-in-stock">{{ productData.variants && isInStock }}</span></p>
                    <div class="product-page-sizes">
                        <p class="product-page-detail">Размеры: </p>
                        <div class="product-page-sizes-list">
                            <span class="product-page-size-box" v-for="size in $store.state.productsCatalog.sizes" :class="{'product-page-size-box-unavailable': !availableSizes.includes(size)}">{{ size }}</span>
                        </div>
                    </div>
                </div>

                <div class="product-price-and-cart">
                    <p class="product-page-price">
                        {{ productData.variants && productData.variants[0] && productData.variants[0].prise/100 }}
                        <span> руб.</span>
                    </p>
                    <BorderedButton class="add-to-cart-button">
                        <font-awesome-icon :icon="['fas', 'cart-shopping']" />
                        В корзину
                    </BorderedButton>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import ProductsImageSlider from '@/components/ProductPage/ProductImageSlider.vue';

    export default defineComponent({
        components: { ProductsImageSlider },

        props: {
            productData: {
                type: Object,
                reqired: true
            },

            productImages: {
                type: Array,
                reqired: true
            }
        },

        computed: {
            quantityInStock(this: any) {
                if (this.productData.variants) return this.productData.variants.reduce((total: number, variant: any) => total + variant.quantityInStock, 0);
            },

            availableSizes(this: any) {
                if (this.productData.variants) {
                    const availableSizes = this.productData.variants
                        .filter((variant: any) => variant.quantityInStock > 0)
                        .map((variant: any) => variant.shoeSize);

                    return availableSizes;
                }
                return [];
            },

            isInStock(this: any) {
                if (this.quantityInStock > 0) {
                    return 'В наличии (' + this.quantityInStock + ' ' + 'шт.)'
                } else {
                    return 'Нет в наличии'
                }
            },
        }
    })
</script>