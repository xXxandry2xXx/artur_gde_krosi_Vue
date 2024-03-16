<template>
    <div class="main-page-brands">
        <h1>БРЕНДЫ</h1>
        <div class="main-page-brands-list">
            <router-link :to="{ name: 'productsPage', params: { page: 1 }}" v-for="brand in brands" @click="loadFiltersWithCurrentBrand(brand)">
                <Brand :brand="brand" />
            </router-link>
        </div>
        <router-link :to="{ name: 'productsPage', params: { page: 1 } }"  class="main-page-show-all-button">
            СМОТРЕТЬ ВСЕ
        </router-link>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from "vue";
    import { mapActions, mapMutations } from 'vuex';
    import Brand from '@/components/MainPage/Brand.vue'

    export default defineComponent({
        components: { Brand },

        data() {
            return {
                brands: [
                    { brendId: '0cbdb0c5-4728-11ee-0a80-0978000ebd64', name: 'Adidas', imageSrc: 'src/assets/img/brands/adidas.png' },
                    { brendId: '5b28c16c-472b-11ee-0a80-0b6a000fb5ea', name: 'Nike', imageSrc: 'src/assets/img/brands/nike.png' },
                    { brendId: '095d5e4c-472b-11ee-0a80-0978000f64df', name: 'New Balance', imageSrc: 'src/assets/img/brands/newbalance.png' },
                    { brendId: '206fcb43-4cc4-11ee-0a80-02c8001413d5', name: 'Puma', imageSrc: 'src/assets/img/brands/puma.png' },
                    { brendId: 'ff83a703-4729-11ee-0a80-0978000f2f33', name: 'Vans', imageSrc: 'src/assets/img/brands/vans.png' },
                    { brendId: 'bd5641f0-4cc5-11ee-0a80-036a0015008c', name: 'Bape', imageSrc: 'src/assets/img/brands/bape.png' },
                ]
            }
        },

        methods: {
            ...mapMutations(['addFilter']),
            ...mapActions(['applyFilters', 'clearFilters']),

            loadFiltersWithCurrentBrand(brand: Object) {
                this.clearFilters();
                this.addFilter(brand);
                this.applyFilters();
            }
        }
    })
</script>