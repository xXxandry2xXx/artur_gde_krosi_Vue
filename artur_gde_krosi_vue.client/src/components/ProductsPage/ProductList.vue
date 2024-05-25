<template>
    <div class="product-list-wrapper">
        <Preloader v-show="$store.state.productsCatalog.showCatalogPreloader" />
        <div v-if="getFilteredProductsData().productList.length > 0" class="product-list">
            <Product v-for="product in getFilteredProductsData().productList" :key="product.productId" :product="product"/>
        </div>
        <div v-else class="nothing-found">
            <div class="nothing-found-content">
                <span><font-awesome-icon :icon="['fas', 'face-frown']" /></span>
                <p>У Артура нет таких кроссовок</p>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapGetters } from 'vuex';
    import Product from './Product.vue';
    import type { ProductInterface } from '@/store/modules/productsCatalog/types';

    export default defineComponent({
        components: { Product },

        methods: {
            ...mapGetters(['getFilteredProductsData']),
        }
    })

</script>
