<template>
    <aside class="filter-panel">
        <div class="filter-panel-butons">
            <router-link :to="{ name: 'productsPage', params: { page: 1 } }">
                <BorderedButton class="filters-apply-button" @click="$store.dispatch('applyFilters')">Применить</BorderedButton>
            </router-link>
            <router-link :to="{ name: 'productsPage', params: { page: 1 } }">
                <BorderedButton class="filters-apply-button" @click="$store.dispatch('clearFilters')">Очистить</BorderedButton>
            </router-link>   
        </div>

        <div class="filters-list">
            <div class="filter price-ranger-filter">
                <div class="filter-opener">
                    <h2 class="filter-title">Цена</h2>
                </div>
                <PriceRanger />
            </div>

            <Filter :filter="$store.state.productsCatalog.brands" :filterName="'Бренды'" />

            <Filter v-show="$store.state.productsCatalog.models.length > 0" :filter="$store.state.productsCatalog.models" :filterName="'Модели'" />

            <Filter class="sizes-filter" :filter="$store.state.productsCatalog.sizes" :filterName="'Размеры'" />
        </div>
    </aside>
</template>

<script lang="ts">
    import { defineComponent } from "vue";
    import PriceRanger from './PriceRanger.vue'
    import Filter from './Filter.vue';

    export default defineComponent({
        components: { PriceRanger, Filter },
    })
</script>
