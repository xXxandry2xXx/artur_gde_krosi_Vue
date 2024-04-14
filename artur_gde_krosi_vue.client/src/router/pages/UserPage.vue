<template>
    <nav class="top-bar">
        <span class="bread-crumb" @click="$router.go(-1)">Назад</span>
        <div class="bread-crumbs">
            <span class="bread-crumb" @click="$router.push('/')">Главная</span>
            /
            <span class="bread-crumb bread-crumb-current">Профиль</span>
        </div>
    </nav>

    <UserPageAccountSection />





    <!--<span>Имя пользователя: {{ $store.state.authorizedUser.userName }}</span>
    <span>E-mail: {{ $store.state.authorizedUser.email }}</span>
    <span>ФИО: {{ $store.state.authorizedUser.surname }} {{ $store.state.authorizedUser.name }} {{ $store.state.authorizedUser.patronymic }}</span>-->
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import UserPageTabs from '@/components/UserPage/UserPageTabs.vue';
    import UserPageAccountSection from '@/components/UserPage/UserPageAccountSection.vue';


    export default defineComponent({
        components: { UserPageAccountSection, UserPageTabs }
    })
</script>