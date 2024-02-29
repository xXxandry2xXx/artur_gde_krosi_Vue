<template>

    <div class="top-bar">
        <h2 class="category-name">Кроссовки</h2>
    </div>
    <div class="products-main">
        <FiltersPanel/>
        <div class="products-content">
            <SearchAndSort :sortingOptions="$store.state.sortingOptions" />
            <ProductList v-if="$store.state.productsData.products" :products="$store.state.productsData.products" />
        </div>
    </div>
    <footer>
        <div class="footer-content">
            КАКАЯ-НИБУДЬ ИНФА И КОНТАКТЫ В ФУТЕРЕ
        </div>
    </footer>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import ProductList from '@/components/ProductList.vue';
    import FiltersPanel from '@/components/FiltersPanel.vue';
    import SearchAndSort from '@/components/SearchAndSort.vue';

    export default defineComponent({
        components: { ProductList, FiltersPanel, SearchAndSort },

        async mounted() {           
            this.$store.dispatch('loadAppliedFilters');    
            this.$store.dispatch('fetchPrices');
            this.$store.dispatch('fetchBrands');
            this.$store.dispatch('fetchSizes');
        }
    })
</script>
