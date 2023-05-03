import { Component, OnInit } from '@angular/core';
import { CovenApiService } from 'src/services/coven.api.service';
import { Author } from '../interfaces/Author';
import { ImplicitReceiver } from '@angular/compiler';
import { UserWorldMeta, World, WorldMeta, WorldsSummary } from '../interfaces/World';
import { Article, ArticleMeta, WorldArticlesSummary } from '../interfaces/Article';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  constructor(public covenService: CovenApiService) {}

  author: Author;
  summary: WorldsSummary;
  selectedArticle: Article;

  loadingWorldSummary: boolean = false;
  worldArticleSummary: WorldArticlesSummary;

  async ngOnInit() {
    
  }

  async getUser() {
    this.author = await this.covenService.GetWorldAnvilUser();

    this.getWorld();
  }
  async getWorld() {
    this.summary = await this.covenService.GetWorldInfo(); 
  }
  async getWorldArticleSummary(worldMeta: UserWorldMeta) {
    this.loadingWorldSummary = true;
    this.worldArticleSummary = await this.covenService.GetWorldArticleSummary(worldMeta.id);
    this.worldArticleSummary.articles = await this.covenService.GetArticleMetas(worldMeta.id);
    this.loadingWorldSummary = false;
  }
  async getSelectedArticle(article: ArticleMeta) {
    this.selectedArticle = await this.covenService.GetFullArticle(article.id);
  }
}
