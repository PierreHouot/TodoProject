<script setup lang="ts">
import ActivityCard from '@/component/ActivityCard.vue';
import { useActivityStore } from '@/stores/activity';
import { storeToRefs } from 'pinia';
import { computed, onMounted } from 'vue';
const store = useActivityStore()
const { activities } = storeToRefs(store)

// const Props|props = defineProps<Props>();
onMounted(async () => {
    await store.fetchActivities()
})

const hasActivities = computed(() => activities.value && activities.value.length > 0)
</script>

<template>
    <div class="inline-block w-96">
        <div v-if="hasActivities">
            <div :key="activity.id" v-for="activity in activities">
                <ActivityCard :activity="activity" />
            </div>
        </div>
        <div v-else>Pas d'activités</div>
    </div>
</template>
