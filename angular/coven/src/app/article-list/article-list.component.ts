import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ArticleMeta } from '../interfaces/Article';
import { CovenApiService } from 'src/services/coven.api.service';

@Component({
  selector: 'app-article-list',
  templateUrl: './article-list.component.html',
  styleUrls: ['./article-list.component.scss']
})
export class ArticleListComponent implements OnInit {

  constructor(public covenService: CovenApiService) {}

  @Input() articles: ArticleMeta[] = [];
  @Input() worldId: string = "";

  displayedColumns: string[] = ['title', 'type', 'actions'];
  dataSource: MatTableDataSource<ArticleMeta> = new MatTableDataSource<ArticleMeta>();
  
  @Output() articleClickedEmitter = new EventEmitter<any>();

  ngOnInit() {
    this.dataSource.data = this.articles;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  editArticle(article: ArticleMeta) {
    this.articleClickedEmitter.emit(article);
  }


}
