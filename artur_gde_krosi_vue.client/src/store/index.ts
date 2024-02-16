import { createStore } from 'vuex';
import FormData from 'form-data';
import axios from 'axios';
import router from '@/router/router';
import type SelectedFiltersInterface from '@/types/selectedFiltersInterface';
import type ProductsDataInterface from '@/types/productsDataInterface';
import type BrandsInterface from '@/types/brandsInterface';
import type SizesInterface from '@/types/sizesInterface';

export default createStore({
    state: {
        selectedFilters: {
            priceMin: 0,
            priceMax: 0,
            brandIDs: [],
            modelIDs: [],
            checkedSizes: [],
            inStock: true,
            searchValue: '',
            sortOrder: 0,
        } as SelectedFiltersInterface,

        sortingOptions: [
            { value: 0, name: 'По алфавиту (А-Я)' },
            { value: 1, name: 'По алфавиту (Я-А)' },
            { value: 2, name: 'По цене (Дороже)' },
            { value: 3, name: 'По цене (Дешевле)' }
        ],
        productsData: {} as ProductsDataInterface,
        brands: {} as BrandsInterface,
        sizes: {} as SizesInterface,
    },

    mutations: {
        addFilter(state, value) {
            let selectedFiltersCache: any = localStorage.getItem('selectedFilters');
            let parsedSelectedFiltersCache = JSON.parse(selectedFiltersCache);

            let selectedFilters = state.selectedFilters;
            if (selectedFiltersCache) {
                selectedFilters = parsedSelectedFiltersCache;
            }

            if (typeof value === 'number') {
                if (selectedFilters.checkedSizes.includes(value)) {
                    selectedFilters.checkedSizes = selectedFilters.checkedSizes.filter(selectedFilter => selectedFilter !== value);
                } else {
                    selectedFilters.checkedSizes.push(value);
                }
            } else {
                if (selectedFilters.brandIDs.includes(value)) {
                    selectedFilters.brandIDs = selectedFilters.brandIDs.filter(selectedFilter => selectedFilter !== value);
                } else {
                    selectedFilters.brandIDs.push(value);
                }
            }
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
    },

    actions: {
        async updateUrlParams(state) {
            const queryParams: Record<string, string> = {};

            let selectedFiltersCache: any = localStorage.getItem('selectedFilters');
            let parsedSelectedFiltersCache = JSON.parse(selectedFiltersCache);

            let selectedFilters = this.state.selectedFilters;

            if (selectedFilters.brandIDs.length > 0) {
                queryParams.brands = this.state.selectedFilters.brandIDs.join(',');
            }
            if (selectedFilters.checkedSizes.length > 0) {
                queryParams.sizes = this.state.selectedFilters.checkedSizes.join(',');
            }

            await router.replace({ query: queryParams });
        },

        async applyFilters() {
            const form = new FormData();

            if (Object.keys(this.state.selectedFilters).length !== 0) {
                localStorage.setItem('selectedFilters', JSON.stringify(this.state.selectedFilters))
            }

            await this.dispatch('updateUrlParams');


            if (router.currentRoute.value.query.brands) {
                router.currentRoute.value.query.brands.split(',').forEach((brand: string) => {
                    form.append('brendsIds', brand);
                })
            }
            if (router.currentRoute.value.query.sizes) {
                router.currentRoute.value.query.sizes.split(',').forEach((size: string) => {
                    form.append('shoeSizesChecked', size);
                })
            }

            let response = await axios.post(
                'http://localhost:5263/Produts',
                form,
                {
                    headers: {
                        'Content-Type': 'multipart/form-data',
                        'accept': '*/*'
                    }
                }
            );
            console.log(response.data)
        },

        async fetchProducts() {
            await axios
                .get('http://localhost:5263/Produts/')
                .then(response => {
                    this.commit('setProducts', response.data);
                })
                .catch(error => {
                    console.log(error);
                })
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
})