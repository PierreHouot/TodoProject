import { useToasterStore } from '@/stores/toaster.store';
import axios, { AxiosError, type AxiosRequestConfig, type Method } from 'axios';

const callAPI = async <Type>(
  path: string,
  method: Method,
  config: Partial<AxiosRequestConfig> = {},
): Promise<Type> => {
  const apiurl = `${import.meta.env.VITE_API_URL}/api/${path}`;
  const res = await axios({
    url: apiurl,
    method,
    ...config,
  }).catch((error: AxiosError) => {
    const toast = useToasterStore();
    toast.sendToast(error.name, `${error.message} ${error.response?.data as string}`);
    return Promise.reject(error);
  });

  return res.data;
};

export const api = {
  async get<Type>(path: string): Promise<Type[]> {
    return await callAPI(path, 'GET');
  },

  async update<Type>(path: string, id: string, data: Type) {
    return await callAPI<Type>(`${path}/${id}`, 'PUT', { data });
  },

  async getById<Type>(path: string, id: string) {
    return await callAPI<Type>(`${path}/${id}`, 'GET');
  },

  async create<Type>(path: string, data: Omit<Type, 'id'>) {
    return await callAPI<string>(path, 'POST', { data });
  },

  async delete(path: string, id: string) {
    return await callAPI(`${path}/${id}`, 'DELETE');
  },
};
