<template>

    <div class="top-bar">
        <h2 class="category-name">Кроссовки</h2>
    </div>
    <div class="products-main">
        <FiltersPanel :filterBrands="$store.state.brands" :filterSizes="$store.state.sizes" />
        <div class="products-content">
            <SearchAndSort :sortingOptions="$store.state.sortingOptions" />
            <ProductList v-if="$store.state.productsData.products" :products="$store.state.productsData.products" />
        </div>
    </div>

</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import ProductList from '@/components/ProductList.vue';
    import FiltersPanel from '@/components/FiltersPanel.vue';
    import SearchAndSort from '@/components/SearchAndSort.vue';

    export default defineComponent({
        components: { ProductList, FiltersPanel, SearchAndSort },

        mounted() {
            let selectedFiltersCache: any = localStorage.getItem('selectedFilters');
            
            this.$store.dispatch('applyFilters');
            
            this.$store.dispatch('fetchBrands');
            this.$store.dispatch('fetchSizes');
        }
    })
</script>
