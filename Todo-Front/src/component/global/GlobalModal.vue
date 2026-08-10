<script setup lang="ts">
import GlobalButton from './GlobalButton.vue';

defineProps({
  isDisplayed: {
    required: true,
  },
});

const emit = defineEmits(['close']);
</script>

<template>
  <div
    v-if="isDisplayed"
    class="fixed top-0 right-0 flex justify-center items-center h-screen w-screen z-50 bg-[rgba(0,0,0,0.7)]"
    @click.self="() => emit('close')"
  >
    <div
      class="z-20 flex flex-col min-h-1/3 max-h-4/5 min-w-1/3 max-w-4/5 relative shadow-2xl justify-between bg-surface text-dark rounded p-2"
    >
      <div class="w-full max-h-[calc(100%-2.5em)] overflow-scroll text-wrap p-2">
        <slot>kikou</slot>
      </div>
      <div class="flex justify-end">
        <slot name="extra-buttons"></slot>
        <global-button
          @click="() => emit('close')"
          class="bg-dark text-light"
        >
          Close
        </global-button>
      </div>
    </div>
  </div>
</template>
