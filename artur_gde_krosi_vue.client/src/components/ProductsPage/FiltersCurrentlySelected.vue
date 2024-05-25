<template>
    <div class="filter" style="border: none;">
        <div class="filters-currently-selected" :key="getFilteredProductsData().productList.length">
            <FilterSelected v-for="(value, key) in selectedFiltersCached()" :selectedFilterData="{ name: key, value: value }" 
                            v-show="key !== 'inStock' && key !== 'sortOrder'"/>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from "vue";
    import { mapGetters } from 'vuex';
    import FilterSelected from './FilterSelected.vue'

    export default defineComponent({
        components: { FilterSelected },

        methods: {
            ...mapGetters(['selectedFiltersCached', 'getFilteredProductsData'])
        },
    })
</script>

