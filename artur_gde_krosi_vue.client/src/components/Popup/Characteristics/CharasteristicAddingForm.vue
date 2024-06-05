<template>
    <div class="characteristic-popup">
        <div class="characteristic-popup-form">
            <h3>Новая характеристика</h3>
            <div class="characteristic-popup-field">
                <span>Название</span>
                <DefaultInput placeholder="Название характеристики" v-model="newCharacteristic.name"/>
            </div>
            <div class="characteristic-popup-field">
                <span>Значение</span>
                <DefaultInput placeholder="Значение характеристики" v-model="newCharacteristic.value"/>
            </div>
        </div>
        <div class="characteristic-popup-buttons">
            <BorderedButton @click="createNewChar(newCharacteristic)">Сохранить</BorderedButton>
            <BorderedButton @click="setPopupVisibility(false)">Отмена</BorderedButton>
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
            ...mapGetters(['getCurrentProductId']),
            ...mapActions(['createNewChar']),
        },

        mounted() {
           // console.log(this.getCurrentProductId())
        }
    })
</script>