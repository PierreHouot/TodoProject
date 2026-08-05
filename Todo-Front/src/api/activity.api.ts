import type { Activity } from '@/models/activity';
import { api } from '../api/client';

const path = 'activity';

const ActivityApi = {
  async getAll() {
    return await api.get<Activity>(path);
  },

  async getById(id: string) {
    return await api.getById(path, id);
  },

  async create(data: Activity) {
    console.log(data);

    return await api.create(path, data);
  },

  async update(id: string, data: Partial<Activity>) {
    return await api.update(path, id, data);
  },

  async delete(id: string) {
    return await api.delete(path, id);
  },
};

export default ActivityApi;
