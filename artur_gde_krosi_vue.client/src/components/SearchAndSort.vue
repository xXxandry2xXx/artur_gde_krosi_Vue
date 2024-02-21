<template>
    <div class="search-and-sort">
        <div class="search-input-wrapper">
            <DefaultInput class="search-input" v-model="searchQuery" :value="searchQuery" @input="addSearchValue" placeholder="Введите название или бренд..." />
            <span class="clear-search-value" v-show="showClearButton" @click="clearSearchValue"><i class="fa-solid fa-xmark"></i></span>
            <button class="search-button" @click="$store.dispatch('applyFilters')"><i class="fas fa-search"></i></button>
        </div>
        <DropdownMenu class="sort-dropdown" :options="sortingOptions" />
    </div>
</template>

<script lang="ts">
    import { defineComponent } from "vue";
    import { getSelectedFiltersFromLocalStorage } from '@/helper';

    export default defineComponent({

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
            addSearchValue(this: { searchQuery: string, showClearButton: boolean }) {
                let selectedFilters = getSelectedFiltersFromLocalStorage();

                this.showClearButton = this.searchQuery != '' ? true : false;

                selectedFilters.searchValue = this.searchQuery;
                localStorage.setItem('selectedFilters', JSON.stringify(selectedFilters));
            },

            clearSearchValue(this: { searchQuery: string, showClearButton: boolean }) {
                let selectedFilters = getSelectedFiltersFromLocalStorage();

                selectedFilters.searchValue = '';
                this.searchQuery = selectedFilters.searchValue;
                localStorage.setItem('selectedFilters', JSON.stringify(selectedFilters));
                this.showClearButton = false;
            }
        },

        mounted: function () {
            let selectedFilters = getSelectedFiltersFromLocalStorage();

            this.showClearButton = selectedFilters.searchValue != '' ? true : false;

            if (selectedFilters) {
                this.searchQuery = selectedFilters.searchValue;
            }
        },
    })
</script>