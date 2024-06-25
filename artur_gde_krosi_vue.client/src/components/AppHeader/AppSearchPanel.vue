<template>
    <div class="app-subheader-search-content">
        <button class="app-subheader-button"
                @mouseenter="showSearchPanel"
                @mouseleave="hideSearchPanel"
                @click="toggleSearchPanel"
                ref="searchPanelButton">
            <font-awesome-icon :icon="['fas', 'magnifying-glass']" />
        </button>

        <transition :name="isMobile() || this.isTablet() ? 'slide' : 'fade'">
            <div class="app-search-panel" ref="searchPanel" @mouseenter="showSearchPanel" @mouseleave="hideSearchPanel" v-show="isSearchPanelVisible">
                <div class="search-input-wrapper">
                    <div class="search-input">
                        <DefaultInput v-model="searchQuery" :value="searchQuery" @input="addSearchValue" placeholder="Введите название или бренд..." />
                        <button class="clear-value" v-show="showClearButton" @click="clearSearchValue">
                            <font-awesome-icon :icon="['fas', 'xmark']" />
                        </button>
                    </div>
                    <BorderedButton class="search-button" @click="advancedSearch">Найти</BorderedButton>
                    <BorderedButton class="close-search-panel-button" @click="toggleSearchPanel" v-if="isMobile() || this.isTablet()">
                    <font-awesome-icon :icon="['fas', 'xmark']" />
                    </BorderedButton>
                </div>
                <div class="search-hints" v-show="searchQuery != '' && sortedHints.length > 0">
                    <div class="search-hint" v-for="hint in sortedHints" @click="moveToProductPage(hint.productId)">
                        <img :src="hint.herfImage" alt="product-image" />
                        <p>{{ hint.name }}</p>
                    </div>
                </div>
            </div>
        </transition>
    </div>

</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapMutations, mapActions, mapGetters } from 'vuex';
    import type { NavigationGuardNext, RouteLocationNormalized } from 'vue-router';
    import axios from 'axios';

    export default defineComponent({

        data() {
            return {
                searchQuery: '',
                showClearButton: false,
                allProducts: [],

                isSearchPanelVisible: false,
                searchPanelTimeout: null,
            }
        },

        methods: {
            ...mapMutations(['setSelectedSearchValue', 'addStockFilter', 'setSearchPanelVisibility']),
            ...mapActions(['applyFilters']),
            ...mapGetters(['currentSelectedFilters', 'isMobile', 'isTablet']),

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
                    const response = await axios.get('http://192.144.14.63/api/Product/GetAllProductSearch');
                    this.allProducts = response.data;
                } catch (error) {
                    console.log(error)
                }
            },

            moveToProductPage(this: any, hintId: string) {
                this.$router.push(`/products/productId=${hintId}`)
                this.toggleSearchPanel();
            },

            advancedSearch(this: any) {
                if (this.searchQuery != '') {
                    this.setSearchPanelVisibility(false);
                    this.applyFilters();
                    this.$router.push('/products');
                    this.toggleSearchPanel();
                }
            },

            toggleSearchPanel(this: any) {
                if (this.isMobile() || this.isTablet()) {
                    this.isSearchPanelVisible = !this.isSearchPanelVisible
                }
            },

            showSearchPanel(this: any, event: Event) {
                if (!this.isMobile() && !this.isTablet()) {
                    let target = event.target;
                    if (target === this.$refs.searchPanel || target === this.$refs.searchPanelButton) this.clearSearchPanelTimeout(this.searchPanelTimeout);
                    this.isSearchPanelVisible = true;
                }
            },

            hideSearchPanel() {
                if (!this.isMobile() && !this.isTablet()) this.setSearchPanelTimeout();
            },

            setSearchPanelTimeout(this: any) {
                this.searchPanelTimeout = setTimeout(() => this.isSearchPanelVisible = false, 500);
            },

            clearSearchPanelTimeout(this: any) {
                this.searchPanelTimeout = clearTimeout(this.searchPanelTimeout);
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

            this.$router.beforeEach((to: RouteLocationNormalized, from: RouteLocationNormalized, next: NavigationGuardNext) => {
                this.clearSearchPanelTimeout();
                this.isSearchPanelVisible = false;
                next();
            })
        }
    })
</script>