<template>
    <div class="cart-item">
        <div class="cart-item-main" @click="$router.push(`/products/productId=${cartItem.productId}`)">
            <img class="cart-item-img" :src="itemData.previewImage.imageSrc" />
            <div class="cart-item-info">
                <p class="cart-item-name">{{ itemData.name }}</p>
                <p class="cart-item-price">{{ itemData.price }}</p>
                <p class="cart-item-size" style=" color: #000; ">{{ cartItem.quantity }} шт.</p>
                <p class="cart-item-size"> Размер: {{ itemData.size }}</p>
            </div>
        </div>
        <div class="cart-item-remove-button" @click="removeItem()">
            <font-awesome-icon :icon="['fas', 'xmark']" />
        </div>
    </div>
</template>

<script lang="ts">
    import apiUrl from '@/helper'
    import { mapActions, mapGetters } from 'vuex';
    import { defineComponent } from 'vue';
    import axios from 'axios';

    export default defineComponent({
        data() {
            return {
                itemData: {
                    previewImage: '',
                    name: '',
                    size: '',
                    price: ''
                }
            }
        },

        props: {
            cartItem: {
                type: Object,
                required: true
            }
        },

        methods: {
            ...mapActions(['removeItemFromCart']),
            ...mapGetters(['isUserAuthorized']),

            async fetchItemData(this: any) {
                try {
                    const response = await axios.get(
                        apiUrl + '/Product/GetProduct',
                        {
                            params: { 'ProductId': this.cartItem.productId },
                            headers: { 'accept': '*/*' }
                        });

                    if (response.status === 200) {
                        const currentVariant = response.data.variants.find((variant: any) => variant.variantId === this.cartItem.variantId)
                        this.itemData.previewImage = response.data.images.reverse()[0];
                        this.itemData.name = response.data.name;
                        this.itemData.size = currentVariant.shoeSize;
                        this.itemData.price = currentVariant.prise / 100;
                    }

                } catch (error) {
                    console.log(error);
                }
            },

            removeItem(this: any) {
                if (this.isUserAuthorized()) {
                    this.removeItemFromCart(this.cartItem.shoppingСartId);
                } else {
                    this.removeItemFromCart(this.cartItem.variantId);
                }
            }
        },

        async mounted() {
            await this.fetchItemData();
        }
    })
</script>