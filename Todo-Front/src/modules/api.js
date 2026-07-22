import { type Pet } from '../../types/pets';

import axios, { AxiosRequestConfig, Method } from 'axios'
/**
 * Méthode générique qui s'occupera de tous les appels API
 *
 * @param path - Chemin api, ex: `/pets`
 * @param method - Méthode HTTP
 * @param config - Config optionnelle d'axios
 * @returns Retourne une promesse du type passé en Generic TS
 */
const callAPI = async < R > (path: string, method: Method, config: Partial<AxiosRequestConfig> = {}): Promise<R> => {
  const res = await axios({
        url: `http://localhost:3000${path}`,
    method,
    ...config
  })

    return res.data
}

    export const api = {
        getPets() {
    return callAPI<Pet[]>(`/pets`, 'GET')
  },

    updatePet(petID: number, data: Partial<Pet>) {
    return callAPI<Pet>(`/pets/${petID}`, 'PATCH', {data})
  },

            getPet(petID: number) {
    return callAPI<Pet>(`/pets/${petID}`, 'GET')
  },

                createPet(data: Omit<Pet, 'id'>) {
    return callAPI<Pet>(`/pets`, 'POST', {data})
  },

                    deletePet(petID: number) {
    return callAPI<void>(`/pets/${petID}`, 'DELETE')
  }
}