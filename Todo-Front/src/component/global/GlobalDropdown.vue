<script setup lang="ts">
import { ref } from 'vue';

const display = ref(false);
</script>

<template>
  <div
    @mouseleave="() => (display = false)"
    @blur="() => (display = false)"
    class="group/dropdown select-none m-1"
  >
    <div
      @click="() => (display = !display)"
      class="group-hover:opacity-100 lg:opacity-0 p-1 cursor-pointer font-black text-xs text-light leading-[.35rem] hover:bg-accent rounded"
    >
      <div
        v-for="(_, id) in new Array(3)"
        :key="id"
        class="px-1"
      >
        •
      </div>
    </div>
    <div
      v-show="display"
      class="group-hover/dropdown:block absolute right-0 bg-light text-dark p-1 border-2 rounded w-fit"
    >
      <slot>option</slot>
    </div>
  </div>
</template>
