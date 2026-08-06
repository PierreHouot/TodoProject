<script setup lang="ts">
import type { Activity } from '@/models/activity';
import { computed, type PropType } from 'vue';
import GlobalButton from './global/GlobalButton.vue';
import ActivityApi from '@/api/activity.api.ts';
import { useActivityStore } from '@/stores/activity.ts';

const store = useActivityStore();

const props = defineProps({
  activity: { required: true, type: Object as PropType<Activity> },
});

const formatedDate = computed(() =>
  new Date(props.activity.date!.toString()).toLocaleDateString(undefined, {
    day: '2-digit',
    month: 'short',
    year: 'numeric',
    weekday: 'short',
  }),
);

function deleteMoment(id?: string) {
  if (!id) return;
  ActivityApi.delete(id).then(() => store.fetchActivities());
}
</script>

<template>
  <div class="w-full group text-sm tracking-tight leading-tight relative">
    <div class="flex z-10 relative justify-between bg-dark rounded-xl text-center">
      <div class="text-light text-center w-full text-lg">
        {{ activity.name }}
      </div>
      <GlobalButton
        @click="deleteMoment(activity.id)"
        class="group-hover:opacity-100 opacity-0"
        >🗑️</GlobalButton
      >
    </div>
    <div class="bg-block relative -top-2.5 text-dark pt-4 p-2 rounded-b-xl">
      {{ activity.description }}
      <div class="text-xs mt-2 opacity-70">{{ formatedDate }}</div>
    </div>
  </div>
</template>
