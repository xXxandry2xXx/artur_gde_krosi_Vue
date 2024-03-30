<template>
    <div class="not-found-page"><h1>По Вашему запросу ничего не найдено</h1></div>
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
                await this.fetchProducts().then(() => {
                    this.suggestedProducts = this.getProductsData().products;
                })
            }
        },

        async mounted() {
            await this.fetchSuggestedProducts();
            console.log(this.suggestedProducts)
        }
    })
</script>