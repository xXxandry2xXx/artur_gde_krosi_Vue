<template>
    <div class="characteristic-popup-listed-char">
        <h5>Значение {{ valNumber + 1 }}</h5>
        <DefaultInput :value="charValue.value" @input="handleValueEditing"/>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from "vue";
    import { mapMutations } from 'vuex';

    export default defineComponent({

        data() {
            return {
                newValue: ''
            }
        },

        props: ['charValue', 'valNumber'],

        methods: {
            ...mapMutations(['addCurrentCharacteristicValue']),

            handleValueEditing(this: any, event: Event) {
                let element = event.target as HTMLInputElement;
                this.newValue = element.value;
            }
        },

        computed: {
            editedValue(this: any) {
                return { id: this.charValue.characteristicProductValueId, value: this.newValue }
            }
        },

        watch: {
            newValue(this: any) {
                this.addCurrentCharacteristicValue(this.editedValue);
            }
        },

        mounted() {
            console.log()
        }
    })
</script>