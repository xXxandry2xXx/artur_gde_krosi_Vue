import type { GetterTree } from 'vuex';
import type { RootState } from '@/store/types';
import type { ProductsCatalogState } from '@/store/modules/productsCatalog/types';
import type { AvailablePricesInterface } from '@/store/modules/productsCatalog/types';

export const getters: GetterTree<ProductsCatalogState, RootState> = {
    isFiltersPanelShown: state => {
        return state.showFiltersPanel;
    },

    getProductsData: state => {
        return state.productsData;
    },

    getCatalogModels: state => {
        return state.models;
    },

    getCatalogBrands: state => {
        return state.brands;
    },

    selectedFiltersState: state => {
        return state.selectedFilters;
    },

    selectedFiltersCached: state => {
        let selectedFiltersCache = localStorage.getItem('selectedFilters');
        if (selectedFiltersCache !== null) return JSON.parse(selectedFiltersCache);
        return false;
    },

    currentSelectedFilters: state => {
        let selectedFiltersCache = localStorage.getItem('selectedFilters');
        return selectedFiltersCache ? JSON.parse(selectedFiltersCache) : state.selectedFilters
    },

    availablePrices: state => {
        const availablePrices: AvailablePricesInterface = {};

        for (const price in state.availablePrices) {
            availablePrices[price] = state.availablePrices[price] / 100
        }

        return availablePrices;
    },

    currentFiltersFormData: (state, getters) => {
        const form = new FormData();
        let selectedFilters = getters.currentSelectedFilters;

        selectedFilters.brandIDs.forEach((brand: string) => form.append('brendsIds', brand.toString()));
        selectedFilters.checkedSizes.forEach((size: string) => form.append('shoeSizesChecked', size.toString()));
        selectedFilters.modelIDs.forEach((model: string) => form.append('modelKrosovocksIds', model.toString()));
        form.append('availability', selectedFilters.inStock);
        form.append('PlaceholderContent', selectedFilters.searchValue.toString());
        form.append('sortOrder', selectedFilters.sortOrder.toString());
        form.append('priseDown', selectedFilters.priceMin.toString());
        form.append('priseUp', selectedFilters.priceMax.toString());
        form.append('pageProducts', state.currentPage.toString());

        return form;
    },

    getCurrentPage: state => {
        return state.currentPage;
    },

    getTotalPages: state => {
        return state.totalPages;
    },

    getFilteredProductsData: state => {
        return state.filteredProductsData
    },
}