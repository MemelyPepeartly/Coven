import { Component, Input } from '@angular/core';
import { UserWorldMeta, WorldMeta, WorldsSummary } from '../interfaces/World';
import { CovenApiService } from 'src/services/coven.api.service';
import { Article, WorldArticlesSummary } from '../interfaces/Article';

@Component({
  selector: 'app-summary',
  templateUrl: './summary.component.html',
  styleUrls: ['./summary.component.scss']
})
export class SummaryComponent {
  
  constructor(public covenService: CovenApiService) {}

  @Input() summary!: WorldsSummary | undefined;
  loadingWorldSummary: boolean = false;
  worldArticleSummary: WorldArticlesSummary | undefined;

  async GetWorldArticles(worldMeta: UserWorldMeta)
  {
    this.loadingWorldSummary = true;

    this.worldArticleSummary = await this.covenService.GetWorldArticleSummary(worldMeta.id);
    
    this.loadingWorldSummary = false;
  }
}
