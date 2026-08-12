<script setup lang="ts">
import type { Activity } from '@/models/activity';
import { computed, ref, watch } from 'vue';
import GlobalModal from './global/GlobalModal.vue';
import GlobalButton from './global/GlobalButton.vue';
import ActivityApi from '@/api/activity.api.ts';
import { useActivityStore } from '@/stores/activity.store.ts';
import { useToasterStore } from '@/stores/toaster.store.ts';
import type { AxiosError } from 'axios';

const store = useActivityStore();
const toast = useToasterStore();
const editedActivity = ref<Activity | undefined>(undefined);

const props = defineProps({
  show: { required: true, type: Boolean },
  activityId: { required: true, type: String },
});

const emit = defineEmits(['modified']);

watch(
  () => props.activityId,
  () => {
    const activity = store.activities?.find((item) => item.id == props.activityId);
    editedActivity.value = activity ? { ...activity } : undefined;
  },
);

const display = computed(() => props.show && editedActivity.value);

function modifyActivity() {
  if (editedActivity.value == null) return;
  if (!editedActivity.value.id) return;

  ActivityApi.update(editedActivity.value.id, editedActivity.value)
    .then(() => {
      toast.sendToast('Moment Updated', `Moment "${editedActivity.value?.name}" has been updated`);
    })
    .finally(() => {
      store.fetchActivities();
      emit('modified');
    })
    .catch((error: AxiosError) => {
      toast.sendToast(error.name, error.message);
    });
}
</script>

<template>
  <GlobalModal :is-displayed="display">
    <div class="flex flex-col">
      <label
        for="name"
        class="mb-1 mt-2"
        >Name</label
      >
      <input
        id="name"
        v-model="editedActivity!.name"
        class="bg-light rounded p-1"
        placeholder="Activity..."
      />
      <label
        for="desc"
        class="mb-1 mt-2"
        >Description</label
      >
      <textarea
        id="desc"
        v-model="editedActivity!.description"
        class="bg-light rounded p-1"
        placeholder="Description..."
        rows="4"
      />
      <label
        for="date"
        class="mb-1 mt-2"
      >
        Date</label
      >
      <input
        class="bg-light rounded p-1"
        id="date"
        type="date"
        v-model="editedActivity!.date"
      />
    </div>
    <template #extra-buttons>
      <GlobalButton
        class="bg-accent text-light hover:bg-light hover:text-accent"
        @click="modifyActivity()"
      >
        Send
      </GlobalButton>
    </template>
  </GlobalModal>
</template>
