<template>
    <div class="filter">
        <h2 class="filter-title">{{ filter.name }}</h2>
        <div :class="filter.name == 'Размеры' ? 'sizes-filter-items' : 'filter-items'">
            <FilterItem v-for="item in filter.items" :item="item" />
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from "vue";
    import FilterItem from './FilterItem.vue'

    export default defineComponent({
        components: { FilterItem },
        props: {
            filter: {
                type: Object,
                required: true
            }
        }
    })
</script>

<style>
    .filter {
    background: #fff;
    border-radius: 7px;
    }

    .filter-title {
        margin-top: 20px;
        margin-bottom: 10px;
    }
</style>