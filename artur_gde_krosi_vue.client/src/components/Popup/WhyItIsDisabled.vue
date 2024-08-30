<template>
    <div class="change-data-confirmation-popup">
        <h3>Этот модуль недоступен</h3>
        <p>В связи с тем, что это демонстрационная версия нашего проекта, этот модуль отключён.</p>
        <BorderedButton class="confirm-popup-changes-button" @click="closePopup()">Понятно!</BorderedButton>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapMutations } from 'vuex';
    export default defineComponent({

        methods: {
            ...mapMutations(['setPopupVisibility']),

            closePopup(this: any) {
                this.setPopupVisibility(false);
            }
        },
    })
</script>
