<template>
    <aside class="filter-panel">
        <div class="filter-panel-butons disabled">
            <button class="filters-apply-button" @click="$store.dispatch('applyFilters')">Применить</button>
            <button class="filters-apply-button" @click="$store.dispatch('clearFilters')">Очистить</button>
        </div>

        <div class="filters-list">
            <div class="filter price-ranger-filter">
                <div class="filter-opener">
                    <h2 class="filter-title">Цена</h2>
                </div>
                <PriceRanger />
            </div>

            <Filter :filter="$store.state.brands" :filterName="'Бренды'" />

            <Filter v-show="$store.state.models.length > 0" :filter="$store.state.models" :filterName="'Модели'" />

            <Filter class="sizes-filter" :filter="$store.state.sizes" :filterName="'Размеры'" />
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
