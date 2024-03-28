<template>
    <div class="search-and-sort">

        <div class="filter-item-container">
            <label class="filter-item">
                <input class="filter-item-checkbox" type="checkbox" @change="toggleStockFilter" :checked="isChecked">
                <span class="filter-item-checkbox-fake"></span>
                <span class="filter-item-name">В наличии</span>
            </label>
        </div>

        <div class="search-input-wrapper">
            <div class="search-input">
                <DefaultInput v-model="searchQuery" :value="searchQuery" @input="addSearchValue" placeholder="Введите название или бренд..." />
                <button class="clear-search-value" v-show="showClearButton" @click="clearSearchValue"><i class="fa-solid fa-xmark"></i></button>
            </div>
            <BorderedButton class="search-button" @click="$store.dispatch('applyFilters')">
                <font-awesome-icon :icon="['fas', 'magnifying-glass']" />
            </BorderedButton>
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

        data() {
            return {
                searchQuery: '',
                showClearButton: false,
            }
        },

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


            addSearchValue(this: any) {
                this.showClearButton = this.searchQuery !== '' ? true : false;
                this.setSelectedSearchValue(this.searchQuery)
            },

            clearSearchValue(this: any) {
                this.setSelectedSearchValue('')
                this.searchQuery = this.selectedFiltersState.searchValue;
                this.showClearButton = false;
                this.applyFilters();
            },

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

        mounted() {
            this.showClearButton = this.currentSelectedFilters().searchValue != '' ? true : false;

            if (this.currentSelectedFilters()) {
                this.searchQuery = this.currentSelectedFilters().searchValue;
            }
        },
    })
</script>