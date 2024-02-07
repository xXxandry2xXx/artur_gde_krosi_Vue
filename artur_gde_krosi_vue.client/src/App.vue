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

    export default defineComponent({
        components: { AppHeader, ProductList, FiltersPanel },
        data() {
            return {
                filters: {
                    brands: { id: 0, name: 'Бренды', items: ['Adidas', 'Puma', 'Nike', 'New Balance', 'Bape'] },
                    models: { id: 1, name: 'Модели', items: ['Air Max (Аир Максы)', 'Yeezy (Изики)', 'Air Jordan(Джорданы)', 'Blazer (Блейзеры)', 'Air Force 1 (Форсы)'] },
                    sizes: { id: 2, name: 'Размеры', items: ['27', '33', '39', '41', '43', '33', '39', '41', '43', '33', '39', '41', '43', '33', '39', '41', '43'] }
                },

                products: [
                    {
                        id: 0,
                        brand: 'Nike',
                        name: 'Nike Dunk Low WMNS "Purple Pulse"',
                        previewSrc: '/src/assets/img/productPreviews/nike_preview.jpg',
                        price: 5999
                    },
                    {
                        id: 1,
                        brand: 'Nike',
                        name: 'Nike Dunk Low WMNS "Purple Pulse"',
                        previewSrc: '/src/assets/img/productPreviews/nike_preview.jpg',
                        price: 5999
                    },
                    {
                        id: 2,
                        brand: 'Nike',
                        name: 'Nike Dunk Low WMNS "Purple Pulse"',
                        previewSrc: '/src/assets/img/productPreviews/nike_preview.jpg',
                        price: 5999
                    },
                    {
                        id: 3,
                        brand: 'Nike',
                        name: 'Nike Dunk Low WMNS "Purple Pulse"',
                        previewSrc: '/src/assets/img/productPreviews/nike_preview.jpg',
                        price: 5999
                    },
                    {
                        id: 4,
                        brand: 'Nike',
                        name: 'Nike Dunk Low WMNS "Purple Pulse"',
                        previewSrc: '/src/assets/img/productPreviews/nike_preview.jpg',
                        price: 5999
                    },
                    {
                        id: 5,
                        brand: 'Nike',
                        name: 'Nike Dunk Low WMNS "Purple Pulse"',
                        previewSrc: '/src/assets/img/productPreviews/nike_preview.jpg',
                        price: 5999
                    },
                    {
                        id: 6,
                        brand: 'Nike',
                        name: 'Nike Dunk Low WMNS "Purple Pulse"',
                        previewSrc: '/src/assets/img/productPreviews/nike_preview.jpg',
                        price: 5999
                    },

                ],
            }
        },

        methods: {
            fetchProducts() {
                axios
                    .get('http://localhost:5263/Produts')
                    .then(response => {
                        console.log(response.data);
                    })
                    .catch(error => {
                        console.log(error);
                    })
            }
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
