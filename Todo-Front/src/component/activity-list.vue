<script setup lang="ts">
import { useActivityStore } from '@/stores/activity';
import { storeToRefs } from 'pinia';
import { onMounted } from 'vue';
const store = useActivityStore()
const { activities } = storeToRefs(store)

// const Props|props = defineProps<Props>();
onMounted(async () => {
    await store.fetchActivities()
})
</script>

<template>
    <div>
        <div :key="task.id" v-for="task in activities">
            <div>{{ task.name }}</div>
        </div>
    </div>

</template>

<style scoped></style>
