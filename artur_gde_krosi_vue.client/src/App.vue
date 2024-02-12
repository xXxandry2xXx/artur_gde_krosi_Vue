<template>
    <AppHeader />
    <span class="title">Кроссовки</span>
    <div class="content">
        <main>
            <ProductList :products="products" />
        </main>
        <FiltersPanel :filters="filters" />
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import axios from 'axios';
    import AppHeader from './components/AppHeader.vue';
    import ProductList from './components/ProductList.vue';
    import FiltersPanel from './components/FiltersPanel.vue';
    import type ProductInterface from '@/types/productInterface';

    export default defineComponent({
        components: { AppHeader, ProductList, FiltersPanel },
        data() {
            return {
                products: [] as ProductInterface[],
            }
        },

        methods: {
            fetchProducts() {
                axios
                    .get('http://localhost:5263/Produts/')
                    .then(response => {
                        this.products = response.data as ProductInterface[];

                    })
                    .catch(error => {
                        console.log(error);
                    })
            },
            
        },

        mounted() {
            this.fetchProducts();
        }
    })
</script>

<style scoped>
    .content {
        display: flex;
        flex-direction: row-reverse;
        justify-content: space-around;
        margin-top: 50px;
    }

    .title {
        margin-left: 80px;
        font-size: 50px;
        font-weight: 400;
        color: #947704;
    }
</style>
