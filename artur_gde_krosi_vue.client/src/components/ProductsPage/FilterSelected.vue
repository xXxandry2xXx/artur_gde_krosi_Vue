<template>
    <div class="filters-currently-selected-group">
        <div class="filter-selected-box" v-if="selectedFilterData.value.constructor === Array && filterData.length > 0" v-for="filter in filterData">
            <button class="coat" :value="filter.value" @click.stop="removeSelectedFilter"></button>
            <p>{{ filter.displayedName }}</p>
            <div class="clear-value">
                <font-awesome-icon :icon="['fas', 'xmark']" />
            </div>
        </div>
        <div class="filter-selected-box" v-else-if="isShown">
            <button class="coat" @click.stop="removeSelectedFilter"></button>
            <p>{{ filterName }} <span>{{ filterData }}</span></p>
            <div class="clear-value">
                <font-awesome-icon :icon="['fas', 'xmark']" />
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { mapGetters, mapActions, mapMutations } from 'vuex';
    import { defineComponent } from "vue";

    export default defineComponent({

        data() {
            return {
                models: []
            }
        },

        props: ['selectedFilterData'],

        methods: {
            ...mapActions(['applyFilters', 'fetchModels']),
            ...mapMutations(['removeFilter', 'setMinSelectedPrice', 'setMaxSelectedPrice', 'setSelectedSearchValue']),
            ...mapGetters(['getCatalogBrands', 'availablePrices']),

            removeSelectedFilter(this: any, event: Event) {
                const element = event.target as HTMLButtonElement;

                if (this.selectedFilterData.value.constructor === Array) {
                    if (this.selectedFilterData.name === 'brandIDs') {
                        this.removeFilter({ brendId: element.value });
                    } else if (this.selectedFilterData.name === 'modelIDs') {
                        this.removeFilter({ modelKrosovockId: element.value });
                    } else if (this.selectedFilterData.name === 'checkedSizes') {
                        this.removeFilter(Number(element.value));
                    }
                } else if (this.selectedFilterData.name === 'priceMin') {
                    let minPrice = this.availablePrices().priseMin
                    this.setMinSelectedPrice(minPrice)
                } else if (this.selectedFilterData.name === 'priceMax') {
                    let maxPrice = this.availablePrices().priseMax
                    this.setMaxSelectedPrice(maxPrice);
                } else if (this.selectedFilterData.name === 'searchValue') {
                    this.setSelectedSearchValue('')
                }

                this.applyFilters();
            }
        },

        computed: {
            filterName(this: any) {
                let filterName;
                switch (this.selectedFilterData.name) {
                    case 'priceMin':
                        filterName = 'Цена: мин.';
                        break;
                    case 'priceMax':
                        filterName = 'Цена: макс.';
                        break;
                    case 'brandIDs':
                        filterName = 'Бренды';
                        break;
                    case 'modelIDs':
                        filterName = 'Модели';
                        break;
                    case 'checkedSizes':
                        filterName = 'Размеры';
                        break;
                    case 'searchValue':
                        filterName = 'Поиск';
                        break;
                    default: filterName = 'ОШИБКА: Фильтр не найден либо не должен отображаться';
                }

                return filterName;
            },

            filterData(this: any) {
                const data = this.selectedFilterData;
                const brands = this.getCatalogBrands();
                const models = this.models;
                let handledFilterData;

                if (data.value.constructor === Array) {
                    handledFilterData = [...data.value]
                    if (data.name === 'brandIDs' && brands && brands.length > 0) {
                        handledFilterData.forEach((filter: any, index: any) => {
                            const filterFullBrand = brands.find((brand: any) => brand.brendId === filter);
                            if (filterFullBrand) handledFilterData[index] = { displayedName: filterFullBrand.name, value: filterFullBrand.brendId };
                        })
                    } else if (data.name === 'modelIDs' && models && models.length > 0) {
                        handledFilterData.forEach((filter: any, index: any) => {
                            const filterFullModel = models.find((model: any) => model.modelKrosovockId === filter);
                            if (filterFullModel) handledFilterData[index] = { displayedName: filterFullModel.name, value: filterFullModel.modelKrosovockId };
                        })
                    } else if (data.name === 'checkedSizes') {
                        handledFilterData.forEach((filter: any, index: any) => {
                            const sizeFilterValue = Number(filter);
                            handledFilterData[index] = { displayedName: filter, value: sizeFilterValue };
                        })
                    }
                } else {
                    handledFilterData = data.value;
                }

                return handledFilterData;
            },

            isShown(this: any) {
                if (this.selectedFilterData.name === 'priceMin' || this.selectedFilterData.name === 'priceMax') {
                    let minPrice = this.availablePrices().priseMin
                    let maxPrice = this.availablePrices().priseMax
                    if (this.selectedFilterData.name === 'priceMin' && this.selectedFilterData.value !== minPrice) return true;
                    if (this.selectedFilterData.name === 'priceMax' && this.selectedFilterData.value !== maxPrice) return true;
                } else if (this.selectedFilterData.name === 'searchValue') {
                    if (this.selectedFilterData.value.length > 0) return true;
                }

                return false;
            }
        },

        async beforeMount() {
            this.models = await this.fetchModels([]);
        }
    })
</script>
