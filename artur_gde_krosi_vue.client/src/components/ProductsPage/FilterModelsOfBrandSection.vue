<template>
    <div class="models-of-brand-section" v-if="brandIsSelected && models.length > 0">
        <div class="brand-title" @click="collapseExpandFilter">
            <h2>{{ brand.name }}</h2>
            <span class="filter-collapse-expand-button" :class="{'filter-collapse-expand-button-expanded': !isCollapsed}">
                <font-awesome-icon :icon="['fas', 'angle-down']" />
            </span>
        </div>
        
        <div class="models-of-brand-boxes" v-show="!isCollapsed">
            <Filter :filter="models" />
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from "vue";
    import { mapActions, mapGetters } from 'vuex';
    import Filter from './Filter.vue';

    export default defineComponent({
        components: { Filter },

        data() {
            return {
                models: [],
                isCollapsed: false,
            }
        },

        props: {
            brand: {
                type: Object,
                required: true
            },
        },

        methods: {
            ...mapActions(['fetchModels']),
            ...mapGetters(['selectedFiltersState']),

            collapseExpandFilter(this: { isCollapsed: boolean }) {
                this.isCollapsed = !this.isCollapsed;
            }
        },

        computed: {
            brandIsSelected(this: any) {
                let selectedBrands = this.selectedFiltersState().brandIDs
                if (selectedBrands.length > 0 && selectedBrands.includes(this.brand.brendId) || selectedBrands.length == 0) {
                    return true;
                } else {
                    return false;
                }
            }
        },

        async beforeMount() {
            let fetched = await this.fetchModels([this.brand.brendId]);
            this.models = fetched[0].modelKrosovoks;
        }
    })
</script>