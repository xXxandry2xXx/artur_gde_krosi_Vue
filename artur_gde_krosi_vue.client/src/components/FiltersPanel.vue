<template>
    <aside class="filter-panel">
        <button class="filters-apply-button" @click="$store.dispatch('applyFilters')">Применить</button>
        <button class="filters-apply-button disabled" @click="$store.commit('clearFilters')">Очистить фильтры</button>
        <div class="filters-list">
            <div class="filter instock-filter">
                <div class="filter-opener">
                    <h2 class="filter-title">Наличие</h2>
                </div>
                <div class="filter-item-container">
                    <label class="filter-item">
                        <input class="filter-item-checkbox" type="checkbox" @click="$store.commit('addStockFilter')" :checked="isChecked">
                        <span class="filter-item-checkbox-fake"></span>
                        <span class="filter-item-name">В наличии</span>
                    </label>
                </div>
            </div>
            <Filter :filter="filterBrands" :filterName="filterBrandsName" />
            <Filter class="sizes-filter" :filter="filterSizes" :filterName="filterSizesName" />
        </div>
    </aside>
</template>

<script lang="ts">
    import { defineComponent } from "vue";
    import { getSelectedFiltersFromLocalStorage } from '@/helper';
    import Filter from './Filter.vue';

    export default defineComponent({
        components: { Filter },

        props: {
            filterBrands: {
                type: Object,
                required: true
            },
            filterSizes: {
                type: Object,
                required: true
            }
        },
        data() {
            return {
                filterBrandsName: 'Бренды',
                filterSizesName: 'Размеры',
            }
        },

        computed: {
            isChecked() {
                let selectedFilters = getSelectedFiltersFromLocalStorage();
                return selectedFilters.inStock;
            }
        }
    })
</script>
