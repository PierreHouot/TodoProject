<script setup lang="ts">
import type { Activity } from '@/models/activity';
import { ref } from 'vue';
import GlobalModal from './global/GlobalModal.vue';
import GlobalButton from './global/GlobalButton.vue';
import ActivityApi from '@/api/activity.api.ts';
import { useActivityStore } from '@/stores/activity.ts';
const store = useActivityStore();

defineProps({
  show: { required: true, type: Boolean },
});

const emit = defineEmits(['posted']);

const activity = ref<Activity>({ name: '', description: '' });

function postActivity() {
  if (!activity.value.name) return;
  ActivityApi.create(activity.value)
    .then(() => store.fetchActivities())
    .finally(() => emit('posted'));
}
</script>

<template>
  <GlobalModal :is-displayed="show">
    <div class="flex flex-col">
      <label class="mb-1 mt-2">Name</label>
      <input
        v-model="activity.name"
        class="bg-light rounded p-1"
        placeholder="Activity..."
      />
      <label class="mb-1 mt-2">Description</label>
      <textarea
        v-model="activity.description"
        class="bg-light rounded p-1"
        placeholder="Description..."
        rows="4"
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
