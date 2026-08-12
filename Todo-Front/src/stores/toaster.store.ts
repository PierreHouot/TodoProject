import type { Toast } from '@/models/toast';
import { defineStore } from 'pinia';
import { ref } from 'vue';

export const useToasterStore = defineStore('toaster', () => {
  const notifications = ref<Toast[]>([]);

  function sendToast(notif: Toast) {
    notifications.value.push(notif);
  }

  function discardFirstToast() {
    notifications.value.shift();
  }

  return { notifications, sendToast, discardFirstToast };
});
