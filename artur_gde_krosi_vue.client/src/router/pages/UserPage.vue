<template>
    <nav class="top-bar">
        <p class="bread-crumb" @click="$router.go(-1)"><span class="bread-crumb-back-arrow"><font-awesome-icon :icon="['fas', 'arrow-left-long']" /></span> Назад</p>
        <div class="bread-crumbs">
            <span class="bread-crumb" @click="$router.push('/')">Главная</span>
            /
            <span class="bread-crumb bread-crumb-current">Профиль</span>
        </div>
    </nav>
    <div class="user-account">
        <div class="user-account-content">
            <UserPageSidePanel />
            <UserPageEdit />
            <div class="user-account-dummy-div" style="width: 20%;"></div>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue'
    import UserPageSidePanel from '@/components/UserPage/UserPageSidePanel.vue';
    import UserPageEdit from '@/components/UserPage/UserPageAccountEdit.vue';

    export default defineComponent({
        components: { UserPageEdit, UserPageSidePanel },
    })
</script>