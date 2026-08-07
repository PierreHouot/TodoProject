import type { Temporal } from "@js-temporal/polyfill";

export type Activity = {
  id?: string;
  name: string;
  description?: string;
  date?: Temporal.PlainDate
};
