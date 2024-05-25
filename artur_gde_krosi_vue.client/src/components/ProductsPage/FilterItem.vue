<template>
    <div class="filter-item-container">
        <label class="filter-item">
            <input class="filter-item-checkbox"
                   type="checkbox"
                   :checked="isChecked"
                   :value="checkBoxValue"
                   @change="handleFilterChosing" 
                   />
            <span class="filter-item-checkbox-fake"></span>
            <span v-if="item.name" class="filter-item-name">{{ item.name }}</span>
            <span v-else class="filter-item-name">{{ item }}</span>
        </label>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from "vue";
    import { mapMutations, mapActions, mapGetters } from 'vuex';

    export default defineComponent({

        props: {
            item: {
                type: [Object, Number],
                required: true
            }
        },
        methods: {
            ...mapMutations(['addFilter']),
            ...mapActions(['setFetchedModels']),
            ...mapGetters(['selectedFiltersState', 'selectedFiltersCached']),

            handleFilterChosing(this: any) {
                this.addFilter(this.item);
                if (typeof this.item === 'object' && 'brendId' in this.item) this.setFetchedModels();
            }
        },

        computed: {
            isChecked(this: any) {
                let selectedFilters = this.selectedFiltersState();

                if (typeof this.item === 'number') {
                    return selectedFilters.checkedSizes.includes(this.item);
                } else if (typeof this.item === 'object' && 'brendId' in this.item) {
                    return selectedFilters.brandIDs.includes(this.item.brendId);
                } else if (typeof this.item === 'object' && 'modelKrosovockId' in this.item) {
                    return selectedFilters.modelIDs.includes(this.item.modelKrosovockId);
                }
            },

            checkBoxValue(this: { item: any }) {
                if (typeof this.item === 'number') {
                    return this.item;
                } else if (typeof this.item === 'object' && 'brendId' in this.item) {
                    return this.item.brendId;
                } else if (typeof this.item === 'object' && 'modelKrosovocks' in this.item) {
                    return this.item.modelKrosovockId;
                }
            }
        }

    })
</script>