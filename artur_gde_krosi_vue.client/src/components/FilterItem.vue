<template>
    <div class="filter-item-container">
        <label class="filter-item">
            <input class="filter-item-checkbox"
                   type="checkbox"
                   :checked="isChecked(item)"
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
    import router from '@/router/router';
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

            isChecked(this: { item: number | BrandIdInterface }) {
                if (typeof this.item !== 'number') {
                    if (router.currentRoute.value.query.brands) return router.currentRoute.value.query.brands.includes(this.item.brendId);
                } else {
                    if (router.currentRoute.value.query.sizes) return router.currentRoute.value.query.sizes.includes(this.item);
                }
            }
        },

        computed: {
            
        }
    })
</script>