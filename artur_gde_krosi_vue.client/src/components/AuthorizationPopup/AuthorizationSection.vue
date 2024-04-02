<template>
    <div class="authorization-popup-section" v-if="$store.state.loginPopupMode === currentMode">
        <h3>{{ currentModeText }}</h3>
        <DefaultInput class="authorization-popup-field" v-for="field in fields" :placeholder="field.name"/>
        <BorderedButton class="authorization-popup-button">{{ currentModeText }}</BorderedButton>

        <div class="another-way-to-authorize">
            <div class="authorization-separator">
                <div class="authorization-separator-line"></div>
                <span class="authorization-separator-text">ИЛИ</span>
                <div class="authorization-separator-line"></div>
            </div>
            <button class="another-way-to-authorize-button" @click="openLoginPopup(anotherWayMode)">{{ anotherWayButtonText }}</button>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapMutations } from 'vuex';

    export default defineComponent({
        props: {
            currentMode: {
                type: String,
                required: true,
            },

            currentModeText: {
                type: String,
                required: true,
            },

            anotherWayButtonText: {
                type: String,
                required: true,
            },

            anotherWayMode: {
                type: String,
                required: true,
            },

            fields: {
                type: Array,
                required: true,
            }
        },

        methods: {
            ...mapMutations(['openLoginPopup'])
        }
    })
</script>