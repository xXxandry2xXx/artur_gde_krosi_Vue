<template>
    <nav class="top-bar">
        <span class="bread-crumb" @click="$router.push('/')">На главную</span>
        <div class="bread-crumbs">
            <span class="bread-crumb" @click="$router.push('/')">Главная</span>
            /
            <span class="bread-crumb bread-crumb-current">Все кроссовки</span>
        </div>
    </nav>

    <div class="products-main">
        <FiltersPanel />
        <div class="products-content">
            <SearchAndSort :sortingOptions="$store.state.productsCatalog.sortingOptions" />
            <ProductList v-if="$store.state.productsCatalog.filteredProductsData.productList" :products="$store.state.productsCatalog.filteredProductsData.productList" />
        </div>
    </div>

    <PaginationPages v-show="getTotalPages() > 0" />
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapActions, mapGetters, mapMutations } from 'vuex';
    import type { NavigationGuardNext, RouteLocationNormalized } from 'vue-router';
    import PaginationPages from '@/components/ProductsPage/PaginationPages.vue';
    import ProductList from '@/components/ProductsPage/ProductList.vue';
    import FiltersPanel from '@/components/ProductsPage/FiltersPanel.vue';
    import SearchAndSort from '@/components/ProductsPage/SearchAndSort.vue';

    export default defineComponent({
        components: { ProductList, FiltersPanel, SearchAndSort, PaginationPages },

        methods: {
            ...mapMutations(['setCurrentPage']),
            ...mapActions(['fetchProducts', 'fetchBrands', 'fetchSizes', 'changePage', 'loadAppliedFilters', 'fetchPrices']),
            ...mapGetters(['getCurrentPage', 'getTotalPages']),

            initProductsPage(this: any) {
                this.fetchProducts();
                this.fetchBrands();
                this.fetchSizes();
                this.changePage(Number(this.$route.params.page));
            }
        },

        async mounted() {
            this.initProductsPage();

            this.$router.beforeEach((to: RouteLocationNormalized, from: RouteLocationNormalized, next: NavigationGuardNext) => {
                if (to.meta.requiresFiltersLoading) {
                    const currentPage = this.getCurrentPage();
                    const totalPages = this.getTotalPages();
                    if (to.params.page) {
                        this.setCurrentPage(Number(to.params.page));
                        this.loadAppliedFilters();
                    } else {
                        to.params.page = '1';
                        this.setCurrentPage(Number(to.params.page));
                        this.loadAppliedFilters();
                    }
                    if (totalPages > 0 && to.params.page !== undefined) {
                        if (currentPage < 1 || currentPage > totalPages || Number(to.params.page) < 1 || Number(to.params.page) > totalPages) this.$router.push('/notfound');
                    }
                }
                next();
            });
        }
    })
</script>
