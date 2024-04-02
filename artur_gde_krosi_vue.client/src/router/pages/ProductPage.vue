<template>
    <div class="product-page" :key="changedProductId">
        <nav class="top-bar">
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
        </nav>

        <ProductMainInfo :productData="productData" :productImages="productImages"/>
        <ProductAdditionalInfo />
        <ProductsSlider v-if="relatedProducts.length > 0" :sliderArray="relatedProducts" :sliderTitle="'ЕЩЕ ИЗ  ЭТОЙ КАТЕГОРИИ'" />

    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapMutations, mapActions } from 'vuex';
    import type { NavigationGuardNext, RouteLocationNormalized } from 'vue-router';
    import ProductsSlider from '@/components/ProductsSlider.vue';
    import ProductMainInfo from '@/components/ProductPage/ProductMainInfo.vue';
    import ProductAdditionalInfo from '@/components/ProductPage/ProductAdditionalInfo.vue';
    import axios from 'axios';

    export default defineComponent({
        components: { ProductsSlider, ProductAdditionalInfo, ProductMainInfo },

        data() {
            return {
                productId: this.changedProductId,
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
                    const response = await axios.get('http://localhost:5263/api/Product/GetProduct', { params: { 'ProductId': this.productId }, headers: { 'accept': '*/*' } });
                    this.productData = response.data[0];
                    this.productImages = this.productData.images.reverse()
                    this.currentImgIndex = this.productImages[0].index;
                } catch (error) {
                    console.log(error)
                }
            },

            async getRelatedProducts(this: any) {
                const sameCategoryFilters = { modelIDs: [this.productData.modelKrosovock.modelKrosovockId] }
                await this.getFilteredData(sameCategoryFilters).then((response: any) => {
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
            relatedProducts(this: any) {
                return this.sameCategoryProducts.filter((product: any) => product.productId !== this.productId)
            },

            changedProductId(this: any) {
                return this.productId = this.$route.params.productId;
            }
        },

        watch: {
            async productId(this: any) {
                await this.productPageInit();
                this.getRelatedProducts();
            }
        },

        async mounted() {
            await this.productPageInit();
            this.getRelatedProducts()
        },
    })
</script>