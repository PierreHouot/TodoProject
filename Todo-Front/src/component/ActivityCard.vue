<script setup lang="ts">
import type { Activity } from '@/models/activity';
import { computed, type PropType } from 'vue';
import GlobalButton from './global/GlobalButton.vue';
import ActivityApi from '@/api/activity.api';
import { useActivityStore } from '@/stores/activity.store';
import GlobalDropdown from './global/GlobalDropdown.vue';
import { useToasterStore } from '@/stores/toaster.store';

const store = useActivityStore();
const toast = useToasterStore();

const props = defineProps({
  activity: { required: true, type: Object as PropType<Activity> },
});

const emit = defineEmits(['edit']);

const formatedDate = computed(() =>
  props.activity.date?.toLocaleString(undefined, { dateStyle: 'full' }),
);

function deleteMoment(id?: string) {
  if (!id) return;
  ActivityApi.delete(id).then(() => {
    toast.sendToast('Moment Deleted', `Moment "${props.activity.name}" has been deleted`);
    store.fetchActivities();
  });
}
</script>

<template>
  <div class="w-full group tracking-tight leading-tight">
    <div class="flex relative justify-between items-center bg-dark rounded-t-xl text-center h-fit">
      <div class="w-[95%] text-light ml-0.5 max-md:text-lg">
        {{ activity.name }}
      </div>

      <GlobalDropdown class="z-1 text-xs max-md:text-lg w-max">
        <GlobalButton
          @click="() => emit('edit', activity.id)"
          class="hover:text-light block"
          >Modify
        </GlobalButton>
        <GlobalButton
          @click="deleteMoment(activity.id)"
          class="hover:text-light block"
          >Delete
        </GlobalButton>
      </GlobalDropdown>
    </div>
    <div
      class="bg-block text-[13px] text-dark p-2 border-2 rounded-b-2xl border-dark max-md:text-base"
    >
      <div class="whitespace-break-spaces">{{ activity.description }}</div>
      <div class="text-xs max-md:text-sm text-end opacity-70">{{ formatedDate }}</div>
    </div>
  </div>
</template>
