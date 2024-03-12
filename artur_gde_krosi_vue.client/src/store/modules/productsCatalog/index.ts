import type { Module } from 'vuex';
import type { RootState } from '@/store/types';
import type { ProductsCatalogState } from '@/store/modules/productsCatalog/types';
import type { ProductsDataInterface, BrandsInterface, ModelsInterface, SizesInterface } from '@/store/modules/productsCatalog/types';
import { mutations } from '@/store/modules/productsCatalog/mutations';
import { getters } from '@/store/modules/productsCatalog/getters';
import { actions } from '@/store/modules/productsCatalog/actions';

const state: ProductsCatalogState = {
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
        { value: 2, name: 'По цене (Сначала дешевле)' },
        { value: 3, name: 'По цене (Сначала дороже)' }
    ],
    productsData: {} as ProductsDataInterface,
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