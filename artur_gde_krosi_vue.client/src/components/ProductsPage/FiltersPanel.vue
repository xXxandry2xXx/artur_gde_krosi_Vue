<template>
    <aside class="filter-panel">
        <div class="filter-panel-buttons">
            <div class="filter-panel-interact-buttons">
                <router-link :to="{ name: 'productsPage', params: { page: 1 } }">
                    <BorderedButton class="filters-apply-button" @click="applySelectedFilters">Применить</BorderedButton>
                </router-link>
                <router-link :to="{ name: 'productsPage', params: { page: 1 } }">
                    <BorderedButton class="filters-apply-button" @click="clearSelectedFilters">Очистить</BorderedButton>
                </router-link>
            </div>
            <BorderedButton class="filters-close-button" style="border-radius: 0; border-right: none; " v-if="isMobile() || this.isTablet()" @click="setFiltersPanelVisibility(false)">
                <font-awesome-icon :icon="['fas', 'xmark']" />
            </BorderedButton>
        </div>

        <FiltersCurrentlySelected v-if="
                  $store.state.productsCatalog.models.constructor === Array &&
                  $store.state.productsCatalog.brands.constructor === Array &&
                  $store.getters.selectedFiltersCached &&
                  $store.state.productsCatalog.filteredProductsData.productList" />

        <div class="filters-list">
            <div class="filter" style="border-top: none; margin-top: 0;">
                <div class="filter-opener">
                    <h2 class="filter-title">Цена</h2>
                </div>
                <PriceRanger />
            </div>

            <Filter :filter="$store.state.productsCatalog.brands" :filterName="'Бренды'" />

            <Filter :filterName="'Модели'">
                <div class="models-of-brands-list">
                    <FilterModelsOfBrandSection v-for="brand in $store.state.productsCatalog.brands" :brand="brand" />
                </div>
            </Filter>

            <Filter class="sizes-filter" :filter="$store.state.productsCatalog.sizes" :filterName="'Размеры'" />
        </div>
    </aside>
</template>

<script lang="ts">
    import { defineComponent } from "vue";
    import { mapMutations, mapGetters, mapActions } from 'vuex';
    import PriceRanger from './PriceRanger.vue'
    import Filter from './Filter.vue';
    import FiltersCurrentlySelected from './FiltersCurrentlySelected.vue';
    import FilterModelsOfBrandSection from '@/components/ProductsPage/FilterModelsOfBrandSection.vue';

    export default defineComponent({

        components: { PriceRanger, Filter, FiltersCurrentlySelected, FilterModelsOfBrandSection },

        methods: {
            ...mapMutations(['setFiltersPanelVisibility']),
            ...mapGetters(['isMobile', 'isTablet', 'selectedFiltersState']),
            ...mapActions(['applyFilters', 'clearFilters', 'getBrands']),

            closeFilterPanelIfMobile(this: any) {
                if (this.isMobile() || this.isTablet()) this.setFiltersPanelVisibility(false);
            },

            applySelectedFilters() {
                this.closeFilterPanelIfMobile();
                this.applyFilters();
            },

            clearSelectedFilters() {
                this.closeFilterPanelIfMobile();
                this.clearFilters();
            }
        }
    })
</script>
 