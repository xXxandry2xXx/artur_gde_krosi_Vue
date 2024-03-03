import type { Module } from 'vuex';
import FormData from 'form-data';
import axios from 'axios';
import type ProductsBrowsingState from '@/store/types';
import type SelectedFiltersInterface from '@/types/selectedFiltersInterface';
import type ProductsDataInterface from '@/types/productsDataInterface';
import type BrandsInterface from '@/types/brandsInterface';
import type ModelInterface from '@/types/modelInterface';
import type ModelsInterface from '@/types/modelsInterface';
import type SizesInterface from '@/types/sizesInterface';


const productsBrowsing: Module<ProductsBrowsingState, any> = {
    state: {
        selectedFilters: {
            priceMin: 0,
            priceMax: 0,
            brandIDs: [],
            modelIDs: [],
            checkedSizes: [],
            inStock: false,
            searchValue: '',
            sortOrder: 0,
        } as SelectedFiltersInterface,
        sortingOptions: [
            { value: 0, name: 'По алфавиту (А-Я)' },
            { value: 1, name: 'По алфавиту (Я-А)' },
            { value: 2, name: 'По цене (Сначала дешевле)' },
            { value: 3, name: 'По цене (Сначала дороже)' }
        ],
        filteredProductsData: {} as ProductsDataInterface,
        productsData: {} as ProductsDataInterface,
        brands: {} as BrandsInterface,
        sizes: {} as SizesInterface,
        models: {} as ModelsInterface,
        currentPage: 1,
        totalPages: 0,
    },

    mutations: {

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

        countPages(state) {
            state.totalPages = Math.ceil(state.filteredProductsData.productcount / 20);
        }
    },

    getters: {
        currentSelectedFilters: state => {
            let selectedFiltersCache = localStorage.getItem('selectedFilters');
            return selectedFiltersCache ? JSON.parse(selectedFiltersCache) : state.selectedFilters
        },

        availablePrices: state => {
            return { minAvailablePrice: 0, maxAvailablePrice: state.productsData.priseMax }
        }
    },

    actions: {

        async changePage(state, pageNumber) {
            (this.state.currentPage as any) = pageNumber;

            this.dispatch('loadAppliedFilters')
        },

        async clearFilters() {
            this.state.selectedFilters = {
                priceMin: 0,
                priceMax: 0,
                brandIDs: [],
                modelIDs: [],
                checkedSizes: [],
                inStock: false,
                searchValue: '',
                sortOrder: 0,
            }

            localStorage.removeItem('selectedFilters');
            this.dispatch('loadAppliedFilters');
        },

        async applyFilters() {
            localStorage.setItem('selectedFilters', JSON.stringify(this.state.selectedFilters));
            this.dispatch('loadAppliedFilters');
        },

        async loadAppliedFilters() {
            this.state.showPreloader = true;
            const form = new FormData();
            let selectedFilters = this.getters.currentSelectedFilters;
            this.state.selectedFilters = selectedFilters;

            selectedFilters.brandIDs.forEach((brand: string) => form.append('brendsIds', brand.toString()));
            selectedFilters.checkedSizes.forEach((size: string) => form.append('shoeSizesChecked', size.toString()));
            selectedFilters.modelIDs.forEach((model: string) => form.append('modelKrosovocksIds', model.toString()));
            form.append('availability', selectedFilters.inStock);
            form.append('PlaceholderContent', selectedFilters.searchValue.toString());
            form.append('sortOrder', selectedFilters.sortOrder.toString());
            form.append('priseDown', selectedFilters.priceMin.toString());
            form.append('priseUp', selectedFilters.priceMax.toString());
            form.append('pageProducts', this.state.currentPage.toString());

            await axios.post(
                'http://localhost:5263/Produts',
                form,
                {
                    headers: {
                        'Content-Type': 'multipart/form-data',
                        'accept': '*/*'
                    }
                }
            )
                .then(response => {
                    this.commit('setFilteredProducts', response.data);
                    this.state.showPreloader = false;
                })
                .catch(error => console.log(error))

            this.dispatch('fetchModels');
            this.commit('countPages')
        },

        async fetchProducts() {
            try {
                const response = await axios.get('http://localhost:5263/Produts/');
                this.commit('setProducts', response.data);
            } catch (error) {
                console.log(error);
            }
        },

        async fetchModels() {
            const form = new FormData();
            let selectedFilters = this.state.selectedFilters;

            if (selectedFilters.brandIDs) {
                selectedFilters.brandIDs.forEach((brand: string) => form.append('brendsIds', brand.toString()));

                await axios.post(
                    'http://localhost:5263/ModelKrosovocks',
                    form,
                    {
                        headers: {
                            'Content-Type': 'multipart/form-data',
                            'accept': '*/*'
                        }
                    }
                )
                    .then(response => {
                        this.state.models = response.data.reduce((accumulator: ModelInterface[], currentValue: { name: string, modelKrosovocks: ModelInterface[] }) => {
                            return accumulator.concat(currentValue.modelKrosovocks);
                        }, []);
                    })
                    .catch(error => console.log(error))
            } else {
                console.log('no fetched models found')
            }
        },

        async fetchBrands() {
            await axios
                .get('http://localhost:5263/Brends')
                .then(response => {
                    this.commit('setBrands', response.data);
                })
                .catch(error => {
                    console.log(error);
                })
        },

        async fetchSizes() {
            await axios
                .get('http://localhost:5263/ShoeSizes')
                .then(response => {
                    this.commit('setSizes', response.data);
                })
                .catch(error => {
                    console.log(error);
                })
        },
    }
};

export default productsBrowsing;