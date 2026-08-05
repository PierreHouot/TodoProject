<script setup lang="ts">
import type { Activity } from '@/models/activity';
import type { PropType } from 'vue';
import GlobalButton from './global/GlobalButton.vue';
import ActivityApi from '@/api/activity.api.ts';
import { useActivityStore } from '@/stores/activity.ts';

const store = useActivityStore();

defineProps({
  activity: { required: true, type: Object as PropType<Activity> },
});

function deleteMoment(id?: string) {
  if (!id) return;
  ActivityApi.delete(id).then(() => store.fetchActivities());
}
</script>

<template>
  <div class="w-full group text-sm tracking-tight leading-tight relative">
    <div class="flex z-10 relative justify-between bg-dark p-2 rounded-xl text-center">
      <div class="text-light text-center w-full text-lg">{{ activity.name }}</div>
      <GlobalButton
        @click="deleteMoment(activity.id)"
        class="group-hover:opacity-100 opacity-0"
        >🗑️</GlobalButton
      >
    </div>
    <div
      v-if="activity.description"
      class="bg-block relative -top-2.5 text-dark pt-4 p-2 rounded-b-xl"
    >
      {{ activity.description }}
    </div>
  </div>
</template>
