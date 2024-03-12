<template>

    <div class="top-bar">
        <span class="bread-crumb" @click="$router.push('/')">На главную</span>
        <div class="bread-crumbs">
            <span class="bread-crumb" @click="$router.push('/')">Главная</span>
            /
            <span class="bread-crumb bread-crumb-current">Все кроссовки</span>
        </div>
    </div>

    <div class="products-main" ref="productsContent">
        <FiltersPanel />
        <div class="products-content">
            <SearchAndSort :sortingOptions="$store.state.productsCatalog.sortingOptions" />
            <ProductList v-if="$store.state.productsCatalog.filteredProductsData.products" :products="$store.state.productsCatalog.filteredProductsData.products" />
        </div>
    </div>

    <PaginationPages @click="$refs.productsContent.scrollIntoView({ behavior: 'smooth' })" v-show="$store.state.productsCatalog.totalPages > 0" />
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapActions } from 'vuex';
    import PaginationPages from '@/components/ProductsPage/PaginationPages.vue';
    import ProductList from '@/components/ProductsPage/ProductList.vue';
    import FiltersPanel from '@/components/ProductsPage/FiltersPanel.vue';
    import SearchAndSort from '@/components/ProductsPage/SearchAndSort.vue';

    export default defineComponent({
        components: { ProductList, FiltersPanel, SearchAndSort, PaginationPages },

        methods: {
        ...mapActions(['fetchProducts', 'fetchBrands', 'fetchSizes', 'changePage']),
        },

        async mounted() {
            this.fetchProducts();
            this.fetchBrands();
            this.fetchSizes();
            this.changePage(Number(this.$route.params.page));
        }
    })
</script>
