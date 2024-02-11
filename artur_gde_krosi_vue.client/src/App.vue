<template>  
    <div class="filters-container">
        <transition name="slide">
            <FiltersPanel v-show="filtersPanelVisible" :filterBrands="brands" :filterSizes="sizes" />
        </transition>
        <transition name="fade">
            <div class="filters-background" v-show="filtersPanelVisible" @click="openFiltersPanel"></div>
        </transition>
    </div>

    <AppHeader/>

    <div class="top-bar">
        <h2 class="category-name">Кроссовки</h2>
        <div class="sorting-and-filters">
            <SearchAndSort :sortingOptions="sortingOptions" />
            <button class="show-filters-button" @click="openFiltersPanel"><i class="fas fa-filter"></i> ФИЛЬТРЫ</button>
        </div>
    </div>

    <div class="content">
        <main>
            <ProductList v-if="productsData.products" :products="productsData.products"/>
        </main>
    </div>

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
                sortingOptions: ['Алфавиту (А-Я)', 'Алфавиту (Я-А)', 'Цене (Дороже)', 'Цене (Дешевле)'],
                filtersPanelVisible: false,
                productsData: {} as ProductsDataInterface,
                brands: {} as BrandsInterface,
                sizes: {} as SizesInterface
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
            this.fetchBrands();
            this.fetchSizes();
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