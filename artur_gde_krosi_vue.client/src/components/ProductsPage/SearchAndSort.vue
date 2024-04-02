<template>
    <div class="search-and-sort">

        <div class="filter-item-container">
            <label class="filter-item">
                <input class="filter-item-checkbox" type="checkbox" @change="toggleStockFilter" :checked="isChecked">
                <span class="filter-item-checkbox-fake"></span>
                <span class="filter-item-name">В наличии</span>
            </label>
        </div>
        <SortDropdownMenu class="sort-dropdown" :options="sortingOptions" />
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
            ...mapMutations(['setSelectedSearchValue', 'addStockFilter']),
            ...mapActions(['applyFilters']),
            ...mapGetters(['selectedFiltersState', 'currentSelectedFilters']),

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