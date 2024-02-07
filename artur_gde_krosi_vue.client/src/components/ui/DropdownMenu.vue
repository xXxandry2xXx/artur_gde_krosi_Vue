<template>
    <select>
        <option v-for="option in options" :key="option.id">{{ option }}</option>
    </select>
</template>

<script lang="ts">
    import { defineComponent } from "vue";

    export default defineComponent({
        data() {
            return {
                name: 'dropdown-menu'
            }
        },

        props: {
            options: {
                type: Array,
                required: true
            }
        }
    })
</script>