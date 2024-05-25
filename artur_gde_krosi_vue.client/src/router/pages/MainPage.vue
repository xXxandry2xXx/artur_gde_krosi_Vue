<template>
    <div class="main-page-content">
        <ContentPreview />
        <Brands />
        <ProductsSlider v-if="popularProducts.length > 0" :sliderArray="popularProducts" :sliderTitle="'ПОПУЛЯРНОЕ'" />
    </div>
</template>

<script lang="ts">
    import ContentPreview from '@/components/MainPage/ContentPreview.vue';
    import Brands from '@/components/MainPage/Brands.vue';
    import ProductsSlider from '@/components/ProductsSlider.vue';
    import { mapGetters, mapActions, mapMutations } from 'vuex';
    import { defineComponent } from 'vue';

    export default defineComponent({
        components: { ContentPreview, Brands, ProductsSlider },

        data() {
            return {
                popularProducts: [],
            }
        },

        methods: {
            ...mapGetters(['getProductsData']),
            ...mapActions(['fetchProducts', 'getFilteredData']),
            ...mapMutations(['setPreloaderVisibility']),

            async getPopularProducts(this: any) {
                this.setPreloaderVisibility(true);
                await this.getFilteredData({ sortOrder: 5 }).then((fetchedPopularProducts: any) => this.popularProducts = fetchedPopularProducts.productList);
                this.setPreloaderVisibility(false);
            }
        },

        async beforeMount() {
            this.getPopularProducts();
        }
    })
</script>
