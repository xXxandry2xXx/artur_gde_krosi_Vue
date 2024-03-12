<template>
    <div class="top-bar">
        <span class="bread-crumb" @click="$router.go(-1)">Назад</span>
        <div class="bread-crumbs">
            <span class="bread-crumb" @click="$router.push('/')">Главная</span>
            /
            <router-link :to="{ name: 'productsPage', params: { page: 1 } }" class="bread-crumb">
                <span>Все кроссовки</span>
            </router-link>
            /
            <span class="bread-crumb bread-crumb-current">Кроссовок</span>
        </div>
    </div>
    <div class="product-page-content">

    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapGetters, mapActions } from 'vuex';
    import type { ProductInterface } from '@/store/modules/productsCatalog/types';

    export default defineComponent({
        data() {
            return {
                productId: this.$route.params.productId,
            }
        },

        methods: {
            ...mapGetters(['getProductsData']),
            ...mapActions(['fetchProducts']),
        },

        computed: {
            currentProduct(this: any) {
                
            }
        },

        async mounted() {
            
        }
    })
</script>

<style scoped>
</style>