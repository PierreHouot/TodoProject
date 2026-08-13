template
<script setup lang="ts">
import { computed, onMounted, ref } from 'vue';
import ActivityCard from '@/component/ActivityCard.vue';
import { storeToRefs } from 'pinia';
import { useActivityStore } from '@/stores/activity.store.ts';
import ActivityModalModifyForm from './ActivityModalModifyForm.vue';

const store = useActivityStore();
const { activities } = storeToRefs(store);
const showModal = ref(false);

onMounted(async () => {
  await store.fetchActivities();
});

const editedActivityId = ref<string>('');

const hasActivities = computed(() => activities.value && activities.value.length > 0);

function showYear(index: number) {
  if (!activities.value) return false;
  if (index === 0) return true;
  return activities.value[index]?.date?.year !== activities.value[index - 1]?.date?.year;
}

function editActivity(id: string) {
  editedActivityId.value = id;
  showModal.value = true;
}
</script>

<template>
  <div class="flex">
    <div
      v-if="hasActivities"
      class="overflow-y-scroll h-full px-2 md:border-l-2 border-surface"
    >
      <div
        :key="activity.id"
        v-for="(activity, id) in activities"
      >
        <div
          v-if="showYear(id)"
          class="flex items-center mt-2"
          :id="activity.date?.year.toString()"
        >
          <div class="font-title text-surface">{{ activity.date.year }}</div>
          <div class="w-full mx-4 h-0 border-surface border-2 rounded"></div>
        </div>
        <ActivityCard
          :activity="activity"
          class="pl-4 mb-2"
          @edit="(id: string) => editActivity(id)"
        />
      </div>
    </div>
    <div v-else>Pas d'activités</div>
    <ActivityModalModifyForm
      :show="showModal"
      :activityId="editedActivityId"
      @close="() => (showModal = false)"
      @modified="() => (showModal = false)"
    />
  </div>
</template>
