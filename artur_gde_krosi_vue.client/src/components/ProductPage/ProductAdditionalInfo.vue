<template>
    <div class="product-page-additional-content">
        <div class="product-page-description product-page-section">
            <div class="product-page-header">
                <h2>Описание</h2>
                <BorderedButton class="characteristic-interaction-button">
                    <font-awesome-icon :icon="['fas', 'plus']" /> Изменить описание
                </BorderedButton>
            </div>
            <p>
                Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                Feugiat nisl pretium fusce id velit ut tortor. Fames ac turpis egestas sed. Malesuada fames ac turpis egestas maecenas pharetra convallis.
                Nibh tortor id aliquet lectus proin nibh nisl condimentum.
                Purus in massa tempor nec feugiat nisl pretium. At risus viverra adipiscing at in tellus integer.
            </p>
        </div>
        <div class="product-page-characteristics product-page-section">
            <div class="product-page-header">
                <h2>Основные характеристики</h2>
                <BorderedButton class="characteristic-interaction-button" @click="openNewCharPopup('add-new-char')">
                    <font-awesome-icon :icon="['fas', 'plus']" /> Добавить новую характеристику
                </BorderedButton>
            </div>
            <ul v-if="productData.characteristicProducts.length > 0">
                <ProductCharacteristic v-for="char in $store.state.productCharacteristics.characteristicsList" :characteristicData="char" />
            </ul>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapMutations } from 'vuex';
    import ProductCharacteristic from '@/components/ProductPage/ProductCharacteristic.vue';

    export default defineComponent({

        components: { ProductCharacteristic },

        props: ['productData'],

        methods: {
            ...mapMutations(['setPopupVisibility', 'setPopupMode', 'setCurrentProductId']),

            openNewCharPopup(this: any,  popupMode: string) {
                this.setPopupVisibility(true);
                this.setPopupMode(popupMode);
            }
        },

        mounted() {
            const productId = this.productData.productId
            this.setCurrentProductId(productId);
        }
    })
</script>