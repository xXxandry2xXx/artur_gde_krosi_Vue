<template>
    <div class="filter" style="border: none;">
        <div class="filters-currently-selected" key="Date.now()">
            <FilterSelected v-for="(value, key) in this.$store.state.productsCatalog.selectedFilters" :selectedFilterData="{ name: key, value: value }"
                            v-show="key !== 'inStock' || key !== 'sortOrder'" />
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from "vue";
    import FilterSelected from './FilterSelected.vue'

    export default defineComponent({
        components: { FilterSelected },
    })
</script>

