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

        this.dispatch('getFilteredData', this.getters.currentFiltersFormData).then(response => {
            this.commit('setFilteredProducts', response);
            this.commit('setPreloaderVisibility', false);
            this.commit('countPages');
        });
        this.dispatch('fetchModels');
    },

    async getFilteredData(state, filledFormData) {
        try {
            const response = await axios.post('http://localhost:5263/ProdutList', filledFormData, { headers: { 'Content-Type': 'multipart/form-data', 'accept': '*/*' }});
            return response.data;
        } catch (error) {
            console.log(error);
            throw error;
        }
    },

    async fetchProducts() {
        try {
            const response = await axios.get('http://localhost:5263/ProdutList/');
            this.commit('setProducts', response.data);
        } catch (error) {
            console.log(error);
        }
    },

    async fetchModels() {
        const form = new FormData();
        let selectedFilters = this.getters.selectedFiltersState;

        if (selectedFilters.brandIDs) {
            selectedFilters.brandIDs.forEach((brand: string) => form.append('brendsIds', brand.toString()));
            try {
                const response = await axios.post('http://localhost:5263/ModelKrosovocks', form, { headers: { 'Content-Type': 'multipart/form-data', 'accept': '*/*' }});

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
            const response = await axios.get('http://localhost:5263/Brends');
            this.commit('setBrands', response.data);
        } catch (error) {
            console.log(error);
        }
    },

    async fetchSizes() {
        try {
            const response = await axios.get('http://localhost:5263/ShoeSizes');
            this.commit('setSizes', response.data);
        } catch (error) {
            console.log(error);
        }
    },

}