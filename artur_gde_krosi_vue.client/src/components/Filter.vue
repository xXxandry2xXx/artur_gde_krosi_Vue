<template>
    <div class="filter">
        <h2 class="filter-title">{{ filterName }}</h2>
        <div class="filter-items">
            <FilterItem v-for="item in filter" :item="item" />
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
            },
            filterName: {
                type: String,
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
        padding: 0 30px;
        margin-bottom: 10px;
    }

    .filter-items {
        padding: 0px 30px;
    }
</style>