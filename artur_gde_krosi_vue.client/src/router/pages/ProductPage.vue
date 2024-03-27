<template>
    <div class="product-page" :key="productId">
        <div class="top-bar">
            <span class="bread-crumb" @click="$router.go(-1)">Назад</span>
            <div class="bread-crumbs">
                <span class="bread-crumb" @click="$router.push('/')">Главная</span>
                /
                <router-link :to="{ name: 'productsPage', params: { page: 1 } }" class="bread-crumb">
                    <span>Все кроссовки</span>
                </router-link>
                /
                <span class="bread-crumb bread-crumb-current">{{ productData.name }}</span>
            </div>
        </div>

        <div class="product-page-main-content">
            <div class="product-main">
                <ProductsImageSlider v-if="productImages.length > 0" :images="productImages"/>
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

        <div class="product-page-additional-content">
            <!--ВСЯ ИНФОРМАЦИЯ НИЖЕ - ПРОСТО ПЛЕЙСХОЛДЕР-->
            <div class="product-page-description">
                <h2>Описание</h2>
                <p>
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                    Feugiat nisl pretium fusce id velit ut tortor. Fames ac turpis egestas sed. Malesuada fames ac turpis egestas maecenas pharetra convallis.
                    Nibh tortor id aliquet lectus proin nibh nisl condimentum.
                    Purus in massa tempor nec feugiat nisl pretium. At risus viverra adipiscing at in tellus integer.
                </p>
            </div>
            <div class="product-page-characteristics">
                <h2>Основные характеристики</h2>
                <ul>
                    <li><span>Состав материала:</span><span>замша, кожа, текстиль</span></li>
                    <li><span>Комплектация:</span><span>коробка, кроссовки</span></li>
                    <li><span>Сезон:</span><span>зима, весна-осень</span></li>
                    <li><span>Страна производителя:</span><span>Вьетнам</span></li>
                    <li><span>Цвет:</span><span>Белый</span></li>
                </ul>
            </div>
        </div>
        <ProductsSlider v-if="relatedProducts.length > 0" :sliderArray="relatedProducts" :sliderTitle="'ЕЩЕ ИЗ  ЭТОЙ КАТЕГОРИИ'" :key="productId"/>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapMutations, mapActions } from 'vuex';
    import type { NavigationGuardNext, RouteLocationNormalized } from 'vue-router';
    import ProductsSlider from '@/components/ProductsSlider.vue';
    import ProductsImageSlider from '@/components/ProductPage/ProductImageSlider.vue';
    import axios from 'axios';

    export default defineComponent({
        components: { ProductsSlider, ProductsImageSlider },

        data() {
            return {
                productId: this.$route.params.productId,
                productData: {},
                productImages: [],
                sameCategoryProducts: [],
            }
        },

        methods: {
            ...mapMutations(['setPreloaderVisibility']),
            ...mapActions(['fetchSizes', 'getFilteredData']),

            async fetchProduct(this: any) {
                try {
                    const response = await axios.get('http://localhost:5263/Produt', { params: { 'ProductId': this.productId }, headers: { 'accept': '*/*' } });
                    this.productData = response.data[0];
                    this.productImages = this.productData.images.reverse()
                    this.currentImgIndex = this.productImages[0].index;
                } catch (error) {
                    console.log(error)
                }
            },

            async getRelatedProducts(this: any) {
                const form = new FormData();
                form.append('modelKrosovocksIds', this.productData.modelKrosovock.modelKrosovockId);
                await this.getFilteredData(form).then((response: any) => {
                    this.sameCategoryProducts = response.products;
                });
            },

            async productPageInit(this: any) {
                this.setPreloaderVisibility(true);
                await this.fetchSizes();
                await this.fetchProduct().then(this.setPreloaderVisibility(false));

                this.$router.beforeEach((to: RouteLocationNormalized, from: RouteLocationNormalized, next: NavigationGuardNext) => {
                    if (to.meta.isProductPage) this.productId = to.params.productId;
                    next();
                })
            },
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

            relatedProducts(this: any) {
                return this.sameCategoryProducts.filter((product: any) => product.productId !== this.productId)
            }
        },

        watch: {
            async productId(this: any) {
                await this.productPageInit();
                await this.getRelatedProducts()
            }
        },

        async mounted() {
            await this.productPageInit();
            await this.getRelatedProducts()
        },
    })
</script>