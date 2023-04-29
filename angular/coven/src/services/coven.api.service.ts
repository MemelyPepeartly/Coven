import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

const endpoint = "https://localhost:7127/api/"; 

@Injectable({
  providedIn: 'root'
})
export class CovenApiService {

  constructor() { }

  

  async GetWorldAnvilUser()
  {
    const response = await fetch(endpoint + "World/GetAnvilUser");
    const data = await response.json();

    console.log(data);
  }

  async GetWorldInfo()
  {
    const response = await fetch(endpoint + "World/GetWorldInfo");
    const data = await response.json();

    console.log(data);
  }
}
