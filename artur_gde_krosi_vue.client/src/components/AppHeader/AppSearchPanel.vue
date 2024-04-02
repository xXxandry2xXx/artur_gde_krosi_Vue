<template>
    <div class="app-search-panel" v-show="$store.state.showSearchPanel">
        <div class="search-input-wrapper">
            <div class="search-input">
                <DefaultInput v-model="searchQuery" :value="searchQuery" @input="addSearchValue" placeholder="Введите название или бренд..." />
                <button class="clear-search-value" v-show="showClearButton" @click="clearSearchValue"><i class="fa-solid fa-xmark"></i></button>
            </div>
            <BorderedButton class="search-button" @click="advancedSearch">Найти</BorderedButton>
        </div>
        <div class="search-hints" v-show="searchQuery != ''">
            <div class="search-hint" v-for="hint in sortedHints" @click="moveToProductPage(hint.id)">
                <img :src="hint.herfImage[0].imageSrc" alt="product-image" />
                <p>{{ hint.name }}</p>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapMutations, mapActions, mapGetters } from 'vuex';
    import axios from 'axios';

    export default defineComponent({

        data() {
            return {
                searchQuery: '',
                showClearButton: false,
                allProducts: []
            }
        },

        methods: {
            ...mapMutations(['setSelectedSearchValue', 'addStockFilter', 'setSearchPanelVisibility']),
            ...mapActions(['applyFilters']),
            ...mapGetters(['currentSelectedFilters']),

            addSearchValue(this: any) {
                this.showClearButton = this.searchQuery !== '' ? true : false;
                this.setSelectedSearchValue(this.searchQuery);
            },

            clearSearchValue(this: any) {
                this.searchQuery = '';
                this.setSelectedSearchValue(this.searchQuery);
                this.showClearButton = false;
            },

            async fetchAllProducts(this: any) {
                try {
                    const response = await axios.get('http://localhost:5263/api/Product/GetAllProductSearch');
                    this.allProducts = response.data;
                } catch (error) {
                    console.log(error)
                }
            },

            moveToProductPage(this: any, hintId: string) {
                this.setSearchPanelVisibility(false);
                this.$router.push(`/products/productId=${hintId}`)
            },

            advancedSearch(this: any) {
                if (this.searchQuery != '') {
                    this.setSearchPanelVisibility(false);
                    this.applyFilters();
                    this.$router.push('/products');
                } else {
                    console.log('ну а что ты ожидаешь найти с пустой строкой, бестолочь?');
                }
            },
        },

        computed: {
            sortedHints(this: any) {
                return this.allProducts.filter((product: any) => product.name.toLowerCase().includes(this.searchQuery.toLowerCase()));
            }
        },

        mounted() {
            this.fetchAllProducts();
            this.searchQuery = this.currentSelectedFilters().searchValue;
        }
    })
</script>