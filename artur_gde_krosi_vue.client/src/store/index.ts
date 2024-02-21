import { createStore } from 'vuex';
import FormData from 'form-data';
import axios from 'axios';
import { getSelectedFiltersFromLocalStorage } from '@/helper';
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
    },

    mutations: {

        clearFilters() {
            let filters = document.querySelectorAll<HTMLInputElement>('.filter-item-checkbox');
            filters.forEach(filter => {
                if (filter.checked) {
                    console.log(filter.value)
                }
            })
        },

        addFilter(state, value) {
            let selectedFilters = getSelectedFiltersFromLocalStorage();
            let targetFiltersArray = typeof value !== 'number' ? selectedFilters.brandIDs : selectedFilters.checkedSizes;

            if (targetFiltersArray.includes(value)) {
                targetFiltersArray = targetFiltersArray.splice(targetFiltersArray.indexOf(value), 1);
            } else {
                targetFiltersArray.push(value);
            }

            localStorage.setItem('selectedFilters', JSON.stringify(selectedFilters));
        },

        addStockFilter() {
            let selectedFilters = getSelectedFiltersFromLocalStorage();

            selectedFilters.inStock = !selectedFilters.inStock
            localStorage.setItem('selectedFilters', JSON.stringify(selectedFilters));
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

        async applyFilters() {
            const form = new FormData();
            let selectedFilters = getSelectedFiltersFromLocalStorage();

            selectedFilters.brandIDs.forEach((brand: string) => form.append('brendsIds', brand.toString()));
            selectedFilters.checkedSizes.forEach((size: string) => form.append('shoeSizesChecked', size.toString()));
            form.append('availability', selectedFilters.inStock);
            form.append('PlaceholderContent', selectedFilters.searchValue.toString());
            form.append('sortOrder', selectedFilters.sortOrder.toString());

            let prelodader = document.querySelector('.product-list-wrapper .preloader');
            if (prelodader) prelodader.classList.remove('disabled');

            let response = await axios.post(
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