<template>


        <div class="main-page-content-preview">
            <div class="main-page-content-preview-text">
                <h1>О нас</h1>
                <p>
                    Качественные кроссовки по доступной цене. Мы находимся в самом центре г. Перми.
                    Множество кроссовок в наличии в магазине и под заказ.
                    Оригинал, реплика различного качества.
                    Мы развеиваем мифы о реплике в нашем магазине вы можете увидеть разницу оригинала и реплики в живую.
                </p>
                <router-link :to="{ name: 'productsPage', params: { page: 1 } }">
                    <span>ОТКРЫТЬ КАТАЛОГ</span>
                </router-link>
            </div>
            <div class="main-page-content-preview-img">
                <ProductsSlider v-if="getProductsData().productList && getProductsData().productList.length > 0" :sliderArray="getProductsData().productList" :definedViewportProductAmount="1" />
            </div>
        </div>

</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapGetters } from 'vuex';
    import ProductsSlider from '@/components/ProductsSlider.vue';

    export default defineComponent({

        components: { ProductsSlider },

        methods: {
            ...mapGetters(['getProductsData'])
        },

    })
</script>