<template>
    <div class="filter">
        <div class="filter-opener" @click="collapseExpandFilter" v-if="filterName">
            <h2 class="filter-title">{{ filterName }}</h2>
            <span class="filter-collapse-expand-button" :class="{'filter-collapse-expand-button-expanded': !isCollapsed}"><font-awesome-icon :icon="['fas', 'angle-down']" /></span>
        </div>
        <div class="filter-items" v-show="!isCollapsed">
            <FilterItem v-if="filter" v-for="item in filter" :item="item" />
            <slot v-else></slot>
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
                required: false
            },
            filterName: {
                type: String,
                required: false
            }
        },
        methods: {
            collapseExpandFilter(this: { isCollapsed: boolean }) {
                this.isCollapsed = !this.isCollapsed;
            }
        }
    })
</script>