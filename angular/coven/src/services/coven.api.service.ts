import { Injectable } from '@angular/core';
import { WorldArticlesSummary } from 'src/app/interfaces/Article';
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
  
    console.log(data);
    return data;
  }

  async GetWorldInfo(): Promise<WorldsSummary> {
    const response = await fetch(endpoint + "World/GetWorldInfo");
    const data = await response.json() as WorldsSummary;
  
    console.log(data);
    return data;
  }

  async GetWorldArticleSummary(worldId: string): Promise<WorldArticlesSummary> {
    const response = await fetch(`${endpoint}World/GetWorldArticleSummary?worldId=${worldId}`);
    const data = await response.json() as WorldArticlesSummary;
  
    console.log(data);
    return data;
  }
}
