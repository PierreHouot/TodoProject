<script setup lang="ts">
import type { Activity } from '@/models/activity';
import { computed, type PropType } from 'vue';
import GlobalButton from './global/GlobalButton.vue';
import ActivityApi from '@/api/activity.api.ts';
import { useActivityStore } from '@/stores/activity.ts';
import GlobalDropdown from './global/GlobalDropdown.vue';

const store = useActivityStore();

const props = defineProps({
  activity: { required: true, type: Object as PropType<Activity> },
});

const emit = defineEmits(['edit']);

const formatedDate = computed(() =>
  props.activity.date?.toLocaleString(undefined, { dateStyle: 'full' }),
);

function deleteMoment(id?: string) {
  if (!id) return;
  ActivityApi.delete(id).then(() => store.fetchActivities());
}
</script>

<template>
  <div class="w-full group tracking-tight leading-tight">
    <div class="flex z-10 relative justify-between bg-dark rounded-t-xl text-center">
      <div class="flex items-center justify-center text-light w-full ml-0.5">
        {{ activity.name }}
      </div>

      <GlobalDropdown>
        <GlobalButton
          @click="() => emit('edit', activity.id)"
          class="hover:text-light"
          >Modify
        </GlobalButton>
        <GlobalButton
          @click="deleteMoment(activity.id)"
          class="hover:text-light"
          >Delete
        </GlobalButton>
      </GlobalDropdown>
    </div>
    <div
      class="bg-block relative text-[13px] -top-3 text-dark pt-4 p-2 border-2 rounded-b-2xl border-dark"
    >
      <div>{{ activity.description }}</div>
      <div class="text-xs text-end opacity-70">{{ formatedDate }}</div>
    </div>
  </div>
</template>
