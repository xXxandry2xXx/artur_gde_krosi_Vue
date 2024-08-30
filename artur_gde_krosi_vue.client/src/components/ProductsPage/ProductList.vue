<template>
    <div class="product-list-wrapper">
        <Preloader v-show="$store.state.productsCatalog.showCatalogPreloader" />
        <div v-if="getFilteredProductsData().productList.length > 0" class="product-list">
            <Product v-for="product in getFilteredProductsData().productList" :key="product.productId" :product="product"/>
        </div>
        <div v-else class="nothing-found">
            <div class="nothing-found-content">
                <span class="nothing-found-icon">
                    <font-awesome-icon :icon="['fas', 'ban']" />
                </span>
                <div>
                <span>Ничего не найдено.</span>
                <p>Попробуйте изменить фильтры или ввести другой поисковой запрос.</p>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapGetters } from 'vuex';
    import Product from './Product.vue';

    export default defineComponent({
        components: { Product },

        methods: {
            ...mapGetters(['getFilteredProductsData']),
        }
    })

</script>
