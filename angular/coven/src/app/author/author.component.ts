import { Component, Input } from '@angular/core';
import { Author } from '../interfaces/Author';

@Component({
  selector: 'app-author',
  templateUrl: './author.component.html',
  styleUrls: ['./author.component.scss']
})
export class AuthorComponent {
  @Input() author!: Author;
}
