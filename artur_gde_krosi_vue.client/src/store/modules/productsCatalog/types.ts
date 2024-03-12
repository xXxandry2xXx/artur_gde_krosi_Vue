
export interface VariantsInterface {
    variants: {
        shoeSize: number,
        quantityInStock: number,
        prise: number
    }[]
};

export interface ProductImagesInterface {
    images: { imageId: string }[]
};

export interface ProductInterface {
    productId: string,
    name: string,
    prise: number,
    variants: VariantsInterface[],
    modelKrosovock_Name: string,
    brend_Name: string,
    images: ProductImagesInterface
};

export interface ProductsDataInterface {
    priseMax: number,
    priseMin: number,
    productcount: number,
    products: ProductInterface[]
};

export interface ModelInterface {
    name: string,
    modelKrosovockId: string
};

export interface ModelsInterface {
    models: ModelInterface[]
};

export interface BrandInterface {
    brendId: string,
    name: string,
    modelKrosovocks: null
};

export interface BrandsInterface {
    brands: BrandInterface[]
};

export interface SizesInterface {
    sizes: String[]
};

export interface SelectedFiltersInterface {
    priceMin: number,
    priceMax: number,
    brandIDs: Array<string | number>,
    modelIDs: Array<string | number>,
    checkedSizes: Array<string | number>,
    inStock: boolean,
    searchValue: string,
    sortOrder: string,
};
export interface ProductsCatalogState {
    selectedFilters: SelectedFiltersInterface,
    sortingOptions: Array<Object>,
    productsData: ProductsDataInterface,
    filteredProductsData: ProductsDataInterface,
    brands: BrandsInterface,
    sizes: SizesInterface,
    models: ModelsInterface,
    currentPage: number,
    totalPages: number,
};