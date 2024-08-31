import type { Module } from 'vuex';
import type { RootState } from '@/store/types';
import type { ProductsCatalogState } from '@/store/modules/productsCatalog/types';
import type { ProductsDataInterface, BrandsInterface, ModelsInterface, SizesInterface, AvailablePricesInterface } from '@/store/modules/productsCatalog/types';
import { mutations } from '@/store/modules/productsCatalog/mutations';
import { getters } from '@/store/modules/productsCatalog/getters';
import { actions } from '@/store/modules/productsCatalog/actions';

const state: ProductsCatalogState = {
    showCatalogPreloader: false,
    showFiltersPanel: false,

    selectedFilters: {
        priceMin: 0,
        priceMax: 0,
        brandIDs: [],
        modelIDs: [],
        checkedSizes: [],
        inStock: true,
        searchValue: '',
        sortOrder: '0',
    },
    sortingOptions: [
        { value: 0, name: 'По алфавиту (А-Я)' },
        { value: 1, name: 'По алфавиту (Я-А)' },
        { value: 2, name: 'Сначала дешевле' },
        { value: 3, name: 'Сначала дороже' },
        { value: 4, name: 'Сначала менее популярные' },
        { value: 5, name: 'Сначала более популярные' }
    ],
    productsData: {} as ProductsDataInterface,
    availablePrices: {} as AvailablePricesInterface,
    filteredProductsData: {} as ProductsDataInterface,
    brands: {} as BrandsInterface,
    sizes: {} as SizesInterface,
    models: {} as ModelsInterface,
    currentPage: 1,
    totalPages: 0,
};

const productsCatalog: Module<ProductsCatalogState, RootState> = {
    state,
    mutations,
    getters,
    actions
};

export default productsCatalog;