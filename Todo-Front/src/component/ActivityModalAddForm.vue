<script setup lang="ts">
import { ref } from 'vue';
import GlobalModal from './global/GlobalModal.vue';
import GlobalButton from './global/GlobalButton.vue';
import ActivityApi from '@/api/activity.api.ts';
import { useActivityStore } from '@/stores/activity.store.ts';
import type { activityCreate } from '@/api/requests/activityCreate.ts';
import { useToasterStore } from '@/stores/toaster.store.ts';
const store = useActivityStore();

const toast = useToasterStore();

defineProps({
  show: { required: true, type: Boolean },
});

const emit = defineEmits(['posted']);

const activity = ref<activityCreate>({ name: '' });
function postActivity() {
  ActivityApi.create(activity.value)
    .then(() => store.fetchActivities())
    .then(() => {
      toast.sendToast('New moment posted', `${activity.value.name} has been created`);
      emit('posted');
    })
    .finally(() => {
      activity.value.name = '';
      activity.value.description = '';
      activity.value.date = undefined;
    });
}
</script>

<template>
  <GlobalModal :is-displayed="show">
    <div class="flex flex-col">
      <label
        for="name"
        class="mb-1 mt-2"
        >Name</label
      >
      <input
        id="name"
        required
        v-model="activity.name"
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
        v-model="activity.description"
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
        v-model="activity.date"
      />
    </div>
    <template #extra-buttons>
      <GlobalButton
        class="bg-accent text-light hover:bg-light hover:text-accent"
        @click="postActivity()"
      >
        Send
      </GlobalButton>
    </template>
  </GlobalModal>
</template>
