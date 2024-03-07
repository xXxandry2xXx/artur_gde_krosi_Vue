<template>
    <div class="top-bar">
        <span class="bread-crumb" @click="$router.push('/')">На главную</span>
        <div class="bread-crumbs">
            <span class="bread-crumb" @click="$router.push('/')">Главная</span>
            /
            <router-link :to="{ name: 'productsPage', params: { page: 1 } }" class="bread-crumb">
                <span>Все кроссовки</span>
            </router-link>
        </div>
    </div>
    <div class="products-main">
        <FiltersPanel />
        <div class="products-content" ref="productsContent">
            <SearchAndSort :sortingOptions="$store.state.sortingOptions" />
            <ProductList v-if="$store.state.filteredProductsData.products" :products="$store.state.filteredProductsData.products" />
        </div>
    </div>
    <PaginationPages @click="$refs.productsContent.scrollIntoView({ behavior: 'smooth' })" v-show="$store.state.totalPages > 0" />
    <footer>
        <div class="footer-content">
            КАКАЯ-НИБУДЬ ИНФА И КОНТАКТЫ В ФУТЕРЕ (пока что это просто заглушка)
        </div>
    </footer>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import PaginationPages from '@/components/ui/PaginationPages.vue';
    import ProductList from '@/components/ProductList.vue';
    import FiltersPanel from '@/components/FiltersPanel.vue';
    import SearchAndSort from '@/components/SearchAndSort.vue';

    export default defineComponent({
        components: { ProductList, FiltersPanel, SearchAndSort, PaginationPages },

        async mounted() {
            this.$store.dispatch('fetchProducts');
            this.$store.dispatch('fetchBrands');
            this.$store.dispatch('fetchSizes');
            this.$store.dispatch('changePage', Number(this.$route.params.page));
        }
    })
</script>
