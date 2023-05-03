import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { Article } from '../interfaces/Article';
import { CovenApiService } from 'src/services/coven.api.service';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ArticleComponent implements OnInit {
  constructor(public covenService: CovenApiService){}

  @Input() selectedArticle: Article;
  @Input() articleLoaded: boolean = false;

  async ngOnInit() {
  }

}
