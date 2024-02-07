<template>
    <aside class="filter-panel">
        <div class="filters-header">
            <h4>Фильтры</h4>
            <span>X</span>
        </div>
        <div class="filters-list">
            <Filter v-for="filter in filters" :filter="filter" />
        </div>
    </aside>
</template>

<script lang="ts">
    import { defineComponent } from "vue";
    import Filter from './Filter.vue';

    export default defineComponent({
        components: { Filter },
        props: {
            filters: {
                type: Object,
                required: true
            }
        }
    })
</script>

<style>
    .filters-header {
        display: flex;
        justify-content: space-between;
        background: #FDDD00;
        padding: 5px 10px;
        font-size: 19px;
        color: #947704;
        font-weight: 500;
        border-top-left-radius: 10px;
        border-top-right-radius: 10px;
    }

    .filters-list {
        padding: 0 30px;
    }

    .filter-panel {
        box-sizing: border-box;
        height: fit-content;
        background: #fff;
        border-radius: 10px;
        box-shadow: 4px 4px 7px 1px rgb(0, 0, 0, 20%);
        padding-bottom: 20px;
        border-bottom-left-radius: 10px;
        border-bottom-right-radius: 10px;
    }
</style>
