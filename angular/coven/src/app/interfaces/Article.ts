import { Author } from "./Author";
import { Category } from "./Category";
import { Sections } from "./Section";
import { WorldMeta } from "./World";

export interface Article {
    id: string;
    title: string;
    template: string;
    isWip: boolean;
    isDraft: boolean;
    state: string;
    passcode: string;
    wordcount: number;
    creationDate: WorldAnvilDateDTO;
    updateDate: WorldAnvilDateDTO;
    publication_date: WorldAnvilDateDTO;
    notification_date: WorldAnvilDateDTO;
    tags: string;
    url: string;
    category: Category; // Make sure to define the Category interface, based on your Category class
    author: Author;
    world: WorldMeta;
    content: string;
    contentParsed: string;
    sections: Sections;
    relations: any; // Replace with a more specific type if you know the structure
    fullRender: string;
}
export interface WorldAnvilDateDTO {
    date: string; // Note: Date type in C# is serialized as a string in JSON
    timezoneType: number;
    timezone: string;
}

export interface WorldArticlesSummary {
    world: WorldMeta;
    term: string;
    offset: string;
    orderBy: string;
    trajectory: string;
    articles: ArticleMeta[];
}
export interface ArticleMeta {
    id: string;
    title: string;
    state: string;
    isWip: boolean;
    isDraft: boolean;
    templateType: string;
    wordcount?: number;
    views?: number;
    likes?: number;
    exerpt: string;
    tags: string;
    adultContent: boolean;
    lastUpdate: WorldAnvilDateDTO;
    url: string;
    author: Author;
    world: WorldMeta;
}