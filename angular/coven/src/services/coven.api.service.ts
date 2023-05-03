import { Injectable } from '@angular/core';
import { Article, ArticleMeta, WorldArticlesSummary } from 'src/app/interfaces/Article';
import { Author } from 'src/app/interfaces/Author';
import { World, WorldsSummary } from 'src/app/interfaces/World';

const endpoint = "https://localhost:7127/api/"; 

@Injectable({
  providedIn: 'root'
})
export class CovenApiService {

  constructor() { }

  async GetWorldAnvilUser(): Promise<Author>
  {
    const response = await fetch(endpoint + "World/GetAnvilUser");
    const data = await response.json() as Author;

    return data;
  }

  async GetWorldInfo(): Promise<WorldsSummary> {
    const response = await fetch(endpoint + "World/GetWorldInfo");
    const data = await response.json() as WorldsSummary;

    return data;
  }

  async GetWorldArticleSummary(worldId: string): Promise<WorldArticlesSummary> {
    const response = await fetch(`${endpoint}World/${worldId}/GetWorldArticleSummary`);
    const data = await response.json() as WorldArticlesSummary;

    return data;
  }

  async GetFullArticle(articleId: string): Promise<Article> {
    const response = await fetch(`${endpoint}World/GetWorldArticles/${articleId}`);
    const data = await response.json() as Article;

    return data;
  }

  async GetArticleMetas(worldId: string): Promise<Array<ArticleMeta>>
  {
    const response = await fetch(`${endpoint}World/${worldId}/GetWorldArticleMetas`);
    const data = await response.json() as Array<ArticleMeta>;

    return data;
  }
}
