<template>
    <select>
        <option disabled>Сортировать по</option>
        <option v-for="option in options">{{ option.name }}</option>
    </select>
</template>

<script lang="ts">
    import { defineComponent } from "vue";

    export default defineComponent({
        name: 'DropdownMenu',

        props: {
            options: {
                type: Array,
                required: true
            }
        }
    })
</script>