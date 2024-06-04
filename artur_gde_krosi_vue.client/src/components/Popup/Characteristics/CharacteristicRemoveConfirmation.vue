<template>
    <div class="characteristic-popup">
        <div class="characteristic-popup-form">
            <h3>Удаление характеристики</h3>
            <p class="characteristic-popup-text">Вы уверены что хотите удалить эту характеристику?</p>
        </div>
        <div class="characteristic-popup-buttons">
            <BorderedButton @click="confirmRemoving">Да</BorderedButton>
            <BorderedButton @click="declineRemoving">Нет</BorderedButton>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from "vue";
    import { mapGetters, mapActions, mapMutations } from 'vuex';

    export default defineComponent({
        data() {
            return {
                newCharacteristic: {
                    name: '',
                    value: ''
                }
            }
        },

        methods: {
            ...mapMutations(['setPopupVisibility']),
            ...mapGetters(['getCurrentCharacteristicId']),
            ...mapActions(['removeChar']),

            confirmRemoving() {
                this.removeChar(this.getCurrentCharacteristicId());
                this.setPopupVisibility(false);
            },

            declineRemoving() {
                this.setPopupVisibility(false);
            }
        },

        mounted() {
            // console.log()
        }
    })
</script>