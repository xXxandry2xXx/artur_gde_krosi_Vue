<template>
    <div class="characteristic-popup">
        <div class="characteristic-popup-form">
            <h3>Новое значение характеристики</h3>
            <div class="characteristic-popup-field">
                <span>Значение</span>
                <DefaultInput placeholder="Значение характеристики" v-model="charNewValue.value" />
            </div>
        </div>
        <div class="characteristic-popup-buttons">
            <BorderedButton @click="addNewValueToChar">Сохранить</BorderedButton>
            <BorderedButton @click="setPopupVisibility(false)">Отмена</BorderedButton>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from "vue";
    import { mapActions, mapGetters, mapMutations } from 'vuex';

    export default defineComponent({

        data() {
            return {
                charNewValue: {
                    targetCharId: '',
                    value: ''
                }
            }
        },

        methods: {
            ...mapMutations(['setPopupVisibility']),
            ...mapActions(['addValueToChar']),
            ...mapGetters(['getCurrentCharacteristicId']),

            addNewValueToChar(this: any) {
                if (this.charNewValue.value.length > 0) {
                    this.addValueToChar(this.charNewValue);
                    this.setPopupVisibility(false);
                } else {
                    console.log('Заполните обязательное поле (Значение)')
                }
            },
        },

        mounted() {
            this.charNewValue.targetCharId = this.getCurrentCharacteristicId();
        }
    })
</script>