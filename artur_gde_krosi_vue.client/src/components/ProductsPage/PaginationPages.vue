<template>
    <nav class="pagination-pages">
        <router-link :to="{ name: 'productsPage', params: { page: 1 } }"
                     class="pagination-page pagination-arrow"
                     @click="changePage(1)"
                     v-show="getCurrentPage() !== 1">

            <font-awesome-icon :icon="['fas', 'angles-left']" />
        </router-link>

        <router-link :to="{ name: 'productsPage', params: { page: pervPage } }"
                     @click="toPervPage"
                     class="pagination-page pagination-arrow"
                     :class="{'pagination-arrow-unavailable': getCurrentPage() == 1}">
            <font-awesome-icon :icon="['fas', 'angle-left']" />
        </router-link>

        <router-link :to="{ name: 'productsPage', params: { page: page } }" class="pagination-page"
                     v-for="page in visiblePages"
                     @click="changePage(page)"
                     :class="{'pagination-page-current': page === getCurrentPage()}">
            {{ page }}

        </router-link>

        <router-link :to="{ name: 'productsPage', params: { page: nextPage } }"
                     @click="toNextPage"
                     class="pagination-page pagination-arrow"
                     :class="{'pagination-arrow-unavailable': getCurrentPage() == getTotalPages()}">
            <font-awesome-icon :icon="['fas', 'angle-right']" />
        </router-link>

        <router-link :to="{ name: 'productsPage', params: { page: getTotalPages() } }"
                     class="pagination-page pagination-arrow"
                     @click="changePage(getTotalPages())"
                     v-show="getCurrentPage() !== getTotalPages()">
            <font-awesome-icon :icon="['fas', 'angles-right']" />
        </router-link>
    </nav>

</template>

<script lang="ts">
    import { defineComponent } from "vue";
    import { mapActions, mapGetters } from 'vuex';
    import router from '@/router/router';
    import store from '@/store';

    export default defineComponent({

        methods: {
            ...mapActions(['changePage']),
            ...mapGetters(['getCurrentPage', 'getTotalPages']),

            toPervPage(this: any) {
                if (Number(router.currentRoute.value.params.page) > 1) {
                    this.changePage(this.pervPage);
                }
            },
            toNextPage(this: any) {
                const totalPages: number = this.getTotalPages();
                if (Number(router.currentRoute.value.params.page) < totalPages) {
                    this.changePage(this.nextPage);
                }
            },
        },

        computed: {

            pervPage() {
                if (Number(router.currentRoute.value.params.page) > 1) {
                    return Number(router.currentRoute.value.params.page) - 1
                }
            },

            nextPage() {
                const totalPages: number = store.getters.getTotalPages;
                if (Number(router.currentRoute.value.params.page) < totalPages) {
                    return Number(router.currentRoute.value.params.page) + 1
                }
            },

            visiblePages(): number[] {
                const currentPage: number = store.getters.getCurrentPage;
                const totalPages: number = store.getters.getTotalPages;

                const startPage = Math.max(1, currentPage - 2);
                const endPage = Math.min(totalPages, currentPage + 2);

                return Array.from({ length: endPage - startPage + 1 }, (_, idx) => startPage + idx);
            },
        },

        beforeMount() {
            if (!router.currentRoute.value.params.page) router.currentRoute.value.params.page = 1;
        },
        beforeUpdate() {
            if (!router.currentRoute.value.params.page) router.currentRoute.value.params.page = 1;
        }
    })
</script>

