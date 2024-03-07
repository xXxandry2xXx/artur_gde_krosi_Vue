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
            <span class="bread-crumb">Кроссовок</span>
        </div>
    </div>

</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    //import type ProductInterface from '@/types/productInterface';

    export default defineComponent({

    })
</script>

<style scoped>
   
</style>