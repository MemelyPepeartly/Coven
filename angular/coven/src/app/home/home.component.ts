import { Component } from '@angular/core';
import { CovenApiService } from 'src/services/coven.api.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  constructor(public covenService: CovenApiService) {}

  async getUser() {
    await this.covenService.GetWorldAnvilUser();
  }
  async getWorld() {
    await this.covenService.GetWorldInfo(); 
  }
}
