<template>
    <div class="product-page">
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

        <div class="product-page-main-content">
            <div class="product-main">
                <div class="product-page-images">
                    <img class="product-current-image" :src="currentImageSrc" />
                    <div class="product-all-images">
                        <img v-for="(image, index) in productImages"
                             class="product-image"
                             :class="{'product-current-image-icon': currentImgIndex === index}"
                             :src="image.imageSrc"
                             alt="product-image"
                             @click="openImage(index)" />
                    </div>
                </div>
                <div class="product-page-info">
                    <div class="product-page-info-details">
                        <h1 class="product-name">{{ productData.name }}</h1>
                        <p class="product-page-detail">Бренд: <span class="product-page-detail-value">{{ productData.modelKrosovock && productData.modelKrosovock.brend && productData.modelKrosovock.brend.name }}</span></p>
                        <p class="product-page-detail">Наличие: <span class="product-page-detail-value is-in-stock">{{ productData.variants && isInStock }}</span></p>
                        <div class="product-page-sizes">
                            <p class="product-page-detail">Размеры: </p>
                            <div class="product-page-sizes-list">
                                <span class="product-page-size-box" v-for="size in $store.state.productsCatalog.sizes" :class="{'product-page-size-box-unavailable': !availableSizes.includes(size)}">{{ size }}</span>
                            </div>
                        </div>
                    </div>

                    <div class="product-price-and-cart">
                        <p class="product-page-price">
                            {{ productData.variants && productData.variants[0] && productData.variants[0].prise/100 }}
                            <span> руб.</span>
                        </p>
                        <BorderedButton class="add-to-cart-button">
                            <font-awesome-icon :icon="['fas', 'cart-shopping']" />
                            В корзину
                        </BorderedButton>
                    </div>
                </div>
            </div>
        </div>

        <div class="product-page-additional-content">
            <!--ВСЯ ИНФОРМАЦИЯ НИЖЕ - ПРОСТО ПЛЕЙСХОЛДЕР-->
            <div class="product-page-description">
                <h2>Описание</h2>
                <p>
                    Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                    Feugiat nisl pretium fusce id velit ut tortor. Fames ac turpis egestas sed. Malesuada fames ac turpis egestas maecenas pharetra convallis.
                    Nibh tortor id aliquet lectus proin nibh nisl condimentum.
                    Purus in massa tempor nec feugiat nisl pretium. At risus viverra adipiscing at in tellus integer.
                </p>
            </div>
            <div class="product-page-characteristics">
                <h2>Основные характеристики</h2>
                <ul>
                    <li><span>Состав материала:</span><span>замша, кожа, текстиль</span></li>
                    <li><span>Комплектация:</span><span>коробка, кроссовки</span></li>
                    <li><span>Сезон:</span><span>зима, весна-осень</span></li>
                    <li><span>Страна производителя:</span><span>Вьетнам</span></li>
                    <li><span>Цвет:</span><span>Белый</span></li>
                </ul>
            </div>
        </div>
        <PopularProducts/>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapMutations, mapActions } from 'vuex';
    import PopularProducts from '@/components/MainPage/PopularProducts.vue';
    import axios from 'axios';

    export default defineComponent({
        components: { PopularProducts },

        data() {
            return {
                productId: this.$route.params.productId,
                productData: {},
                currentImgIndex: 0,
                productImages: []
            }
        },

        methods: {
            ...mapMutations(['setPreloaderVisibility']),
            ...mapActions(['fetchSizes']),

            async fetchProduct(this: any) {
                try {
                    const response = await axios.get('http://localhost:5263/Produt', { params: { 'ProductId': this.productId }, headers: { 'accept': '*/*' } });
                    this.productData = response.data[0];
                    this.productImages = this.productData.images.reverse()
                    this.currentImgIndex = this.productImages[0].index;
                } catch (error) {
                    console.log(error)
                }
            },

            openImage(this: any, index: number) {
                this.currentImgIndex = index;
                this.currentImage = this.productImages[this.currentImgIndex];
            }
        },

        computed: {
            quantityInStock(this: any) {
                if (this.productData.variants) return this.productData.variants.reduce((total: number, variant: any) => total + variant.quantityInStock, 0);
            },

            availableSizes(this: any) {
                if (this.productData.variants) {
                    const availableSizes = this.productData.variants
                        .filter((variant: any) => variant.quantityInStock > 0)
                        .map((variant: any) => variant.shoeSize);

                    return availableSizes;
                }
                return [];
            },
             
            isInStock(this: any) {
                if (this.quantityInStock > 0) {
                    return 'В наличии (' + this.quantityInStock + ' ' + 'шт.)'
                } else {
                    return 'Нет в наличии'
                }
            },

            currentImageSrc(this: any) {
                if (this.productImages[this.currentImgIndex]) return this.productImages[this.currentImgIndex].imageSrc;
            }
        },

        async beforeMount() {
            this.setPreloaderVisibility(true);
            await this.fetchSizes();
            await this.fetchProduct().then(this.setPreloaderVisibility(false));
        },
    })
</script>

<style scoped>
</style>