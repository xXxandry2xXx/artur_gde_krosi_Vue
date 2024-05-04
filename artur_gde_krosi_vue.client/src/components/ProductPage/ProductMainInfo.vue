<template>
    <div class="product-page-main-content">
        <div class="product-main">
            <ProductsImageSlider v-if="productImages.length > 0" :images="productImages" />
            <div class="product-page-info">
                <div class="product-page-info-details">
                    <h1 class="product-name" @click=" console.log(quantityInStock)">{{ productData.name }}</h1>
                    <p class="product-page-detail">Бренд: <span class="product-page-detail-value">{{ productData.modelKrosovock && productData.modelKrosovock.brend && productData.modelKrosovock.brend.name }}</span></p>
                    <p class="product-page-detail">Модель: <span class="product-page-detail-value">{{ productData.modelKrosovock && productData.modelKrosovock.name }}</span></p>
                    <p class="product-page-detail">Наличие: <span class="product-page-detail-value is-in-stock">{{ productData.variants && isInStock }}</span></p>
                    <div class="product-page-sizes" v-if="productData.variants && quantityInStock.length > 0">
                        <p class="product-page-detail">Размеры: </p>
                        <div class="product-page-sizes-list">
                            <ProductPickSizeButton v-for="variant in quantityInStock"
                                                   :productId="productData.productId"
                                                   :currentVariant="variant"
                                                   :class="{'product-page-size-box-picked': variant.variantId === getChosenVariant()}" />
                        </div>
                    </div>
                </div>

                <div class="product-price-and-cart">
                    <p class="product-page-price">
                        {{ productData.variants && productData.variants[0] && productData.variants[0].prise/100 }}
                        <span> руб.</span>
                    </p>
                    <BorderedButton class="add-to-cart-button" @click="addToCart">
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
    import { mapActions, mapGetters } from 'vuex';
    import ProductPickSizeButton from '@/components/ProductPage/ProductPickSizeButton.vue';
    import ProductsImageSlider from '@/components/ProductPage/ProductImageSlider.vue';

    export default defineComponent({
        components: { ProductsImageSlider, ProductPickSizeButton },

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

        methods: {
            ...mapActions(['addItemToCart']),
            ...mapGetters(['getChosenVariant']),

            async addToCart(this: any) {
                const currentSelectedVariantID = this.getChosenVariant();
                if (currentSelectedVariantID !== '') {
                    this.addItemToCart(currentSelectedVariantID);
                } else {
                    console.log('Товар не выбран');
                }
            }
        },

        computed: {
            quantityInStock(this: any) {
                if (this.productData.variants) {
                    return this.productData.variants.reduce((total: Array<any>, variant: any) => {
                        if (variant.quantityInStock > 0) {
                            total.push(variant);
                        }
                        return total;
                    }, []);
                }
            },

            isInStock(this: any) {
                if (this.quantityInStock.length > 0) {
                    return 'В наличии (' + this.quantityInStock.length + ' ' + 'шт.)'
                } else {
                    return 'Нет в наличии'
                }
            },
        },
    })
</script>