<template>  
    <AppHeader/>

    <div class="top-bar">
        <h2 class="category-name">Кроссовки</h2>
    </div>

    <main class="main-content">
        <FiltersPanel :filterBrands="brands" :filterSizes="sizes" />
        <div class="products-content">
            <SearchAndSort :sortingOptions="sortingOptions" />
            <ProductList v-if="productsData.products" :products="productsData.products" />
        </div>
    </main>

</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import axios from 'axios';
    import AppHeader from './components/AppHeader.vue';
    import ProductList from './components/ProductList.vue';
    import FiltersPanel from './components/FiltersPanel.vue';
    import SearchAndSort from './components/SearchAndSort.vue'
    import type ProductsDataInterface from '@/types/productsDataInterface';
    import type BrandsInterface from './types/brandsInterface';
    import type SizesInterface from '@/types/sizesInterface'

    export default defineComponent({
        components: { AppHeader, ProductList, FiltersPanel, SearchAndSort },
        data() {
            return {
                sortingOptions: ['По алфавиту (А-Я)', 'По алфавиту (Я-А)', 'По цене (Дороже)', 'По цене (Дешевле)'],
                productsData: {} as ProductsDataInterface,
                brands: {} as BrandsInterface,
                sizes: {} as SizesInterface,
                filterOptions: {
                    priceMin: Number,
                    priceMax: Number,
                    brandIDs: [],
                    productModels: [],
                    checkedSizes: [],
                    inStock: Boolean,
                    searchValue: String,
                    sortOrder: Number
                }
            }
        },

        methods: {
            openFiltersPanel(this: { filtersPanelVisible: boolean}) {
                this.filtersPanelVisible = !this.filtersPanelVisible;
            },

            fetchProducts(this: { productsData: ProductsDataInterface }) {
                axios
                    .get('http://localhost:5263/Produts/')
                    .then(response => {
                        this.productsData = response.data;

                    })
                    .catch(error => {
                        console.log(error);
                    })
            },

            fetchBrands(this: { brands: BrandsInterface }) {
                axios
                    .get('http://localhost:5263/Brends')
                    .then(response => {
                        this.brands = response.data;
                    })
                    .catch(error => {
                        console.log(error);
                    })
            },

            fetchSizes(this: { sizes: SizesInterface }) {
                axios
                    .get('http://localhost:5263/ShoeSizes')
                    .then(response => {
                        this.sizes = response.data;
                    })
                    .catch(error => {
                        console.log(error);
                    })
            }
        },

        mounted() {
            this.fetchProducts();
            this.foo();
        }
    })
</script>

<style scoped>
    .fade-enter-active,
    .fade-leave-active {
        transition: opacity 0.3s ease;
    }

    .fade-enter-from,
    .fade-leave-to {
        opacity: 0;
    }

    .fade-enter-to,
    .fade-leave-from {
        opacity: 0.5;
    }

    .slide-enter-active,
    .slide-leave-active {
        transition: opacity 0.3s ease, transform 0.3s ease;
    }

    .slide-enter-from,
    .slide-leave-to {
        transform: translateX(100%);
    }

    .slide-enter-to,
    .slide-leave-from {
        transform: translateX(0);
    }
</style>