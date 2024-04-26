<template>
    <div class="price-ranger">
        <div class="price-ranger-input-boxes">
            <div class="price-input">
                <span class="price-min">от</span>
                <input type="number" class="price-min-input" :value="minRangerValue" @input="handlePriceMinBox" @blur="handleEmptyMinPriceBox" :max="maxPrice" />
            </div>
            <span class="price-ranger-separator">-</span>
            <div class="price-input">
                <span class="price-max">до</span>
                <input type="number" class="price-max-input" :value="maxRangerValue" @input="handlePriceMaxBox" @blur="handleEmptyMaxPriceBox" :max="maxPrice" />
            </div>
        </div>
        <div class="price-ranger-slider">
            <div class="progress" :style="{ left: progressLeft, right: progressRight }"></div>
        </div>
        <div class="price-ranger-range-inputs">
            <input type="range" class="range-price-min" @input="handleMinPrice" :value="minRangerValue" :min="minPrice" :max="maxPrice" ref="minPriceSlider" />
            <input type="range" class="range-price-max" @input="handleMaxPrice" :value="maxRangerValue" :min="minPrice" :max="maxPrice" ref="maxPriceSlider" />
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from "vue";
    import { mapActions, mapMutations, mapGetters } from 'vuex';

    export default defineComponent({

        data() {
            return {
                minPrice: 0,
                maxPrice: 0,
                minRangerValue: 0,
                maxRangerValue: 0,
                progressLeft: '0%',
                progressRight: '0%',
                priceGap: 1000,
            }
        },

        methods: {
            ...mapActions(['fetchPrices']),
            ...mapMutations(['setMinSelectedPrice', 'setMaxSelectedPrice']),
            ...mapGetters(['availablePrices', 'selectedFiltersState']),

            handleMinPrice(this: any, event: Event) {
                let target = event.target as HTMLInputElement;
                let minVal = parseInt(target.value);

                this.minRangerValue = minVal;

                if (this.maxRangerValue - this.minRangerValue < this.priceGap) {
                    target.value = (this.maxRangerValue - this.priceGap).toString();
                    this.minRangerValue = this.maxRangerValue - this.priceGap;
                } else {
                    target.value = this.minRangerValue.toString()
                    this.progressLeft = (this.minRangerValue / this.maxPrice) * 100 + '%';
                }

                this.setMinSelectedPrice(Number(target.value));
            },

            handleMaxPrice(this: any, event: Event) {
                let target = event.target as HTMLInputElement;
                let maxVal = parseInt(target.value);

                this.maxRangerValue = maxVal;

                if (this.maxRangerValue - this.minRangerValue < this.priceGap) {
                    target.value = (this.minRangerValue + this.priceGap).toString();
                    this.maxRangerValue = this.minRangerValue + this.priceGap;
                } else {
                    target.value = this.maxRangerValue.toString()
                    this.progressRight = 100 - (this.maxRangerValue / this.maxPrice) * 100 + '%';
                }

                this.setMaxSelectedPrice(Number(target.value));
            },

            handlePriceMinBox(this: any, event: Event) {
                let target = event.target as HTMLInputElement;

                if (Number(target.value) < this.minPrice) {
                    target.value = this.minPrice.toString();
                } else if (Number(target.value) > this.maxPrice) {
                    target.value = (this.maxRangerValue - this.priceGap).toString();
                }
                if (target.value !== '' && Number(target.value) <= (this.maxRangerValue - this.priceGap)) this.handleMinPrice(event);
            },

            handlePriceMaxBox(this: any, event: Event) {
                let target = event.target as HTMLInputElement;

                if (Number(target.value) < this.minPrice) {
                    target.value = (this.minRangerValue + this.priceGap).toString();
                } else if (Number(target.value) > this.maxPrice) {
                    target.value = this.maxPrice.toString();
                }
                if (target.value !== '' && Number(target.value) >= (this.minRangerValue + this.priceGap)) this.handleMaxPrice(event);
            },

            handleEmptyMinPriceBox(this: any, event: Event) {
                let target = event.target as HTMLInputElement;
                if (target.value === '') {
                    target.value = this.minPrice;
                } else if (Number(target.value) >= (this.maxRangerValue - this.priceGap)) {
                    target.value = (this.maxRangerValue - this.priceGap).toString()
                }
                this.handleMinPrice(event)
            },

            handleEmptyMaxPriceBox(this: any, event: Event) {
                let target = event.target as HTMLInputElement;
                if (target.value === '') {
                    target.value = this.maxPrice;
                } else if (Number(target.value) <= (this.minRangerValue + this.priceGap)) {
                    target.value = (this.minRangerValue + this.priceGap).toString();
                }
                this.handleMaxPrice(event)
            },

            fillRangerData(this: any) {
                this.minPrice = 0;
                this.maxPrice = this.currentAvailablePrices.priseMax;
                this.minRangerValue = this.currentSelectedFilters.priceMin;
                this.maxRangerValue = this.currentSelectedFilters.priceMax !== 0 ? this.currentSelectedFilters.priceMax : this.currentAvailablePrices.priseMax;
                this.progressLeft = (this.minRangerValue / this.maxPrice) * 100 + '%';
                this.progressRight = 100 - (this.maxRangerValue / this.maxPrice) * 100 + '%';
            },

            async initRangerData(this: any) {
                await this.fillRangerData();
                if (this.$refs.minPriceSlider && this.$refs.maxPriceSlider) {
                    this.$refs.minPriceSlider.value = this.minRangerValue;
                    this.$refs.maxPriceSlider.value = this.maxRangerValue;
                }
            }
        },

        computed: {
            currentSelectedFilters(this: any) {
                return this.selectedFiltersState();
            },

            currentAvailablePrices(this: any) {
                return this.availablePrices();
            }
        },

        watch: {
            currentSelectedFilters: {
                deep: true,
                handler(this: any) {
                    this.initRangerData();
                },
            },
            currentAvailablePrices: {
                deep: true,
                handler(this: any) {
                    this.initRangerData();
                },
            },
        },

        async mounted() {
            await this.fetchPrices();
            this.initRangerData();
        }
    })
</script>