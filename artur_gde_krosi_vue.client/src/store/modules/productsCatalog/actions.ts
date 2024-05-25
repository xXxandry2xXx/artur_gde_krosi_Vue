import axios from 'axios';
import type { ActionTree } from 'vuex';
import type { RootState } from '@/store/types';
import type { ProductsCatalogState } from '@/store/modules/productsCatalog/types';
import type { ModelInterface } from '@/store/modules/productsCatalog/types';

export const actions: ActionTree<ProductsCatalogState, RootState> = {

    async changePage(state, pageNumber) {
        this.commit('setCurrentPage', pageNumber);
        this.dispatch('loadAppliedFilters')
    },

    async clearFilters({ state }: { state: ProductsCatalogState }) {
        const emptyFilters = {
            priceMin: state.availablePrices.priseMin / 100,
            priceMax: 0,
            brandIDs: [],
            modelIDs: [],
            checkedSizes: [],
            inStock: false,
            searchValue: '',
            sortOrder: '0',
        }

        this.commit('setSelectedFilters', emptyFilters);
        localStorage.removeItem('selectedFilters');
        this.dispatch('loadAppliedFilters');
    },

    async applyFilters({ state }: { state: ProductsCatalogState }) {
        localStorage.setItem('selectedFilters', JSON.stringify(state.selectedFilters));
        this.dispatch('loadAppliedFilters');
    },

    async loadAppliedFilters() {
        this.commit('setCatalogPreloaderVisibility', true);
        this.commit('setSelectedFilters', this.getters.currentSelectedFilters);

        this.dispatch('getFilteredData', this.getters.currentSelectedFilters).then(response => {
            this.commit('setFilteredProducts', response);
            this.commit('setCatalogPreloaderVisibility', false);
            this.commit('countPages');
        });
        this.dispatch('setFetchedModels');
    },

    async setFetchedModels() {
        let selectedBrands = this.getters.selectedFiltersState.brandIDs;
        const fetchedModels = await this.dispatch('fetchModels', selectedBrands);
        this.commit('setModels', fetchedModels);
    },

    async getFilteredData({ state }: { state: ProductsCatalogState }, selectedFilters: any) {
        try {
            const headers: Record<string, string> = {
                'accept': '*/*',
            };

            if (selectedFilters.hasOwnProperty('priceMin')) {
                headers['priseDown'] = selectedFilters.priceMin.toString();
            }

            if (selectedFilters.hasOwnProperty('priceMax')) {
                headers['priseUp'] = selectedFilters.priceMax.toString();
            }

            if (selectedFilters.brandIDs) {
                headers['brendsIds'] = selectedFilters.brandIDs.join();
            }

            if (selectedFilters.modelIDs) {
                headers['modelKrosovocksIds'] = selectedFilters.modelIDs.join();
            }

            if (selectedFilters.checkedSizes) {
                headers['shoeSizesChecked'] = selectedFilters.checkedSizes.join();
            }

            if (selectedFilters.hasOwnProperty('inStock')) {
                headers['availability'] = selectedFilters.inStock.toString();
            }

            if (selectedFilters.searchValue) {
                headers['PlaceholderContent'] = selectedFilters.searchValue.toString();
            }

            if (selectedFilters.hasOwnProperty('sortOrder')) {
                headers['sortOrder'] = selectedFilters.sortOrder.toString();
            }

            headers['pageProducts'] = state.currentPage.toString();

            const response = await axios.get('http://localhost:5263/api/Product/GetProductList', {
                headers: headers
            });

            return response.data;
        } catch (error) {
            throw error;
        }
    },

    async fetchProducts() {
        this.commit('setPreloaderVisibility', true);
        try {
            const response = await axios.get('http://localhost:5263/api/Product/GetProductList');
            if (response.status === 200) {
                this.commit('setProducts', response.data);
                this.commit('setPreloaderVisibility', false);
            }
        } catch (error) {
            console.log(error);
        }
    },

    async fetchModels({ state }: { state: ProductsCatalogState }, brandsArray: Array<string>) {
        let fetchedModels = [];

        if (brandsArray) {
            try {
                const response = await axios.get('http://localhost:5263/api/Filter/ModelKrosovocks', { headers: { 'accept': '*/*', 'brendsIds': brandsArray.join() } });

                fetchedModels = response.data.reduce((modelsArray: ModelInterface[], currentBrand: { name: string, modelKrosovoks: ModelInterface[] }) => {
                    return modelsArray.concat(currentBrand.modelKrosovoks);
                }, []);


            } catch (error) {
                console.log(error);
            }
        } else {
            console.log('no fetched models found')
        }

        return fetchedModels;
    },

    async fetchBrands() {
        try {
            const response = await axios.get('http://localhost:5263/api/Filter/Brends');
            this.commit('setBrands', response.data);
        } catch (error) {
            console.log(error);
        }
    },

    async fetchSizes() {
        try {
            const response = await axios.get('http://localhost:5263/api/Filter/ShoeSizes');
            this.commit('setSizes', response.data);
        } catch (error) {
            console.log(error);
        }
    },

    async fetchPrices() {
        try {
            const response = await axios.get('http://localhost:5263/api/Filter/MinMaxPrise');
            this.commit('setPrices', response.data);
            if (this.getters.selectedFiltersState.priceMin == 0) this.commit('setMinSelectedPrice', response.data.priseMin / 100);
            if (this.getters.selectedFiltersState.priceMax == 0) this.commit('setMaxSelectedPrice', response.data.priseMax / 100);
        } catch (error) {
            console.log(error);
        }
    },

    async fetchProduct(state, productId) {
        try {
            const response = await axios.get('http://localhost:5263/api/Product/GetProduct', { params: { 'ProductId': productId }, headers: { 'accept': '*/*' } });
            console.log(response.data)
            return response;
        } catch (error) {
            console.log(error)
        }
    },

    async fetchVariant(state, variantId) {
        try {
            const response = await axios.get('http://localhost:5263/api/Product/Variant', {
                headers: {
                    'accept': '*/*',
                    'VariantId': variantId
                }
            });
            return response;
        } catch (error) {
            console.log(error)
        }
    }
}