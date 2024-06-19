<template>
    <div class="search-and-sort">
        <div class="sorting-and-filters">
            <SortDropdownMenu class="sort-dropdown" :options="sortingOptions" />
            <BorderedButton class="filters-panel-opener" @click="setFiltersPanelVisibility(true)" v-if="isMobile() || isTablet()">Фильтры</BorderedButton>
        </div>
        <div class="filter-item-container">
            <label class="filter-item">
                <input class="filter-item-checkbox" type="checkbox" @change="toggleStockFilter" :checked="isChecked">
                <span class="filter-item-checkbox-fake"></span>
                <span class="filter-item-name">Кроссовки есть в наличии</span>
            </label>
        </div>
    </div>
</template>

<script lang="ts">
    import { mapMutations, mapActions, mapGetters } from 'vuex';
    import { defineComponent } from "vue";
    import ProductList from './ProductList.vue';
    import SortDropdownMenu from './SortDropdownMenu.vue';

    export default defineComponent({
        components: { ProductList, SortDropdownMenu },

        props: {
            sortingOptions: {
                type: Array,
                required: true
            }
        },

        methods: {
            ...mapMutations(['setSelectedSearchValue', 'addStockFilter', 'setFiltersPanelVisibility']),
            ...mapActions(['applyFilters']),
            ...mapGetters(['selectedFiltersState', 'currentSelectedFilters', 'isMobile', 'isTablet']),

            toggleStockFilter() {
                this.addStockFilter();
                this.applyFilters();
            }
        },

        computed: {
            isChecked(this: any): boolean {
                return this.selectedFiltersState().inStock;
            }
        },
    })
</script>