import { Component, Input, OnInit } from '@angular/core';
import { Article } from '../interfaces/Article';
import { CovenApiService } from 'src/services/coven.api.service';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.scss']
})
export class ArticleComponent implements OnInit {
  constructor(public covenService: CovenApiService){}

  @Input() selectedArticle!: Article;

  async ngOnInit() {
  }

}
