<template>
    <nav class="top-bar">
        <span class="bread-crumb" @click="$router.go(-1)">Назад</span>
        <div class="bread-crumbs">
            <span class="bread-crumb" @click="$router.push('/')">Главная</span>
            /
            <span class="bread-crumb bread-crumb-current">Профиль</span>
        </div>
    </nav>

    <div class="user-account-content">
        <div class="user-account-side-panel">
            <h1>{{ $store.state.authorizedUser.userName }}</h1>
            <div class="user-account-buttons">
                <div class="user-account-button">
                    <font-awesome-icon :icon="['fas', 'user']" />
                    <span>Профиль</span>
                </div>
                <div class="user-account-button">
                    <font-awesome-icon :icon="['fas', 'box']" />
                    <span>Заказы</span>
                </div>
                <div class="user-account-button">
                    <span class="logout-button-icon"><font-awesome-icon :icon="['fas', 'right-from-bracket']" /></span>
                    <span>Выход</span>
                </div>
            </div>
        </div>
        <div class="user-account-editing">
            <div class="user-account-fields">
                <div class="user-account-field">
                    <span>Имя:</span>
                    <DefaultInput :value="$store.state.authorizedUser.name" placeholder="Введите своё имя"/>
                </div>
                <div class="user-account-field">
                    <span>Фамилия:</span>
                    <DefaultInput :value="$store.state.authorizedUser.surname" placeholder="Введите свою фамилию"/>
                </div>
                <div class="user-account-field">
                    <span>Отчество:</span>
                    <DefaultInput :value="$store.state.authorizedUser.patronymic" placeholder="Введите своё отчество"/>
                </div>
                <div class="user-account-field">
                    <span>E-mail:</span>
                    <DefaultInput :value="$store.state.authorizedUser.email" placeholder="Введите свой E-mail адрес"/>
                </div>
                <div class="user-account-field">
                    <span>Телефон:</span>
                    <DefaultInput v-if="$store.state.authorizedUser.phoneNumber !== null" :value="$store.state.authorizedUser.phoneNumber" placeholder="Введите свой номер телефона"/>
                    <DefaultInput v-else placeholder="Не указан" />
                </div>
            </div>
            <div class="user-account-password-fields">
                <div class="user-account-field">
                    <span>Новый пароль</span>
                    <DefaultInput placeholder="Введите новый пароль"/>
                </div>
                <div class="user-account-field">
                    <span>Подтвердите новый пароль</span>
                    <DefaultInput placeholder="Подтвердите новый пароль"/>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';

    export default defineComponent({

    })
</script>