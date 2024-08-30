<template>
    <div class="product-page-additional-content">
        <div class="product-page-description product-page-section">
            <div class="product-page-header">
                <h2>ОПИСАНИЕ</h2>
                <BorderedButton class="characteristic-interaction-button" v-if="isUserManager">
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
        <div class="product-page-characteristics product-page-section" v-if="productData.characteristicProducts.length > 0 || isUserManager">
            <div class="product-page-header">
                <h2>Основные характеристики</h2>
                <BorderedButton class="characteristic-interaction-button" @click="openNewCharPopup('add-new-char')"  v-if="isUserManager">
                    <font-awesome-icon :icon="['fas', 'plus']" /> Добавить новую характеристику
                </BorderedButton>
            </div>
            <ul>
                <ProductCharacteristic v-for="char in $store.state.productCharacteristics.characteristicsList" :characteristicData="char" :isManager="isUserManager"/>
            </ul>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapMutations, mapGetters } from 'vuex';
    import ProductCharacteristic from '@/components/ProductPage/ProductCharacteristic.vue';

    export default defineComponent({

        components: { ProductCharacteristic },

        props: ['productData'],

        methods: {
            ...mapGetters(['getAuthorizedUserToken']),
            ...mapMutations(['setPopupVisibility', 'setPopupMode', 'setCurrentProductId']),

            openNewCharPopup(this: any, popupMode: string) {
                this.setPopupVisibility(true);
                this.setPopupMode(popupMode);
            },

            decodeToken(this: any) {

            }
        },

        computed: {
            isUserManager(this: any) {
                const token = this.getAuthorizedUserToken();

                if (token) {
                    const tokenParts = token.split('.');
                    const decodedPayload = JSON.parse(atob(tokenParts[1]));
                    const currentUserRole = decodedPayload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

                    return currentUserRole === "Manager";
                }
            }
        },

        mounted() {
            const productId = this.productData.productId
            this.setCurrentProductId(productId);
        }
    })
</script>