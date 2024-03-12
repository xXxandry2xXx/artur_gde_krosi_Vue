<template>
    <div class="main-page-brands">
        <h1>БРЕНДЫ</h1>
        <div class="main-page-brands-list">
            <Brand v-for="brand in brands" :brand="brand"/>
        </div>
        <span class="main-page-show-all-button">СМОТРЕТЬ ВСЕ</span>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from "vue";
    import Brand from '@/components/MainPage/Brand.vue'

    export default defineComponent({
        components: { Brand },

        data() {
            return {
                brands: [
                    { name: 'Adidas', imageSrc: 'src/assets/img/brands/adidas.png' },
                    { name: 'Nike', imageSrc: 'src/assets/img/brands/nike.png' },
                    { name: 'New Balance', imageSrc: 'src/assets/img/brands/newbalance.png' },
                    { name: 'Puma', imageSrc: 'src/assets/img/brands/puma.png' },
                    { name: 'Vans', imageSrc: 'src/assets/img/brands/vans.png' },
                    { name: 'Bape', imageSrc: 'src/assets/img/brands/bape.png' },
                ]
            }
        }
    })
</script>