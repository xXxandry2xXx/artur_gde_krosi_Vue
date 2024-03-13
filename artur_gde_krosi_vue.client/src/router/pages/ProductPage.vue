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
            <span class="bread-crumb bread-crumb-current">{{ productData.name }}</span>
        </div>
    </div>
    <div class="product-page-content">

    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapMutations } from 'vuex';
    import axios from 'axios';
    import type { ProductInterface } from '@/store/modules/productsCatalog/types';

    export default defineComponent({
        data() {
            return {
                productId: this.$route.params.productId,
                productData: {}
            }
        },

        methods: {
            ...mapMutations(['setPreloaderVisibility']),

            async fetchProduct(this: any) {
                try {
                    const response = await axios.get('http://localhost:5263/Produt', { params: { 'ProductId': this.productId }, headers: { 'accept': '*/*' } });
                    this.productData = response.data[0]
                } catch (error) {
                    console.log(error)
                }
            }
        },

        async beforeMount() {
            this.setPreloaderVisibility(true);
            await this.fetchProduct().then(this.setPreloaderVisibility(false));
            //console.log(this.productData);
        }
    })
</script>

<style scoped>
</style>