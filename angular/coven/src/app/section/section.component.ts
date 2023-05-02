import { Component, Input, OnInit } from '@angular/core';
import { CovenApiService } from 'src/services/coven.api.service';

@Component({
  selector: 'app-section',
  templateUrl: './section.component.html',
  styleUrls: ['./section.component.scss']
})
export class SectionComponent implements OnInit {
  constructor(public covenService: CovenApiService){}

  async ngOnInit() {

  }
}
