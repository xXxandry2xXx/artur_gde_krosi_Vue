<template>
    <div class="filter-item-container">
        <label class="filter-item">
            <input class="filter-item-checkbox"
                   type="checkbox"
                   :checked="isChecked"
                   :value="typeof item === 'number' ? item: item.brendId"
                   @change="toggleFilter" />
            <span class="filter-item-checkbox-fake"></span>
            <span v-if="item.name" class="filter-item-name">{{ item.name }}</span>
            <span v-else class="filter-item-name">{{ item }}</span>
        </label>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from "vue";
    import type BrandIdInterface from '@/types/brandIDInterface';
    import store from '@/store';

    export default defineComponent({

        props: {
            item: {
                type: [Object, Number],
                required: true
            }
        },

        methods: {
            toggleFilter(this: { item: number | BrandIdInterface }, event: Event) {

                let target = event.target as HTMLInputElement;
                let value = target.value;

                if (typeof this.item !== 'number') {
                    store.commit('addFilter', value);
                } else {
                    store.commit('addFilter', Number(value));
                }
            },
        },

        computed: {
            isChecked(this: { item: number | BrandIdInterface }) {
                let selectedFiltersCache = localStorage.getItem('selectedFilters');
                let selectedFilters = selectedFiltersCache ? JSON.parse(selectedFiltersCache) : store.state.selectedFilters;

                if (typeof this.item !== 'number') {
                    return selectedFilters.brandIDs.includes(this.item.brendId);
                } else {
                    return selectedFilters.checkedSizes.includes(this.item);
                }
            }
        }

    })
</script>