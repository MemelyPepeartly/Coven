import { Author } from "./Author";

export interface World {
    id: string;
    name: string;
    locale: string;
    description: string;
    description_parsed: string;
    display_css: string;
    theme: number;
    tags: string;
    slug: string;
    url: string;
    author: Author;
  }
  