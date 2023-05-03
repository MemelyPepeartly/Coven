import { Component, EventEmitter, Input, Output } from '@angular/core';
import { UserWorldMeta, WorldMeta, WorldsSummary } from '../interfaces/World';
import { CovenApiService } from 'src/services/coven.api.service';
import { Article, ArticleMeta, WorldArticlesSummary } from '../interfaces/Article';

@Component({
  selector: 'app-summary',
  templateUrl: './summary.component.html',
  styleUrls: ['./summary.component.scss']
})
export class SummaryComponent {
  
  constructor(public covenService: CovenApiService) {}

  @Input() summary: WorldsSummary;
  @Output() worldClickedEmitter = new EventEmitter<UserWorldMeta>();

  async emitWorldClicked(worldMeta: UserWorldMeta)
  {
    this.worldClickedEmitter.emit(worldMeta);
  }
}
