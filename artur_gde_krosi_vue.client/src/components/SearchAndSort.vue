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
    import store from '@/store';
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
               this.showClearButton = this.searchQuery != '' ? true : false;
                store.state.selectedFilters.searchValue = this.searchQuery;
            },

            clearSearchValue(this: { searchQuery: string, showClearButton: boolean }) {
                store.state.selectedFilters.searchValue = '';
                this.searchQuery = store.state.selectedFilters.searchValue;
                this.showClearButton = false;
            }
        },

        mounted: function () {
            this.showClearButton = store.getters.currentSelectedFilters.searchValue != '' ? true : false;

            if (store.getters.currentSelectedFilters) {
                this.searchQuery = store.getters.currentSelectedFilters.searchValue;
            }
        },
    })
</script>