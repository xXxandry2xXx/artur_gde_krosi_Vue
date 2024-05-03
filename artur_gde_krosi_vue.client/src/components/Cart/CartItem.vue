<template>
    <div class="cart-item">
        <div class="cart-item-main" @click="$router.push(`/products/productId=${productId}`)">
            <img :src="itemData.previewImage.imageSrc" />
            <div class="cart-item-info">
                <p class="cart-item-name">{{ itemData.name }}</p>
                <p class="cart-item-price">{{ itemData.price }}</p>
                <p class="cart-item-size"> Размер: {{ itemData.size }}</p>
            </div>
        </div>
        <button class="cart-item-remove-button" @click="removeItemFromCart(cartItemId)">
            <font-awesome-icon :icon="['fas', 'xmark']" />
        </button>
    </div>
</template>

<script lang="ts">
    import { mapActions } from 'vuex';
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
            productId: {
                type: String,
                required: true
            },

            variantId: {
                type: String,
                required: true
            },

            cartItemId: {
                type: String,
                required: true
            }
        },

        methods: {
            ...mapActions(['removeItemFromCart']),

            async fetchItemData(this: any) {
                try {
                    const response = await axios.get(
                        'http://localhost:5263/api/Product/GetProduct',
                        {
                            params: { 'ProductId': this.productId },
                            headers: { 'accept': '*/*' }
                        });

                    if (response.status === 200) {
                        const currentVariant = response.data.variants.find((variant: any) => variant.variantId === this.variantId)
                        this.itemData.previewImage = response.data.images.reverse()[0];
                        this.itemData.name = response.data.name;
                        this.itemData.size = currentVariant.shoeSize;
                        this.itemData.price = currentVariant.prise / 100;
                    }

                } catch (error) {
                    console.log(error);
                }
            }
        },

        async mounted() {
            await this.fetchItemData();
        }
    })
</script>