import { Component, OnInit } from '@angular/core';
import { CovenApiService } from 'src/services/coven.api.service';
import { Author } from '../interfaces/Author';
import { ImplicitReceiver } from '@angular/compiler';
import { World, WorldsSummary } from '../interfaces/World';
import { Article } from '../interfaces/Article';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  constructor(public covenService: CovenApiService) {}

  author: Author | undefined;
  summary: WorldsSummary | undefined;
  selectedArticle: Article | undefined;

  async ngOnInit() {
    
  }

  async getUser() {
    this.author = await this.covenService.GetWorldAnvilUser();
  }
  async getWorld() {
    this.summary = await this.covenService.GetWorldInfo(); 
  }
  async getSelectedArticle(articleId: string) {
    this.selectedArticle = await this.covenService.GetFullArticle(articleId);
  }
}
