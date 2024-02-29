import { createStore } from 'vuex';
import FormData from 'form-data';
import axios from 'axios';
import type SelectedFiltersInterface from '@/types/selectedFiltersInterface';
import type ProductsDataInterface from '@/types/productsDataInterface';
import type BrandsInterface from '@/types/brandsInterface';
import type ModelInterface from '@/types/modelInterface';
import type SizesInterface from '@/types/sizesInterface';

export default createStore({
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
        productsData: {} as ProductsDataInterface,
        brands: {} as BrandsInterface,
        sizes: {} as SizesInterface,
        models: {},
        availablePrices: { minAvailablePrice: 0, maxAvailablePrice: 0 }
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

        setProducts(state, targetProducts) {
            state.productsData = targetProducts;
        },

        setBrands(state, brandsArray) {
            state.brands = brandsArray;
        },

        setSizes(state, sizesArray) {
            state.sizes = sizesArray;
        },

        setAvailablePrices(state, prices) {
            state.availablePrices = prices;
        }
    },

    getters: {
        currentSelectedFilters: state => {
            let selectedFiltersCache = localStorage.getItem('selectedFilters');
            return selectedFiltersCache ? JSON.parse(selectedFiltersCache) : state.selectedFilters
        },

        availablePrices: state => {
            return state.availablePrices;
        }
    },

    actions: {

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

            let prelodader = document.querySelector('.product-list-wrapper .preloader');
            if (prelodader) prelodader.classList.remove('disabled');

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
                    this.commit('setProducts', response.data);
                    if (prelodader) prelodader.classList.add('disabled');
                })
                .catch(error => console.log(error))

            this.dispatch('fetchModels')
        },

        async fetchModels() {
            const form = new FormData();
            let selectedFilters = this.state.selectedFilters;

            if (selectedFilters.brandIDs) {
                selectedFilters.brandIDs.forEach((brand) => form.append('brendsIds', brand.toString()));

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

        async fetchPrices() {
            try {
                const response = await axios.get('http://localhost:5263/Produts/');
                let fetchedAvailablePrices = { minAvailablePrice: response.data.priseMin, maxAvailablePrice: response.data.priseMax }
                this.commit('setAvailablePrices', fetchedAvailablePrices);
            } catch (error) {
                console.log(error);
            }
        }
    }
})