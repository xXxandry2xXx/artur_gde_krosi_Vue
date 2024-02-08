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

                products: {},
            }
        },

        methods: {
            fetchProducts() {
                axios
                    .get('http://localhost:5263/Produts/')
                    .then(response => {
                        //this.products = response.data;

                    })
                    .catch(error => {
                        console.log(error);
                    })
            },
            foo() {
                let config = {
                    method: 'get',
                    maxBodyLength: Infinity,
                    url: 'https://api.moysklad.ru/api/remap/1.2/download/29dacef2-93b9-47ed-a357-f1f8ac562d32',
                    headers: {
                        'Authorization': 'Bearer ad4bc311f51bafdc7357e20ece905d282f6fe448'
                    }
                };

                axios.request(config)
                    .then((response) => {
                        console.log(JSON.stringify(response.data));
                    })
                    .catch((error) => {
                        console.log(error);
                    });
            }
        },

        mounted() {
            this.fetchProducts();
            this.foo();
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
