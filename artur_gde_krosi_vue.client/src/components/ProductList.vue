<template>
    <div class="product-list">
        <Product v-for="product in products" :key="product.productId" :product="product"/>
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

<style scoped>
    .product-list {
        display: grid;
        grid-template-columns: 20% 20% 20% 20%;
        gap: 20px;
        justify-content: center;
    }
</style>