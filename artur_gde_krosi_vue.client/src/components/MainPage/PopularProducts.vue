<template>
    <div class="main-page-popular-products">
        <h1>ПОПУЛЯРНОЕ</h1>
        <div class="main-page-popular-products-list-wrapper">
            <div class="main-page-popular-products-list">
                <Product v-for="product in popularProducts" :product="product" />
            </div>
        </div>
        <div class="main-page-popular-products-scroll-buttons"></div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapGetters, mapActions } from 'vuex';
    import Product from '@/components/ProductsPage/Product.vue';

    export default defineComponent({

        components: { Product },

        data() {
            return {
                popularProducts: [],
            }
        },

        methods: {
            ...mapGetters(['getProductsData']),
            ...mapActions(['fetchProducts']),
        },

        async beforeMount() {
            await this.fetchProducts();
            this.popularProducts = this.getProductsData().products;
        }
    })
</script>