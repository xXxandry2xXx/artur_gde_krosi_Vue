<template>
    <div class="product-list-wrapper">
        <div v-if="products.length > 0" class="product-list">
            <Product v-for="product in products" :key="product.productId" :product="product" @click="$router.push(`/products/productId=${product.productId}`)"/>
        </div>
        <div v-else class="nothing-found">
            <div class="nothing-found-content">
                <span><i class="far fa-times-circle"></i></span>
                <p>По вашему запросу ничего не найдено. Попробуйте сбросить фильтры или ввести другой поисковой запрос.</p>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import Product from './Product.vue';
    import type ProductInterface from '@/types/productInterface';

    export default defineComponent({
        components: { Product },
        props: {
            products: {
                type: Array as () => ProductInterface[],
                required: true
            }
        }
    })

</script>
