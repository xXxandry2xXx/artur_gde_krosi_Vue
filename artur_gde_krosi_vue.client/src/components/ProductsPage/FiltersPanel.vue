<template>
    <aside class="filter-panel">
        <div class="filter-panel-butons">
            <router-link :to="{ name: 'productsPage', params: { page: 1 } }">
                <BorderedButton class="filters-apply-button" @click="applySelectedFilters">Применить</BorderedButton>
            </router-link>
            <router-link :to="{ name: 'productsPage', params: { page: 1 } }">
                <BorderedButton class="filters-apply-button" @click="clearSelectedFilters">Очистить</BorderedButton>
            </router-link>
            <BorderedButton class="filters-apply-button" v-if="isMobile()" @click="setFiltersPanelVisibility(false)">
                <font-awesome-icon :icon="['fas', 'xmark']" />
            </BorderedButton>
        </div>

        <FiltersCurrentlySelected v-if="
                  $store.state.productsCatalog.models.constructor === Array &&
                  $store.state.productsCatalog.brands.constructor === Array &&
                  $store.getters.selectedFiltersCached &&
                  $store.state.productsCatalog.filteredProductsData.productList" />

        <div class="filters-list">
            <div class="filter">
                <div class="filter-opener">
                    <h2 class="filter-title">Цена</h2>
                </div>
                <PriceRanger />
            </div>

            <Filter :filter="$store.state.productsCatalog.brands" :filterName="'Бренды'" />

            <Filter v-show="$store.state.productsCatalog.models.length > 0" :filter="$store.state.productsCatalog.models" :filterName="'Модели'" />

            <Filter class="sizes-filter" :filter="$store.state.productsCatalog.sizes" :filterName="'Размеры'" />
        </div>
    </aside>
</template>

<script lang="ts">
    import { defineComponent } from "vue";
    import { mapMutations, mapGetters, mapActions } from 'vuex';
    import PriceRanger from './PriceRanger.vue'
    import Filter from './Filter.vue';
    import FiltersCurrentlySelected from './FiltersCurrentlySelected.vue'

    export default defineComponent({
        components: { PriceRanger, Filter, FiltersCurrentlySelected },

        methods: {
            ...mapMutations(['setFiltersPanelVisibility']),
            ...mapGetters(['isMobile']),
            ...mapActions(['applyFilters', 'clearFilters']),

            closeFilterPanelIfMobile(this: any) {
                if (this.isMobile) this.setFiltersPanelVisibility(false);
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
 