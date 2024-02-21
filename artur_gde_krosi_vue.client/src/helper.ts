import store from '@/store';

export function getSelectedFiltersFromLocalStorage(): any {
    let selectedFiltersCache = localStorage.getItem('selectedFilters');
    let selectedFilters = selectedFiltersCache ? JSON.parse(selectedFiltersCache) : store.state.selectedFilters;
    return selectedFilters;
}