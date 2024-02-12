<template>
    <aside class="filter-panel">
        <div class="filters-list">
            <Filter :filter="filterBrands" :filterName="filterBrandsName" />
            <Filter class="sizes-filter" :filter="filterSizes" :filterName="filterSizesName" />
        </div>
    </aside>
</template>

<script lang="ts">
    import { defineComponent } from "vue";
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
            },
        },
        data() {
            return {
                filterBrandsName: 'Бренды',
                filterSizesName: 'Размеры',
            }
        },
    })
</script>
