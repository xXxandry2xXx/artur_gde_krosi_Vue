<template>
    <article class="product">
        <img class="product-preview-image" :src="`http://localhost:5263/ImageProduct?id=${product.productId}`" alt="preview" />
        <div class="product-info">
            <h3 class="product-info-brand">{{ product.brend_Name }}</h3>
            <span class="product-info-name">{{ product.name }}</span>
            <span class="product-info-price">{{ product.variants[0].prise / 100 }} </span>
        </div>
    </article>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import type ProductInterface from '@/types/productInterface';

    export default defineComponent({
        props: {
            product: {
                type: Object as () => ProductInterface,
                require: true
            }
        },

        mounted() {
            //this.log()
            //this.fetchImage()
        }
    })
</script>

<style scoped>
    .product {
        display: flex;
        flex-direction: column;
        align-items: center;
        border-radius: 7px;
        box-shadow: 4px 4px 7px 1px rgb(0, 0, 0, 20%);
        background: #fff;
    }

    .product-preview-image {
        width: 100%;
        object-fit: cover;
        border-top-left-radius: 7px;
        border-top-right-radius: 7px;
    }

    .product-info {
        display: flex;
        flex-direction: column;
        padding: 10px 20px;
    }

    .product-info h3 {
        text-align: center;
        font-size: 25px;
        font-weight: 800;
    }

    .product-info span {
        text-align: center;
    }

    .product-info-price {
        font-size: 30px;
        font-weight: 400;
        color: #d2a805;
    }

</style>