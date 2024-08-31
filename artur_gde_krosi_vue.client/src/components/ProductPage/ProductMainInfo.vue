<template>
    <div class="product-page-main-content">
        <div class="product-main">
            <ProductsImageSlider v-if="productImages.length > 0" :images="productImages" />
            <div class="product-page-info">
                <div class="product-page-info-details">
                    <h1 class="product-name">{{ productData.name }}</h1>
                    <p class="product-page-detail">Бренд: <span class="product-page-detail-value">{{ productData.modelKrosovock && productData.modelKrosovock.brend && productData.modelKrosovock.brend.name }}</span></p>
                    <p class="product-page-detail">Модель: <span class="product-page-detail-value">{{ productData.modelKrosovock && productData.modelKrosovock.name }}</span></p>
                    <p class="product-page-detail">Наличие: <span class="product-page-detail-value is-in-stock">{{ productData.variants && isInStockText }}</span></p>
                    <div class="product-page-sizes" v-if="productData.variants && variantsInStock.length > 0">
                        <p class="product-page-detail">Размеры: </p>
                        <div class="product-page-sizes-list">
                            <ProductPickSizeButton v-for="variant in variantsInStock"
                                                   :currentVariant="variant"
                                                   :class="{'product-page-size-box-picked': variant.variantId === getChosenVariant()}" />
                        </div>
                    </div>
                </div>

                <div class="product-price-and-cart">
                    <CartButton v-if="isInStock" :class="{'cart-button-default-unavailable': !isInStock}" @click="addToCart">
                        {{ productData.variants && productData.variants[0] && productData.variants[0].prise/100 }}
                    </CartButton>
                    <BorderedButton class="product-page-price-unavailable bordered-button-default-unavailable" v-else>Нет в наличии</BorderedButton>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapActions, mapGetters, mapMutations } from 'vuex';
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
            ...mapMutations(['setCurrentChosenVariantId', 'setCurrentChosenProductId']),

            async addToCart(this: any) {
                const currentSelectedVariantID = this.getChosenVariant();
                if (currentSelectedVariantID !== '') {
                    this.addItemToCart(currentSelectedVariantID);
                } else {
                    console.log('Товар не выбран');
                }
            }
        },

        watch: {
            productData(this: any) {
                if (this.variantsInStock.length > 0) {
                    this.setCurrentChosenVariantId(this.variantsInStock[0].variantId);
                    this.setCurrentChosenProductId(this.productData.productId);
                } else {
                    this.setCurrentChosenVariantId(null);
                    this.setCurrentChosenProductId(null);
                }
            }
        },

        computed: {
            variantsInStock(this: any) {
                if (this.productData.variants) {
                    return this.productData.variants.reduce((total: Array<any>, variant: any) => {
                        if (variant.quantityInStock > 0) {
                            total.push(variant);
                        }
                        return total;
                    }, []);
                }
            },

            isInStockText(this: any) {
                if (this.variantsInStock.length > 0) {
                    if (this.variantsInStock.length > 10) {
                        return 'В наличии';
                    }
                    return 'В наличии (' + this.variantsInStock.length + ' ' + 'шт.)';
                } else {
                    return 'Нет в наличии'
                }
            },

            isInStock(this: any) {
                if (this.variantsInStock && this.variantsInStock.length > 0) {
                    return true;
                } else {
                    return false;
                }
            }
        },
    })
</script>