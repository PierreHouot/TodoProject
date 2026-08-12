import type { Toast } from '@/models/toast';
import { defineStore } from 'pinia';
import { ref } from 'vue';

export const useToasterStore = defineStore('toaster', () => {
  const notifications = ref<Toast[]>([]);

  function sendToast(title: string, message: string) {
    notifications.value.push({ title, message });
  }

  function discardFirstToast() {
    notifications.value.shift();
  }

  return { notifications, sendToast, discardFirstToast };
});
