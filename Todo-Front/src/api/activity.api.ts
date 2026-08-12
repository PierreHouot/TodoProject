import type { Activity } from '@/models/activity';
import { api } from '../api/client';
import { Temporal } from '@js-temporal/polyfill';
import type { ActivityDTO } from './requests/activityDTO';
import type { activityCreate } from './requests/activityCreate';

const path = 'activity';

const ActivityApi = {
  async getAll() {
    return (await api.get<ActivityDTO>(path)).map((item) => ({
      ...item,
      date: stringToPlainDate(item.date),
    })) as Activity[];
  },

  async getById(id: string) {
    return (await api
      .getById<ActivityDTO>(path, id)
      .then((item) => ({ ...item, date: stringToPlainDate(item.date) }))) as Activity;
  },

  async create(data: activityCreate) {
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

function stringToPlainDate(date: string) {
  return Temporal.PlainDate.from(date);
}
