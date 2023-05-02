import { Author } from "./Author";
import { Category } from "./Category";
import { Sections } from "./Section";
import { WorldMeta } from "./World";

export interface Article {
    id: string;
    title: string;
    template: string;
    is_wip: boolean;
    is_draft: boolean;
    state: string;
    passcode: string;
    wordcount: number;
    creation_date: WorldAnvilDateDTO;
    update_date: WorldAnvilDateDTO;
    publication_date: WorldAnvilDateDTO;
    notification_date: WorldAnvilDateDTO;
    tags: string;
    url: string;
    category: Category; // Make sure to define the Category interface, based on your Category class
    author: Author;
    world: WorldMeta;
    content: string;
    content_parsed: string;
    sections: Sections;
    relations: any; // Replace with a more specific type if you know the structure
    full_render: string;
}
export interface WorldAnvilDateDTO {
    date: string; // Note: Date type in C# is serialized as a string in JSON
    timezone_type: number;
    timezone: string;
}

export interface WorldArticlesSummary {
    world: WorldMeta;
    term: string;
    offset: string;
    order_by: string;
    trajectory: string;
    articles: ArticleMeta[];
}
export interface ArticleMeta {
    id: string;
    title: string;
    state: string;
    is_wip: boolean;
    is_draft: boolean;
    template_type: string;
    wordcount?: number;
    views?: number;
    likes?: number;
    exerpt: string;
    tags: string;
    adult_content: boolean;
    last_update: WorldAnvilDateDTO;
    url: string;
    author: Author;
    world: WorldMeta;
}