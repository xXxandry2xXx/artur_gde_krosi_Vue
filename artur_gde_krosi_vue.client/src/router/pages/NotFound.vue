<template>
    <div class="not-found-page">
        <h1>404</h1>
        <p>Извините, такой страницы не существует</p>
    </div>
    <ProductsSlider v-if="suggestedProducts.length > 0" :sliderArray="suggestedProducts" :sliderTitle="'Возможно, Вас заинтересует'" />
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapActions, mapGetters } from 'vuex';
    import ProductsSlider from '@/components/ProductsSlider.vue';


    export default defineComponent({
        components: { ProductsSlider },

        data() {
            return {
                suggestedProducts: [],
            }
        },

        methods: {
            ...mapActions(['fetchProducts']),
            ...mapGetters(['getProductsData']),

            async fetchSuggestedProducts(this: any) {
                await this.fetchProducts();
                this.suggestedProducts = this.getProductsData().productList;
            }
        },

        mounted() {
            this.fetchSuggestedProducts();
        }
    })
</script>