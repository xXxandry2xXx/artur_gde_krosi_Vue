<template>
    <nav class="pagination-pages">
        <router-link :to="{ name: 'productsPage', params: { page: 1 } }" 
                     class="pagination-page pagination-arrow" 
                     @click="$store.dispatch('changePage', 1)"
                     v-show="$store.state.currentPage !== 1">
                     
            <i class="fa-solid fa-angles-left"></i>
        </router-link>

        <router-link :to="{ name: 'productsPage', params: { page: pervPage } }"
                     @click="toPervPage"
                     class="pagination-page pagination-arrow"
                     :class="{'pagination-arrow-unavailable': $store.state.currentPage == 1}">
            <i class="fas fa-angle-left"></i>
        </router-link>

        <router-link :to="{ name: 'productsPage', params: { page: page } }" class="pagination-page"
                     v-for="page in visiblePages"
                     @click="$store.dispatch('changePage', page)"
                     :class="{'pagination-page-current': page === $store.state.currentPage}">
            {{ page }}

        </router-link>

        <router-link :to="{ name: 'productsPage', params: { page: nextPage } }"
                     @click="toNextPage"
                     class="pagination-page pagination-arrow"
                     :class="{'pagination-arrow-unavailable': $store.state.currentPage == $store.state.totalPages}">
            <i class="fas fa-angle-right"></i>
        </router-link>

        <router-link :to="{ name: 'productsPage', params: { page: $store.state.totalPages } }" 
                     class="pagination-page pagination-arrow" 
                     @click="$store.dispatch('changePage', $store.state.totalPages)"
                     v-show="$store.state.currentPage !== $store.state.totalPages">
            <i class="fa-solid fa-angles-right"></i>
        </router-link>
    </nav>

</template>

<script lang="ts">
    import { defineComponent } from "vue";
    import router from '@/router/router';
    import store from '@/store';

    export default defineComponent({

        methods: {
            toPervPage(this: any) {
                if (Number(router.currentRoute.value.params.page) > 1) {
                    store.dispatch('changePage', this.pervPage)
                }
            },
            toNextPage(this: any) {
                if (Number(router.currentRoute.value.params.page) < store.state.totalPages) {
                    store.dispatch('changePage', this.nextPage)
                }
            }
        },

        computed: {
            pervPage() {
                if (Number(router.currentRoute.value.params.page) > 1) {
                    return Number(router.currentRoute.value.params.page) - 1
                }
            },
            nextPage() {
                if (Number(router.currentRoute.value.params.page) < store.state.totalPages) {
                    return Number(router.currentRoute.value.params.page) + 1
                }
            },
            visiblePages(): number[] {
                const startPage = Math.max(1, store.state.currentPage - 2);
                const endPage = Math.min(store.state.totalPages, store.state.currentPage + 2);

                return Array.from({ length: endPage - startPage + 1 }, (_, idx) => startPage + idx);
            },
        }
    })
</script>

