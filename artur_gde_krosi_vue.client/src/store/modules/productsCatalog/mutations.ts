import type { MutationTree } from 'vuex';
import type { ProductsCatalogState } from '@/store/modules/productsCatalog/types';

export const mutations: MutationTree<ProductsCatalogState> = {
    setCatalogPreloaderVisibility(state, value) {
        state.showCatalogPreloader = value;
    },

    setFiltersPanelVisibility(state, value) {
        state.showFiltersPanel = value;
    },

    addFilter(state, value) {
        let targetFiltersArray;

        if (typeof value === 'number') {
            targetFiltersArray = state.selectedFilters.checkedSizes;
        } else if (typeof value === 'object' && 'brendId' in value) {
            targetFiltersArray = state.selectedFilters.brandIDs;
            value = value.brendId;
        } else if (typeof value === 'object' && 'modelKrosovockId' in value) {
            targetFiltersArray = state.selectedFilters.modelIDs;
            value = value.modelKrosovockId;
        }

        if (targetFiltersArray) {
            if (targetFiltersArray.includes(value)) {
                targetFiltersArray = targetFiltersArray.splice(targetFiltersArray.indexOf(value), 1);
            } else {
                targetFiltersArray.push(value);
            }
        }
    },

    removeFilter(state, value) {
        let targetFiltersArray;

        if (typeof value === 'number') {
            targetFiltersArray = state.selectedFilters.checkedSizes;
        } else if (typeof value === 'object' && 'brendId' in value) {
            targetFiltersArray = state.selectedFilters.brandIDs;
            value = value.brendId;
        } else if (typeof value === 'object' && 'modelKrosovockId' in value) {
            targetFiltersArray = state.selectedFilters.modelIDs;
            value = value.modelKrosovockId;
        }

        if (targetFiltersArray) {
            if (targetFiltersArray.includes(value)) {
                targetFiltersArray = targetFiltersArray.splice(targetFiltersArray.indexOf(value), 1);
            } 
        }
    },

    addStockFilter(state) {
        state.selectedFilters.inStock = !state.selectedFilters.inStock
    },

    setFilteredProducts(state, targetProducts) {
        state.filteredProductsData = targetProducts;
    },

    setProducts(state, targetProducts) {
        state.productsData = targetProducts;
    },

    setBrands(state, brandsArray) {
        state.brands = brandsArray;
    },

    setSizes(state, sizesArray) {
        state.sizes = sizesArray;
    },

    setModels(state, modelsArray) {
        state.models = modelsArray;
    },

    countPages(state) {
        state.totalPages = Math.ceil(state.filteredProductsData.count / 20);
    },

    setCurrentPage(state, page) {
        state.currentPage = page
    },

    setSelectedFilters(state, targetSelectedFilters) {
        state.selectedFilters = targetSelectedFilters;
    },

    setMinSelectedPrice(state, price) {
        state.selectedFilters.priceMin = price;
    },

    setMaxSelectedPrice(state, price) {
        state.selectedFilters.priceMax = price;
    },

    setSelectedBrandIds(state, targetBrandIDs) {
        state.selectedFilters.brandIDs = targetBrandIDs;
    },

    setSelectedModelIds(state, targetModelIDs) {
        state.selectedFilters.modelIDs = targetModelIDs;
    },

    setSelectedSizes(state, sizes) {
        state.selectedFilters.checkedSizes = sizes;
    },

    setInStockMode(state, value) {
        state.selectedFilters.inStock = value;
    },

    setSelectedSearchValue(state, value) {
        state.selectedFilters.searchValue = value;
    },

    setSelectedSortingOrder(state, order) {
        state.selectedFilters.sortOrder = order;
    },

    setPrices(state, value) {
        state.availablePrices = value;
    }
}