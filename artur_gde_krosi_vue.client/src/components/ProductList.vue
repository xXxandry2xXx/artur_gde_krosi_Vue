<template>
    <div class="product-list">
        <Product v-for="product in products" :key="product.id" :product="product"/>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import Product from './Product.vue';
import type { ProductInterface } from '../types/productInterface';

    export default defineComponent({
        components: { Product },
        props: {
            products: {
                type: Array,
                required: true
            }
        }
    })

</script>

<style scoped>
    .product-list {
        display: grid;
        grid-template-columns: 270px 270px 270px;
        gap: 20px;
        justify-content: center;
    }
</style>