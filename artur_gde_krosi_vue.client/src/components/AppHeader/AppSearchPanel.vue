<template>
    <div class="app-search-panel" v-show="isVisible">
        <div class="search-input-wrapper">
            <div class="search-input">
                <DefaultInput v-model="searchQuery" :value="searchQuery" @input="addSearchValue" placeholder="Введите название или бренд..." />
                <button class="clear-search-value" v-show="showClearButton" @click="clearSearchValue"><i class="fa-solid fa-xmark"></i></button>
            </div>
            <BorderedButton class="search-button">Найти</BorderedButton>
        </div>
        <div class="search-hints">

        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapMutations, mapActions, mapGetters } from 'vuex';

    export default defineComponent({
        props: {
            isVisible: {
                type: Boolean,
                required: true
            }
        },

        data() {
            return {
                searchQuery: '',
                showClearButton: false,
                allProducts: []
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

            async fetchAllProducts() {

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