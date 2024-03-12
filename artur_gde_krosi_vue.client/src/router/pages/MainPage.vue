<template>
    <div class="main-page-content">
        <ContentPreview />
        <Brands />
        <PopularProducts />
    </div>
</template>

<script lang="ts">
    import ContentPreview from '@/components/MainPage/ContentPreview.vue';
    import Brands from '@/components/MainPage/Brands.vue';
    import PopularProducts from '@/components/MainPage/PopularProducts.vue';
    import { defineComponent } from 'vue';

    export default defineComponent({
        components: { ContentPreview, Brands, PopularProducts }
    })
</script>
