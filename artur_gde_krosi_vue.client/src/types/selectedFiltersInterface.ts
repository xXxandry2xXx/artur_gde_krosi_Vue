export default interface SelectedFiltersInterface {
    priceMin: number,
    priceMax: number,
    brandIDs: Array<string | number>,
    modelIDs: Array<string | number>,
    checkedSizes: Array<string | number>,
    inStock: boolean,
    searchValue: string,
    sortOrder: number,
}