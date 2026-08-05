import axios, { type AxiosRequestConfig, type Method } from 'axios';
/**
 * Méthode générique qui s'occupera de tous les appels API
 *
 * @param path - Chemin api, ex: path`
 * @param method - Méthode HTTP
 * @param config - Config optionnelle d'axios
 * @returns Retourne une promesse du type passé en Generic TS
 */

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
    return await callAPI<void>(`${path}/${id}`, 'DELETE');
  },
};
