<template>
    <div class="main-page-content-preview">
        <div class="main-page-content-preview-info">
            <h1>Какая-то инфа</h1>
            <p>
                Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.
            </p>
            <router-link :to="{ name: 'productsPage', params: { page: 1 } }">
                <BorderedButton class="main-page-content-preview-catalog-button">Каталог</BorderedButton>
            </router-link>
        </div>
        <div class="main-page-content-preview-media">
            <img src="../../assets/img/Placeholder.png" alt="placeholder" />
        </div>
    </div>
</template>