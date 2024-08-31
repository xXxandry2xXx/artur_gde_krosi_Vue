<template>
    <div class="product-page-images">
        <div class="product-current-image-wrapper" ref="currentImageWrapper" 
             @mousemove="zoomCurrentImage" 
             @mouseleave="nullifyImageCondition" 
             v-touch:swipe.left="swipeLeft" 
             v-touch:swipe.right="swipeRight"
             >
            <img class="product-current-image" :src="currentImageSrc" ref="currentProductImage" />
        </div>
        <div class="product-all-images">
            <img v-for="(image, index) in images"
                 class="product-image"
                 :class="{'product-current-image-icon': currentImgIndex === index}"
                 :src="image.imageSrc"
                 alt="product-image"
                 @click="openImage(index)" />
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';

    export default defineComponent({
        data() {
            return {
                currentImgIndex: 0,
                mouseX: 0,
                mouseY: 0
            }
        },

        props: {
            images: {
                type: Array,
                required: true
            }
        },

        methods: {
            openImage(this: any, index: number) {
                this.currentImgIndex = index;
                this.currentImage = this.images[this.currentImgIndex];
            },

            zoomCurrentImage(this: any, event: Event) {
                this.$refs.currentProductImage.style.transform = 'scale(1.8)';

                const { top, left, width, height } = this.$refs.currentProductImage.getBoundingClientRect();

                this.mouseX = (event.pageX - left) / width * 100;
                this.mouseY = (event.pageY - top) / height * 100;

                this.$refs.currentProductImage.style.transformOrigin = `${this.mouseX}% ${this.mouseY}%`;
            },

            nullifyImageCondition(this: any) {
                this.$refs.currentProductImage.style.transform = null;
            },

            swipeLeft(this: any) {
                if (this.currentImgIndex == this.images.length - 1) {
                    this.currentImgIndex = 0;
                } else {
                    this.currentImgIndex += 1;
                }
            },

            swipeRight(this: any) {
                if (this.currentImgIndex == 0) {
                    this.currentImgIndex = this.images.length - 1;
                } else {
                    this.currentImgIndex -= 1;
                }
            }
        },

        computed: {
            currentImageSrc(this: any) {
                if (this.images[this.currentImgIndex]) return this.images[this.currentImgIndex].imageSrc;
            },


        }
    })
</script>