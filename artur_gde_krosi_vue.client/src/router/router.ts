import { createRouter, createWebHashHistory } from 'vue-router';
import ProductsPage from '@/router/pages/ProductsPage.vue';
import ProductPage from '@/router/pages/ProductPage.vue';
import UserPage from '@/router/pages/UserPage.vue';
import MainPage from '@/router/pages/MainPage.vue';
import NotFound from '@/router/pages/NotFound.vue';
import Confirmation from '@/router/pages/EmailConfirmation.vue';
import store from '@/store/index';
import type { NavigationGuardNext, RouteLocationNormalized } from 'vue-router';

const router = createRouter({
    routes: [
        { path: '/', component: MainPage },
        { path: '/products', component: ProductsPage, meta: { requiresFiltersLoading: true } },
        { name: 'productsPage', path: '/products/:page', component: ProductsPage, meta: { requiresFiltersLoading: true } },
        { name: 'productPage', path: '/products/productId=:productId', component: ProductPage },
        { name: 'userPage', path: '/account', component: UserPage, meta: { requiresAuthorizedUser: true } },
        { name: 'notFound', path: '/notfound', component: NotFound },
        { name: 'confirmation', path: '/confirmation/:email/:token', component: Confirmation, props: true },
        { path: '/:catchAll(.*)', component: NotFound },
    ],
    history: createWebHashHistory(),

    scrollBehavior(to) {
        if (to.hash) {
            return {
                el: to.hash,
                behavior: 'smooth',
            };
        } else {
            return {
                top: 0,
                behavior: 'smooth'
            };
        }
    },
});

router.beforeEach((to, from, next) => {
    if (to.meta.requiresAuthorizedUser && !store.getters.isUserAuthorized) {
        next('/');
    } else {
        next();
    }
});

export default router;
