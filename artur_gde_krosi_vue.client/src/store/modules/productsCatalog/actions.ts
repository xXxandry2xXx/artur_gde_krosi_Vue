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

    async clearFilters() {
        const emptyFilters = {
            priceMin: 0,
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
        this.commit('setPreloaderVisibility', true);
        this.commit('setSelectedFilters', this.getters.currentSelectedFilters);

        this.dispatch('getFilteredData', this.getters.currentSelectedFilters).then(response => {
            this.commit('setFilteredProducts', response);
            this.commit('setPreloaderVisibility', false);
            this.commit('countPages');
        });
        this.dispatch('fetchModels');
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
            console.log(error);
            throw error;
        }
    },

    async fetchProducts() {
        try {
            const response = await axios.get('http://localhost:5263/api/Product/GetProductList');
            this.commit('setProducts', response.data);
        } catch (error) {
            console.log(error);
        }
    },

    async fetchModels() {
        let selectedFilters = this.getters.selectedFiltersState;
        if (selectedFilters.brandIDs) {
            try {
                const response = await axios.get('http://localhost:5263/api/Filter/ModelKrosovocks', {
                    headers: {
                        'accept': '*/*',
                        'brendsIds': selectedFilters.brandIDs.join()
                    }
                });

                let fetchedModels = response.data.reduce((accumulator: ModelInterface[], currentValue: { name: string, modelKrosovocks: ModelInterface[] }) => {
                    return accumulator.concat(currentValue.modelKrosovocks);
                }, []);

                this.commit('setModels', fetchedModels);
            } catch (error) {
                console.log(error);
            }
        } else {
            console.log('no fetched models found')
        }
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

}