<template>
    <div class="filter">
        <div class="filter-opener" @click="colapseExpandFilter">
            <h2 class="filter-title">{{ filterName }}</h2>
            <span :class="{'filter-collapse-expand-button-expanded': !isCollapsed}"><font-awesome-icon :icon="['fas', 'angle-down']" /></span>
        </div>
        <div class="filter-items" v-show="!isCollapsed">
            <FilterItem v-for="item in filter" :item="item" />
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from "vue";
    import FilterItem from './FilterItem.vue'

    export default defineComponent({
        components: { FilterItem },

        data() {
            return {
                isCollapsed: false,
            }
        },

        props: {
            filter: {
                type: Object,
                required: true
            },
            filterName: {
                type: String,
                required: true
            }
        },
        methods: {
            colapseExpandFilter(this: { isCollapsed: boolean }) {
                this.isCollapsed = !this.isCollapsed;
            }
        }
    })
</script>